using System.Threading.Tasks;
using System;

namespace Dan398.Time.Network
{
    public interface IServerTimeProvider
    {
        string SourceName { get; }

        Task<DateTime> GetServerTimeAsync();
    }
}