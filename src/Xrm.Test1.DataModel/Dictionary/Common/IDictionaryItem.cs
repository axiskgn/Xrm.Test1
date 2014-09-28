namespace Xrm.Test1.DataModel.Dictionary.Common
{

    /// <summary>
    /// Базовый элемент словаря
    /// </summary>
    public interface IDictionaryItem
    {
        int Id { get; }
        string Name { get; }  
    }
}