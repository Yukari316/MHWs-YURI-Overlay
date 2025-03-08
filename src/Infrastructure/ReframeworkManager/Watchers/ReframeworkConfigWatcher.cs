namespace YURI_Overlay;
internal partial class ReframeworkConfigWatcher : IDisposable
{
	private readonly FileSystemWatcher _watcher;
	private DateTime _lastEventTime = DateTime.MinValue;

	private bool _disabled = false;

	public ReframeworkConfigWatcher()
	{
		try
		{
			LogManager.Info("[ReframeworkConfigWatcher] Initializing...");

			_watcher = new FileSystemWatcher(@".");

			_watcher.NotifyFilter = NotifyFilters.Attributes
								 | NotifyFilters.CreationTime
								 | NotifyFilters.FileName
								 | NotifyFilters.LastWrite
								 | NotifyFilters.Security
								 | NotifyFilters.Size;

			_watcher.Changed += OnReframeworkConfigChanged;
			_watcher.Created += OnReframeworkConfigCreated;
			_watcher.Renamed += OnReframeworkConfigRenamed;
			_watcher.Deleted += OnReframeworkConfigDeleted;
			_watcher.Error += OnReframeworkConfigError;

			LogManager.Info("[ReframeworkConfigWatcher] Watching " + _watcher.Path);

			_watcher.Filter = Constants.ReframeworkConfigWithExtension;
			_watcher.EnableRaisingEvents = true;

			LogManager.Info("[ReframeworkConfigWatcher] Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~ReframeworkConfigWatcher()
	{
		Dispose();
	}

	public void Enable()
	{
		_disabled = false;
	}

	public void DelayedEnable()
	{
		Timers.SetTimeout(Enable, Constants.ReenableWatcherDelayMilliseconds);
	}

	public void Disable()
	{
		_disabled = true;
	}

	public void Dispose()
	{
		LogManager.Info("[ReframeworkConfigWatcher] Disposing...");

		_watcher.Dispose();

		LogManager.Info("[ReframeworkConfigWatcher] Disposed!");
	}

	private void OnReframeworkConfigChanged(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);
			if(name == null)
			{
				return;
			}

			var eventTime = File.GetLastWriteTime(e.FullPath);

			if(eventTime.Ticks - _lastEventTime.Ticks < Constants.DuplicateEventThresholdTicks)
			{
				return;
			}

			ReframeworkManager.Instance.ReadReframeworkConfig();
			_lastEventTime = eventTime;

			LogManager.Info($"[ReframeworkConfigWatcher] \"{name}\": Changed.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnReframeworkConfigCreated(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);

			ReframeworkManager.Instance.ReadReframeworkConfig();

			LogManager.Info($"[ReframeworkConfigWatcher] \"{name}\": Created.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnReframeworkConfigDeleted(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"[ReframeworkConfigWatcher] \"{name}\": Deleted.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnReframeworkConfigRenamed(object sender, RenamedEventArgs e)
	{
		try
		{
			if(_disabled) return;


			var oldName = Path.GetFileNameWithoutExtension(e.OldName);
			var name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"[ReframeworkConfigWatcher] \"{oldName}\": Renamed to \"{name}\".");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnReframeworkConfigError(object sender, ErrorEventArgs e)
	{
		if(_disabled) return;

		LogManager.Info("[ReframeworkConfigWatcher] Unknown error.");
	}
}
