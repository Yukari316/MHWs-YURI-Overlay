namespace YURI_Overlay;

internal static class Timers
{
	public static System.Timers.Timer SetInterval(Action method, int delayInMilliseconds)
	{
		System.Timers.Timer timer = new(delayInMilliseconds);

		timer.Elapsed += (source, eventArgs) => method();
		timer.Enabled = true;
		timer.Start();

		// Returns a stop handle which can be used for stopping
		// the timer, if required
		return timer;
	}

	public static System.Timers.Timer SetTimeout(Action method, int delayInMilliseconds)
	{
		//return Task.Delay(delayInMilliseconds).ContinueWith((_) => method());

		System.Timers.Timer timer = new(delayInMilliseconds);

		timer.Elapsed += (source, eventArgs) => method();
		timer.AutoReset = false;
		timer.Enabled = true;
		timer.Start();

		// Returns a stop handle which can be used for stopping
		// the timer, if required
		return timer;
	}
}