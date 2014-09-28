using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using Xrm.Test1.DataModel.Dictionary.Common;
using Xrm.Test1.DataModel.Factory;

namespace Xrm.Test1.DataModel.SimpleFactory
{

    /// <summary>
    /// Фабрика элементов словаря основанная на кодогенерации
    /// </summary>
    public class DictionaryFactory:IDictionaryFactory
    {

        /// <summary>
        /// Словарь делегатов на генерацию объектов необходимого типа
        /// </summary>
        private static IDictionary<Type, Func<int, string, object>> _classDictionary;

        /// <summary>
        /// Конструктор ;)
        /// </summary>
        public DictionaryFactory()
        {
            if (_classDictionary == null)
            {
                var typeList = new DictionaryHelper().GetTypesNeedForGeneration().ToList();
                var sourceCode = GetAssemblySourceCode(typeList);
                var assembly = CompileAssembly(sourceCode);
                _classDictionary = GetClassesForInterfaces(assembly, typeList);
            }
        }

        /// <summary>
        /// Создание элемента словаря
        /// </summary>
        /// <typeparam name="T">Тип элемента словаря</typeparam>
        /// <param name="id">Идентификатор элемента словаря</param>
        /// <param name="name">Название элемента словаря</param>
        /// <returns>Созданный элемент словаря</returns>
        public T CreateDictionaryItem<T>(int id, string name) where T : IDictionaryItem
        {
            if (!_classDictionary.ContainsKey(typeof (T)))
            {
                return default (T);
            }
            return (T)_classDictionary[typeof (T)](id, name);
        }

        /// <summary>
        /// Создание словаря делегатов
        /// </summary>
        /// <param name="assembly">Сборка из которой надо доставать классы</param>
        /// <param name="typeList">Список интерфейсов для которых необходимо достать классы</param>
        /// <returns></returns>
        private IDictionary<Type, Func<int, string, object>> GetClassesForInterfaces(Assembly assembly, IEnumerable<Type> typeList)
        {
            var result = new Dictionary<Type, Func<int, string, object>>();
            foreach (var type in typeList)
            {
                var className = GetClassNameFromInterface(type);
                var typeObj = assembly.GetType("TmpObject." + className);

               result.Add(type, (id, name) => Activator.CreateInstance(typeObj, new object[] {id, name}));
            }
            return result;
        }

        /// <summary>
        /// Компиляция сборки из исходного кода
        /// </summary>
        /// <param name="sourceCode">исходный код</param>
        /// <returns></returns>
        private Assembly CompileAssembly(string sourceCode)
        {
            var providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v3.5" } };
            var provider = new CSharpCodeProvider(providerOptions);

            var compilerParams = new CompilerParameters { GenerateInMemory = true, GenerateExecutable = false };
            compilerParams.ReferencedAssemblies.Add("Xrm.Test1.DataModel.Dll");

            var results = provider.CompileAssemblyFromSource(compilerParams, sourceCode);

            return results.CompiledAssembly;
        }
        

        /// <summary>
        /// Генерация исходного кода сборки
        /// </summary>
        /// <param name="typeList"></param>
        /// <returns></returns>
        private string GetAssemblySourceCode(IEnumerable<Type> typeList)
        {

            const string source1 = @"
using Xrm.Test1.DataModel.Dictionary;

namespace TmpObject
{
    ";

            const string source4 = @"
        }
";

            var source = typeList.Aggregate(source1, (current, type) => current + (Environment.NewLine + GetClassSourceCode(type)));

            source += source4;
            return source;
        }

        /// <summary>
        /// Генерация исходного кода класса
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetClassSourceCode(Type type)
        {
            const string source2 = @"
    {
        ";
            const string source3 = @"(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }";
            var className = GetClassNameFromInterface(type);
            return string.Format("public class {0}:{1} ", className, type.Name) +
            source2 + string.Format("public {0}", className) + source3;
        }

        /// <summary>
        /// Генерация названия класса на основе класса интерфейса
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetClassNameFromInterface(Type type)
        {
            return type.Name.Remove(0, 1) + "Class";
        }
    }
}
