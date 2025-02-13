using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LocalizationWatcher : IDisposable
{
	private readonly FileSystemWatcher _watcher;
	private readonly Dictionary<string, DateTime> _lastEventTimes = new();

	private bool _disabled = false;

	public LocalizationWatcher()
	{
		try
		{
			LogManager.Info("LocalizationWatcher: Initializing...");

			_watcher = new(Constants.LOCALIZATIONS_PATH);

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

			LogManager.Info("LocalizationWatcher: Initialized!");
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
		LogManager.Info("LocalizationWatcher: Disposing...");
		_watcher.Dispose();
		LogManager.Info("LocalizationWatcher: Disposed!");
	}

	private void OnConfigFileChanged(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			string name = Path.GetFileNameWithoutExtension(e.Name);

			DateTime eventTime = File.GetLastWriteTime(e.FullPath);
			if(!_lastEventTimes.ContainsKey(name)) _lastEventTimes[name] = DateTime.MinValue;

			if(_lastEventTimes[name].Ticks - eventTime.Ticks < Constants.DUPLICATE_EVENT_THRESHOLD_TICKS) return;

			LogManager.Info($"Localization \"{name}\": Changed.");

			if(LocalizationManager.Instance.localizations.ContainsKey(name)) return;


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

			LogManager.Info($"Localization \"{name}\": Created.");

			LocalizationManager.Instance.InitializeLocalization(name);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileDeleted(object sender, FileSystemEventArgs e)
	{
		try
		{
			if(_disabled) return;

			string name = Path.GetFileNameWithoutExtension(e.Name);

			LogManager.Info($"Localization \"{name}\": Deleted.");
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

			LogManager.Info($"Localization \"{oldName}\": Renamed to \"{name}\".");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnConfigFileError(object sender, ErrorEventArgs e)
	{
		if(_disabled) return;
		LogManager.Info($"LocalizationWatcher: Unknown error.");
	}
}
