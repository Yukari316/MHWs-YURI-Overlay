using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace YURI_Overlay;

internal static class Timers
{
	//public static IDisposable SetInterval(Action method, int delayInMilliseconds)
	//{
	//	var timer = new System.Timers.Timer(delayInMilliseconds);

	//	timer.Elapsed += (source, e) => method();
	//	timer.Enabled = true;
	//	timer.Start();

	//	// Returns a stop handle which can be used for stopping
	//	// the timer, if required
	//	return timer;
	//}

	public static Task SetTimeout(Action method, int delayInMilliseconds)
	{
		return Task.Delay(delayInMilliseconds).ContinueWith((_) => method());

		//var timer = new System.Timers.Timer(delayInMilliseconds);

		//timer.Elapsed += (source, e) => method();
		//timer.AutoReset = false;
		//timer.Enabled = true;
		//timer.Start();

		//// Returns a stop handle which can be used for stopping
		//// the timer, if required
		//return timer;
	}
}