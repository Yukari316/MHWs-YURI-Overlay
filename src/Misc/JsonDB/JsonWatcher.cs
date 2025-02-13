using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YURI_Overlay;

internal class JsonWatcher<T> : IDisposable where T : class, new()
{
	private readonly JsonDatabase<T> jsonDatabaseInstance;
	private readonly FileSystemWatcher watcher;

	private bool _disabled = false;
	private DateTime _lastEventTime  = DateTime.MinValue;

	public JsonWatcher(JsonDatabase<T> jsonDatabase)
	{
		try
		{
			LogManager.Info($"ConfigWatcher \"{jsonDatabase.Name}\": Initializing...");

			jsonDatabaseInstance = jsonDatabase;
			watcher = new(jsonDatabase.FilePath);

			watcher.NotifyFilter = NotifyFilters.Attributes
								 | NotifyFilters.CreationTime
								 | NotifyFilters.FileName
								 | NotifyFilters.LastWrite
								 | NotifyFilters.Security
								 | NotifyFilters.Size;

			watcher.Changed += OnJsonFileChanged;
			watcher.Renamed += OnJsonFileRenamed;
			watcher.Deleted += OnJsonFileDeleted;
			watcher.Error += OnJsonFileError;

			watcher.Filter = $"{jsonDatabase.Name}.json";
			watcher.EnableRaisingEvents = true;

			LogManager.Info($"ConfigWatcher \"{jsonDatabase.Name}\": Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Enable()
	{
		_disabled = false;
	}

	public void DelayedEnable()
	{
		Timers.SetTimeout(Enable, Constants.REENABLE_WATCHER_DELAY_MILLISECONDS);
	}

	public void Disable()
	{
		_disabled = true;
	}

	public void Dispose()
	{
		LogManager.Info($"ConfigWatcher \"{jsonDatabaseInstance.Name}\": Disposing...");
		watcher.Dispose();
		LogManager.Info($"ConfigWatcher \"{jsonDatabaseInstance.Name}\": Disposed!");
	}

	private void OnJsonFileChanged(object sender, FileSystemEventArgs e)
	{
		try
		{
			LogManager.Info($"{jsonDatabaseInstance.Name} jhjhk {_disabled} {_lastEventTime.Ticks} {File.GetLastWriteTime(e.FullPath).Ticks} {File.GetLastWriteTime(e.FullPath).Ticks} - {_lastEventTime.Ticks} = {File.GetLastWriteTime(e.FullPath).Ticks - _lastEventTime.Ticks} < {Constants.DUPLICATE_EVENT_THRESHOLD_TICKS}");

			if(_disabled) return;

			DateTime eventTime = File.GetLastWriteTime(e.FullPath);

			if(eventTime.Ticks - _lastEventTime.Ticks < Constants.DUPLICATE_EVENT_THRESHOLD_TICKS) return;


			LogManager.Info($"File \"{jsonDatabaseInstance.Name}\": Changed.");

			jsonDatabaseInstance.Load();
			jsonDatabaseInstance.EmitChanged();

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
			LogManager.Info($"File \"{e.OldName}\": Renamed to \"{e.Name}\".");

			if(e.Name != watcher.Filter) jsonDatabaseInstance.EmitRenamedFrom();
			else jsonDatabaseInstance.EmitRenamedTo();
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
			LogManager.Info($"File \"{jsonDatabaseInstance.Name}\": Deleted.");

			jsonDatabaseInstance.EmitDeleted();
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
			LogManager.Info($"File \"{jsonDatabaseInstance.Name}\": Unknown error.");

			jsonDatabaseInstance.Load();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
		
	}
}
