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

	public LocalizationCustomization customization;

	public JsonDatabase<Localization> activeLocalization;
	public JsonDatabase<Localization> defaultLocalization;
	public Dictionary<string, JsonDatabase<Localization>> localizations = [];
	public EventHandler activeLocalizationChanged = delegate { };
	public EventHandler anyLocalizationChanged = delegate { };

	private LocalizationWatcher _localizationWatcherInstance;

	private LocalizationManager() {}

	public void Initialize()
	{
		LogManager.Info("LocalizationManager: Initializing...");

		ConfigManager configManager = ConfigManager.Instance;

		LoadAllLocalizations();
		ActivateLocalization(configManager.activeConfig.data.localization);

		configManager.anyConfigChanged += OnAnyConfigChanged;

		_localizationWatcherInstance = new();
		customization = new();

		LogManager.Info("LocalizationManager: Initialized!");
	}

	public void ActivateLocalization(JsonDatabase<Localization> localization)
	{
		LogManager.Info($"LocalizationManager: Activating localization \"{localization.name}\"...");

		activeLocalization = localization;

		EmitActiveLocalizationChanged();

		LogManager.Info($"LocalizationManager: Localization \"{localization.name}\" is activated!");
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

		newLocalization.changed += OnLocalizationFileChanged;
		newLocalization.renamedFrom += OnLocalizationFileRenamedFrom;
		newLocalization.renamedTo += OnLocalizationFileRenamedTo;
		newLocalization.deleted += OnLocalizationFileDeleted;
		newLocalization.error += OnLocalizationFileError;

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

	private void OnAnyConfigChanged(object sender, EventArgs eventArgs)
	{
		ConfigManager configManager = ConfigManager.Instance;

		if(activeLocalization.name == configManager.activeConfig.data.localization) return;

		ActivateLocalization(configManager.activeConfig.data.localization);
	}

	private void OnLocalizationFileChanged(object sender, EventArgs eventArgs)
	{
		LogManager.Info("LocalizationManager: Localization file changed.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileCreated(object sender, EventArgs eventArgs)
	{
		LogManager.Info("LocalizationManager: Localization file created.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileRenamedFrom(object sender, EventArgs eventArgs)
	{
		LogManager.Info("LocalizationManager: Localization file renamed from.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileRenamedTo(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigMaLocalizationManagernager: Localization file renamed to.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileDeleted(object sender, EventArgs eventArgs)
	{
		LogManager.Info("LocalizationManager: Localization file deleted.");
		EmitAnyLocalizationChanged();
	}

	private void OnLocalizationFileError(object sender, EventArgs eventArgs)
	{
		LogManager.Info("LocalizationManager: Localization file throw an error.");
	}

	private void EmitActiveLocalizationChanged()
	{
		Utils.EmitEvents(this, activeLocalizationChanged);
	}

	private void EmitAnyLocalizationChanged()
	{
		Utils.EmitEvents(this, anyLocalizationChanged);
	}
}
