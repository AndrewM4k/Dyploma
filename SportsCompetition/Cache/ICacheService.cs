namespace WebApplication1.Cache
{
    public interface ICacheService
    {
        string GetString(string key);
        T GetValue<T>(string key);
        void SetValue<T>(string key, T value);
        void SetValue(string key, string value);
        void DeleteValue<T>(string key, T value);
        void DeleteValue(string key, string value);
        T UpdateValue<T>(string key);
        string UpdateValue(string key);
    }
}
