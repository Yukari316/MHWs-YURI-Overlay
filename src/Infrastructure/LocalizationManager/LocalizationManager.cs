using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YURI_Overlay;

internal sealed class LocalizationManager : IDisposable
{
	private static readonly Lazy<LocalizationManager> _lazy = new(() => new LocalizationManager());

	public static LocalizationManager Instance => _lazy.Value;

	public JsonDatabase<Localization> activeLocalization;
	public JsonDatabase<Localization> defaultLocalization;
	public Dictionary<string, JsonDatabase<Localization>> localizations = [];
	public EventHandler activeLocalizationChanged = delegate { };

	private LocalizationWatcher _localizationWatcherInstance;

	private LocalizationManager() {}

	public void Initialize()
	{
		LogManager.Info("LocalizationManager: Initializing...");

		LoadAllLocalizations();
		ActivateLocalization(ConfigManager.Instance.activeConfig.data.localization);

		_localizationWatcherInstance = new();

		LogManager.Info("LocalizationManager: Initialized!");
	}

	public void ActivateLocalization(JsonDatabase<Localization> localization)
	{
		LogManager.Info($"LocalizationManager: Activating localization \"{localization.Name}\"...");

		activeLocalization = localization;

		LogManager.Info($"LocalizationManager: Localization \"{localization.Name}\" is activated!");
	}

	public void ActivateLocalization(string name)
	{
		LogManager.Info($"LocalizationManager: Searching for localization \"{name}\" to activate it...");

		bool isGetConfigSuccess = localizations.TryGetValue(name, out JsonDatabase<Localization> localization);

		if(!isGetConfigSuccess)
		{
			LogManager.Info($"LocalizationManager: localization \"{name}\" is not found.");
			LogManager.Info($"LocalizationManager: Activating default localization...");

			ActivateLocalization(defaultLocalization);
			return;
		}

		LogManager.Info($"LocalizationManager: Localization \"{name}\" is found!");

		ActivateLocalization(localization);
	}

	public void InitializeLocalization(string name)
	{
		LogManager.Info($"LocalizationManager: Initializing localization \"{name}\"...");

		JsonDatabase<Localization> newLocalization = new(Constants.LOCALIZATIONS_PATH, name);
		newLocalization.data.isoCode = name;
		newLocalization.Save();
		localizations[name] = newLocalization;

		LogManager.Info($"LocalizationManager: Localization \"{name}\" is intialized!");
	}

	public void Dispose()
	{
		LogManager.Info("LocalizationManager: Disposing...");

		_localizationWatcherInstance.Dispose();

		foreach(var localization in localizations)
		{
			localization.Value.Dispose();
		}

		LogManager.Info("LocalizationManager: Disposed!");
	}

	private void InitializeDefaultLocalization()
	{
		LogManager.Info($"LocalizationManager: Initializing default localization...");

		JsonDatabase<Localization> defaultLocalization = new(Constants.LOCALIZATIONS_PATH, Constants.DEFAULT_LOCALIZATION);
		defaultLocalization.data = new Localization();
		defaultLocalization.Save();
		localizations[Constants.DEFAULT_LOCALIZATION] = defaultLocalization;
		this.defaultLocalization = defaultLocalization;

		LogManager.Info($"LocalizationManager: Default localization is initialized!");
	}

	private void LoadAllLocalizations()
	{
		try
		{
			LogManager.Info("LocalizationManager: Loading all localizations...");

			Directory.CreateDirectory(Path.GetDirectoryName(Constants.LOCALIZATIONS_PATH));

			string[] allConfigFilePathNames = Directory.GetFiles(Constants.LOCALIZATIONS_PATH);

			foreach(var configFilePathName in allConfigFilePathNames)
			{

				string name = Path.GetFileNameWithoutExtension(configFilePathName);

				if(name == Constants.DEFAULT_LOCALIZATION) continue;

				InitializeLocalization(name);
			}

			InitializeDefaultLocalization();

			LogManager.Info("LocalizationManager: Loading all localizations is done!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnActiveLocalizationChanged()
	{
		Utils.EmitEvents(this, activeLocalizationChanged);
	}
}
