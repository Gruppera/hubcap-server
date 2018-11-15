using System;
using System.Net.Http;

namespace Hubcap.TestClient
{
    public static class GameApi
    {
        public static HttpClient Client { get; } = new HttpClient(){Timeout = new TimeSpan(0,2,0,0)};

        static GameApi()
        {
            Client.BaseAddress = new Uri("https://localhost:44345/");
        }
    }
}