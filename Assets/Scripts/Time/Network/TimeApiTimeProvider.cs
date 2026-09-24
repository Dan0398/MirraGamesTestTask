using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine;
using System;

namespace Dan398.Time.Network
{
    public sealed class TimeApiTimeProvider : IServerTimeProvider
    {
        private const string SyncUrl = "https://timeapi.io/api/Time/current/zone?timeZone=UTC";

        public string SourceName => "timeapi";

        public async Task<DateTime> GetServerTimeAsync()
        {
            using UnityWebRequest request = UnityWebRequest.Get(SyncUrl);
            request.timeout = 5;
            await request.SendWebRequest().AsTask();

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new InvalidOperationException(request.error);
            }

            TimeApiResponse response = JsonUtility.FromJson<TimeApiResponse>(request.downloadHandler.text);
            return response.ToUtcDateTime();
        }

        [Serializable]
        private class TimeApiResponse
        {
            public int year;
            public int month;
            public int day;
            public int hour;
            public int minute;
            public int seconds;
            public int milliSeconds;

            public DateTime ToUtcDateTime()
            {
                return new DateTime(year, month, day, hour, minute, seconds, milliSeconds, DateTimeKind.Utc);
            }
        }
    }
}