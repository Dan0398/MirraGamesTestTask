using System.Threading.Tasks;
using UnityEngine;

namespace Dan398.Time.Network
{
    internal static class AsyncOperationExtensions
    {
        public static Task AsTask(this AsyncOperation operation)
        {
            TaskCompletionSource<bool> completion = new();
            operation.completed += _ => completion.SetResult(true);
            return completion.Task;
        }
    }
}