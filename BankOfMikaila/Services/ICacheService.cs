namespace BankOfMikaila.Services
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        bool AddData<T>(string key, T value);
        object RemoveData(string key);

        object Invalidate(string key);

        
    }
}
