using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Dictionary.Common;
using Xrm.Test1.DataModel.Repositories.Common;
using Xrm.Test1.DataModel.SimpleFactory;
using Xrm.Test1.DbRepository.MsAccess.Common;
using Xrm.Test1.Merge.SimpleEngine;
using Xrm.Test1.RawData.E1;
using Xrm.Test1.RawData.E1.BaseJsonConverter;
using Xrm.Test1.Utils.DataGetter;

namespace Xrm.Test1.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootRepository = new MsAccesRootRepository(Path.GetFullPath("base.mdb"), new DictionaryFactory(),
                new PersonFactory(), new ResumeSourceInfoFactory(), new ResumeFactory());

            var mergeEngine = new SimpleMergeEngine(rootRepository);
            //TestDb();
            var dataSource = new E1RawDataSources(new BaseConverter(), new HttpDataGetter(), new E1UrlCreator());
            dataSource.Start();

            var counter = 0;

            var mergeTime = new TimeSpan();
            var downloadTime = new TimeSpan();

            while (true)
            {
                var startDownloadTime = DateTime.Now;
                var dataList = dataSource.GetNextDataBlock();
                var endDownloadTime = DateTime.Now;
                downloadTime += endDownloadTime - startDownloadTime;
                if (dataList == null)
                {
                    break;
                }

                foreach (var resumeRaw in dataList)
                {
                    counter++;
                    var startDateMerge = DateTime.Now;
                    mergeEngine.AddResume(resumeRaw, "E1.RU");
                    var endDateMerge = DateTime.Now;
                    mergeTime += endDateMerge - startDateMerge;
                    Console.WriteLine(@"{0} - {1} - {2}", counter, resumeRaw.Fio, endDateMerge - startDateMerge);
                }

            }

            Console.WriteLine(@"DownloadTime - {0}",downloadTime);
            Console.WriteLine(@"MergeTime - {0}", mergeTime);
            Console.WriteLine(@"OK");
            Console.ReadLine();
        }

        private static void TestDb()
        {
            #region

            var sexValues = new List<string> {"М", "Ж"};
            var maritalStatusValues = new List<string> {"+", "-", "?"};
            var resumeSourceValues = new List<string> {"E1"};
            var districtValues = new List<string> {"Центр", "Ботаника", "Уралмаш", "ВИЗ"};
            var educationValues = new List<string> {"Высшее", "Среднее"};
            var experianceLengthValues = new List<string> {"до 5 лет", "более 5 лет"};
            var rubricValues = new List<string> {"ИТ", "Машиностроение"};
            var rubricSpecValues = new List<string> {"Директор", "Уборщик"};
            var scheduleWorkValues = new List<string> {"Круглосуточно", "8 часов"};
            var workTypeValues = new List<string> {"Полная занятость", "Совмещение"};
            var cityValues = new List<string> {"Екб", "Кгн"};

            var rootRepository = new MsAccesRootRepository(Path.GetFullPath("base.mdb"), new DictionaryFactory(),
                new PersonFactory(), new ResumeSourceInfoFactory(), new ResumeFactory());

            var ra = rootRepository.GetReadAcces();

            var sexList = ra.DictionaryRepository.GetAll<ISex>();
            var maritalStatusList = ra.DictionaryRepository.GetAll<IMaritalStatus>();
            var resumeSourceList = ra.DictionaryRepository.GetAll<IResumeSource>();
            var districtList = ra.DictionaryRepository.GetAll<IDistrict>();
            var educationList = ra.DictionaryRepository.GetAll<IEducation>();
            var experianceLengthList = ra.DictionaryRepository.GetAll<IExperienceLength>();
            var rubricList = ra.DictionaryRepository.GetAll<IRubric>();
            var rubricSpecList = ra.DictionaryRepository.GetAll<IRubricSpeciality>();
            var cityList = ra.DictionaryRepository.GetAll<ICity>();

            var scheduleWorkList = ra.DictionaryRepository.GetAll<IScheduleWork>();
            var workTypeList = ra.DictionaryRepository.GetAll<IWorkingType>();

            Console.WriteLine(@"==============ENUMS========");

            using (var wr = rootRepository.GetWriteAcces())
            {
                CheckAndFillDictionary(sexValues, sexList, wr);
                CheckAndFillDictionary(maritalStatusValues, maritalStatusList, wr);

                CheckAndFillDictionary(resumeSourceValues, resumeSourceList, wr);
                CheckAndFillDictionary(districtValues, districtList, wr);

                CheckAndFillDictionary(educationValues, educationList, wr);
                CheckAndFillDictionary(experianceLengthValues, experianceLengthList, wr);
                CheckAndFillDictionary(rubricValues, rubricList, wr);
                CheckAndFillDictionary(rubricSpecValues, rubricSpecList, wr);

                CheckAndFillDictionary(scheduleWorkValues, scheduleWorkList, wr);
                CheckAndFillDictionary(workTypeValues, workTypeList, wr);

                CheckAndFillDictionary(cityValues, cityList, wr);
            }

            Console.WriteLine(@"==============SMALL ENTITY========");

            sexList = ra.DictionaryRepository.GetAll<ISex>();
            maritalStatusList = ra.DictionaryRepository.GetAll<IMaritalStatus>();
            resumeSourceList = ra.DictionaryRepository.GetAll<IResumeSource>();
            districtList = ra.DictionaryRepository.GetAll<IDistrict>();
            educationList = ra.DictionaryRepository.GetAll<IEducation>();
            experianceLengthList = ra.DictionaryRepository.GetAll<IExperienceLength>();
            rubricList = ra.DictionaryRepository.GetAll<IRubric>();
            rubricSpecList = ra.DictionaryRepository.GetAll<IRubricSpeciality>();
            scheduleWorkList = ra.DictionaryRepository.GetAll<IScheduleWork>();
            workTypeList = ra.DictionaryRepository.GetAll<IWorkingType>();
            cityList = ra.DictionaryRepository.GetAll<ICity>();

            var personList = ra.PersonRepository.GetAll();

            if (!personList.Any())
            {
                using (var wr = rootRepository.GetWriteAcces())
                {
                    Console.WriteLine(@"Добавляем Иванов Иван Иванович");
                    wr.PersonRepository.Insert("Иванов Иван Иванович", DateTime.Now, sexList.First(),
                        new List<string> {"1", "2", "3"},
                        false, true, maritalStatusList.First());
                }
            }

            var sourceInfoList = ra.ResumeSourceInfo.GetAll();

            if (!sourceInfoList.Any())
            {
                using (var wr = rootRepository.GetWriteAcces())
                {
                    Console.WriteLine(@"Добавляем ResumeSourceInfo");
                    wr.ResumeSourceInfo.Insert(156, resumeSourceList.First(), "EEEEEEE");
                }
            }

            personList = ra.PersonRepository.GetAll();
            sourceInfoList = ra.ResumeSourceInfo.GetAll();

            Console.WriteLine(@"==============");

            var person = personList.First();

            Console.WriteLine(person.Id);
            Console.WriteLine(person.Fio);
            Console.WriteLine(person.Sex.Name);
            Console.WriteLine(person.Birthday);
            Console.WriteLine(person.IsDriver);
            Console.WriteLine(person.IsSmoke);
            Console.WriteLine(person.MaritalStatus.Name);

            foreach (var photo in person.Photos)
            {
                Console.WriteLine(@"     " + photo);
            }

            Console.WriteLine(@"------------");
            var sourceInfo = sourceInfoList.First();
            Console.WriteLine(sourceInfo.Id);
            Console.WriteLine(sourceInfo.Source.Name);
            Console.WriteLine(sourceInfo.ResumeSourceId);
            Console.WriteLine(sourceInfo.Link);

            Console.WriteLine(@"============== BIG ENTITY =============");

            var resumeList = ra.ResumeRepository.GetAll();

            if (!resumeList.Any())
            {
                using (var wr = rootRepository.GetWriteAcces())
                {
                    Console.WriteLine(@"Добавляем Резюме");
                    wr.ResumeRepository.Insert(person, "Тестовое резюме", DateTime.Now.AddDays(-5),
                        DateTime.Now.AddDays(-1), new List<IDistrict>(districtList), 15000, educationList.First(),
                        "1234", "12342314", experianceLengthList.First(), "", sourceInfo, "", "", rubricList.First(),
                        rubricSpecList, scheduleWorkList.First(), workTypeList.First(), cityList.First());
                }
            }

            resumeList = ra.ResumeRepository.GetAll();
            if (resumeList.Any())
            {
                Console.WriteLine(@"OK");
            }
            else
            {
                Console.WriteLine(@"ERR");
            }

            #endregion
        }

        private static void CheckAndFillDictionary<T>(IEnumerable<string> districtValues, IList<T> districtList, IWriteAcces wr) where T:IDictionaryItem
        {
            foreach (var testValue in districtValues)
            {
                if (districtList.All(t => t.Name != testValue))
                {
                    Console.WriteLine(@"Добавляем {0}",typeof(T).Name);
                    wr.DictionaryRepository.Insert<T>(testValue);
                }
            }
        }

    }
}
