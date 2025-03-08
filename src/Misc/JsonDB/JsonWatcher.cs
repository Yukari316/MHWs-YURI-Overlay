namespace YURI_Overlay;

internal partial class JsonWatcher<T> : IDisposable where T : class, new()
{
	private readonly JsonDatabase<T> JsonDatabaseInstance;
	private readonly FileSystemWatcher Watcher;

	private bool _disabled = false;
	private DateTime _lastEventTime = DateTime.MinValue;

	public JsonWatcher(JsonDatabase<T> jsonDatabase)
	{
		try
		{
			LogManager.Info($"[JsonWatcher] \"{jsonDatabase.Name}\": Initializing...");

			JsonDatabaseInstance = jsonDatabase;
			Watcher = new FileSystemWatcher(jsonDatabase.FilePath);

			Watcher.NotifyFilter = NotifyFilters.Attributes
								 | NotifyFilters.CreationTime
								 | NotifyFilters.FileName
								 | NotifyFilters.LastWrite
								 | NotifyFilters.Security
								 | NotifyFilters.Size;

			Watcher.Changed += OnJsonFileChanged;
			Watcher.Renamed += OnJsonFileRenamed;
			Watcher.Deleted += OnJsonFileDeleted;
			Watcher.Error += OnJsonFileError;

			Watcher.Filter = $"{jsonDatabase.Name}.json";
			Watcher.EnableRaisingEvents = true;

			LogManager.Info($"[JsonWatcher] \"{jsonDatabase.Name}\": Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	~JsonWatcher()
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
		LogManager.Info($"[JsonWatcher] \"{JsonDatabaseInstance.Name}\": Disposing...");

		Watcher.Dispose();

		LogManager.Info($"[JsonWatcher] \"{JsonDatabaseInstance.Name}\": Disposed!");
	}

	private void OnJsonFileChanged(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			var eventTime = File.GetLastWriteTime(e.FullPath);

			if(eventTime.Ticks - _lastEventTime.Ticks < Constants.DuplicateEventThresholdTicks)
			{
				return;
			}

			LogManager.Info($"File \"{JsonDatabaseInstance.Name}\": Changed.");

			JsonDatabaseInstance.Load();
			JsonDatabaseInstance.EmitChanged();

			_lastEventTime = eventTime;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	// This never gets called because the file is created before the watcher

	//private void OnJsonFileCreated(object sender, FileSystemEventArgs e)
	//{
	//	try
	//	{
	//		if(_disabled) return;
	//		LogManager.Info($"File \"{jsonDatabaseInstance.Name}\": Created.");

	//		jsonDatabaseInstance.Load();
	//		jsonDatabaseInstance.EmitCreated();
	//	}
	//	catch(Exception exception)
	//	{
	//		LogManager.Error(exception);
	//	}
	//}

	private void OnJsonFileRenamed(object sender, RenamedEventArgs e)
	{
		try
		{
			if(_disabled) return;

			LogManager.Info($"[JsonWatcher] File \"{e.OldName}\": Renamed to \"{e.Name}\".");

			if(e.Name != Watcher.Filter)
			{
				JsonDatabaseInstance.EmitRenamedFrom();
			}
			else
			{
				JsonDatabaseInstance.EmitRenamedTo();
			}
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnJsonFileDeleted(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			LogManager.Info($"[JsonWatcher] File \"{JsonDatabaseInstance.Name}\": Deleted.");

			JsonDatabaseInstance.EmitDeleted();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnJsonFileError(object sender, ErrorEventArgs e)
	{
		try
		{
			if(_disabled) return;

			LogManager.Info($"[JsonWatcher] File \"{JsonDatabaseInstance.Name}\": Unknown error.");

			JsonDatabaseInstance.Load();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
