namespace MauiProgram
{
    public class ApiService : IApiService
    {
        public string GetTestData()
        {
            return @"Test data simulating an API call.";
        }
    }
}