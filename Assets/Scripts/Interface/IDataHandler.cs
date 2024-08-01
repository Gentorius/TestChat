namespace Interface
{
    public interface IDataHandler : IService
    {
        public void SaveData<T>(T data, string filePathEnding);
        public T LoadData<T>(string filePathEnding);
    }
}