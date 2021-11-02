using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DiscordWatchBot.Common.Utils
{
    public static class ValueTupleExtensions
    {
	    public static TaskAwaiter<(T1 First, T2 Second)> GetAwaiter<T1, T2>(this (Task<T1> firstTask, Task<T2> secondTask) tuple)
	    {
		    var (firstTask, secondTask) = tuple;

		    return Task.WhenAll(firstTask, secondTask)
			    .ContinueWith(_ => (firstTask.Result, secondTask.Result))
			    .GetAwaiter();
	    }
    }
}
