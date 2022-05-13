namespace MauiProgram
{
    public class ApiService : IApiService
    {
        public string GetTestData()
        {
            return $"Hello at {DateTime.Now.ToLongTimeString()}.";
        }
    }
}