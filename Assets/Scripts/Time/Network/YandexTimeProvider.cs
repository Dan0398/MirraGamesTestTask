using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System;

namespace Dan398.Time.Network
{
    public sealed class YandexTimeProvider : IServerTimeProvider
    {
        private const string SyncUrl = "https://yandex.com/time/sync.json";

        public string SourceName => "yandex";

        public async Task<DateTime> GetServerTimeAsync()
        {
            using UnityWebRequest request = UnityWebRequest.Get(SyncUrl);
            await request.SendWebRequest().AsTask();

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new InvalidOperationException(request.error);
            }

            YandexTimeResponse response = JsonUtility.FromJson<YandexTimeResponse>(request.downloadHandler.text);
            return DateTimeOffset.FromUnixTimeMilliseconds(response.time).UtcDateTime;
        }

        [Serializable]
        private class YandexTimeResponse
        {
            public long time;
        }
    }
}