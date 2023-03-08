using System.Net.Http.Headers;
using System.Net.Http;
using System;

namespace eStoreClient2
{
    public static class GobalVariables
    {
        public static HttpClient WebAPIClient = new();

        static GobalVariables()
        {
            WebAPIClient.BaseAddress = new Uri("https://localhost:44337/api/"); // service api gateway
            WebAPIClient.DefaultRequestHeaders.Clear();
            WebAPIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void AddAuthorizationHeader(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
