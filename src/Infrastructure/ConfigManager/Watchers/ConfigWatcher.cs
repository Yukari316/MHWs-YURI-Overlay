using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YURI_Overlay;

internal class ConfigWatcher : IDisposable
{
	private readonly FileSystemWatcher _watcher;
	private readonly Dictionary<string, DateTime> _lastEventTimes = new();

	private bool _disabled = false;

	public ConfigWatcher()
	{
		try
		{
			LogManager.Info("ConfigWatcher: Initializing...");

			_watcher = new(Constants.CONFIGS_PATH);

			_watcher.NotifyFilter = NotifyFilters.Attributes
								 | NotifyFilters.CreationTime
								 | NotifyFilters.FileName
								 | NotifyFilters.LastWrite
								 | NotifyFilters.Security
								 | NotifyFilters.Size;

			_watcher.Changed += OnConfigFileChanged;
			_watcher.Created += OnConfigFileCreated;
			_watcher.Renamed += OnConfigFileRenamed;
			_watcher.Deleted += OnConfigFileDeleted;
			_watcher.Error += OnConfigFileError;

			_watcher.Filter = "*.json";
			_watcher.EnableRaisingEvents = true;

			LogManager.Info("ConfigWatcher: Initialized!");
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
		LogManager.Info("ConfigWatcher: Disposing...");
		_watcher.Dispose();
		LogManager.Info("ConfigWatcher: Disposed!");
	}

	private void OnConfigFileChanged(object sender, FileSystemEventArgs e)
	{
		LogManager.Info($"Config \"{e.Name}\": Changed.");

		try
		{
			if(_disabled) return;

			string name = Path.GetFileNameWithoutExtension(e.Name);

			DateTime eventTime = File.GetLastWriteTime(e.FullPath);

			if(!_lastEventTimes.ContainsKey(name))
			{
				_lastEventTimes[name] = eventTime;
				LogManager.Info($"Config \"{name}\": Changed.");
				return;
			}

			DateTime lastEventTime = _lastEventTimes[name];

			if(eventTime.Ticks - lastEventTime.Ticks < Constants.DUPLICATE_EVENT_THRESHOLD_TICKS) return;

			LogManager.Info($"Config \"{name}\": Changed.");

			_lastEventTimes[name] = eventTime;
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileCreated(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			string name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{name}\": Created.");

			ConfigManager.Instance.InitializeConfig(name);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileDeleted(object sender, FileSystemEventArgs e)
	{
		try {
			if(_disabled) return;

			string name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{name}\": Deleted.");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
		
	}

	private void OnConfigFileRenamed(object sender, RenamedEventArgs e)
	{
		try
		{
			if(_disabled) return;

			string oldName = Path.GetFileNameWithoutExtension(e.OldName);
			string name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Config \"{oldName}\": Renamed to \"{name}\".");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileError(object sender, ErrorEventArgs e)
	{
		if(_disabled) return;
		LogManager.Info($"ConfigWatcher: Unknown error.");
	}
}
