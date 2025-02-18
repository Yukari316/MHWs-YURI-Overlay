using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YURI_Overlay;

internal class ConfigManager : IDisposable
{
	private static readonly Lazy<ConfigManager> _lazy = new(() => new ConfigManager());
	public static ConfigManager Instance => _lazy.Value;

	public ConfigCustomization customization;

	public JsonDatabase<Config> activeConfig;
	public Dictionary<string, JsonDatabase<Config>> configs = [];

	public EventHandler activeConfigChanged = delegate { };
	public EventHandler anyConfigChanged = delegate { };

	public ConfigWatcher configWatcherInstance;

	private JsonDatabase<CurrentConfig> _currentConfigInstance;

	private ConfigManager() {}

	public void Initialize()
	{
		LogManager.Info("ConfigManager: Initializing...");

		LoadAllConfigs();
		LoadCurrentConfig();

		configWatcherInstance = new();
		customization = new();

		LogManager.Info("ConfigManager: Initialized!");
	}

	public void ActivateConfig(JsonDatabase<Config> config)
	{
		LogManager.Info($"ConfigManager: Activating config \"{config.name}\"...");

		activeConfig = config;
		_currentConfigInstance.data.configName = config.name;
		_currentConfigInstance.Save();

		EmitActiveConfigChanged();

		LogManager.Info($"ConfigManager: Config \"{config.name}\" is activated!");
	}

	public void ActivateConfig(string name)
	{
		LogManager.Info($"ConfigManager: Searching for config \"{name}\" to activate it...");

		bool isGetConfigSuccess = configs.TryGetValue(name, out JsonDatabase<Config> config);

		if(!isGetConfigSuccess)
		{
			LogManager.Info($"ConfigManager: Config \"{name}\" is not found. ...");
			LogManager.Info($"ConfigManager: Searching for default config to activate it...");

			try
			{
				LogManager.Info($"ConfigManager: {Utils.Stringify(configs.Count())}");
			}
			catch(Exception exception)
			{
				LogManager.Error(exception);
			}

			bool isGetDefaultConfigSuccess = configs.TryGetValue(Constants.DEFAULT_CONFIG, out JsonDatabase<Config> defaultConfig);

			if(!isGetDefaultConfigSuccess)
			{
				LogManager.Info($"ConfigManager: Default config is not found. Creating it...");

				defaultConfig = new(Constants.CONFIGS_PATH, Constants.DEFAULT_CONFIG);
				DefaultConfig.ResetTo(defaultConfig.data);
				defaultConfig.Save();
				configs[Constants.DEFAULT_CONFIG] = defaultConfig;

				LogManager.Info($"ConfigManager: Default config is created!");

				ActivateConfig(defaultConfig);
				return;
			}

			LogManager.Info($"ConfigManager: Default config is found!");

			ActivateConfig(defaultConfig);
			return;
		}

		LogManager.Info($"ConfigManager: Config \"{name}\" is found!");

		ActivateConfig(config);
	}

	public JsonDatabase<Config> InitializeConfig(string name, Config configToClone = null)
	{
		LogManager.Info($"ConfigManager: Initializing config \"{name}\"...");

		JsonDatabase<Config> config = new(Constants.CONFIGS_PATH, name, configToClone);
		//if(configToClone == null) DefaultConfig.ResetTo(config.data);
		config.Save();

		config.changed += OnConfigFileChanged;
		config.renamedFrom += OnConfigFileRenamedFrom;
		config.renamedTo += OnConfigFileRenamedTo;
		config.deleted += OnConfigFileDeleted;
		config.error += OnConfigFileError;

		configs[name] = config;

		EmitAnyConfigChanged();

		LogManager.Info($"ConfigManager: Config \"{name}\" is initialized!");

		return config;
	}

	public void NewConfig(string newConfigName)
	{
		configWatcherInstance.Disable();
		var newConfig = InitializeConfig(newConfigName);
		configWatcherInstance.DelayedEnable();

		ActivateConfig(newConfig);
	}

	public void DuplicateConfig(string newConfigName)
	{
		configWatcherInstance.Disable();

		JsonDatabase<Config> newConfig = InitializeConfig(newConfigName, activeConfig.data);
		configWatcherInstance.DelayedEnable();

		ActivateConfig(newConfig);
	}

	public void RenameConfig(string newConfigName)
	{
		configWatcherInstance.Disable();

		var oldConfig = activeConfig;
		JsonDatabase<Config> newConfig = InitializeConfig(newConfigName, activeConfig.data);

		ActivateConfig(newConfig);
		configs.Remove(oldConfig.name);
		oldConfig.Delete();

		configWatcherInstance.DelayedEnable();

		Utils.EmitEvents(this, anyConfigChanged);
	}

	public void ResetConfig()
	{
		DefaultConfig.ResetTo(activeConfig.data);
		activeConfig.Save();
	}

	public void Dispose()
	{
		LogManager.Info("ConfigManager: Disposing...");

		configWatcherInstance.Dispose();
		_currentConfigInstance.Dispose();

		foreach(var config in configs)
		{
			config.Value.Dispose();
		}

		LogManager.Info("ConfigManager: Disposed!");
	}

	private void LoadCurrentConfig()
	{
		LogManager.Info("ConfigManager: Loading current config...");

		_currentConfigInstance = new(Constants.PLUGIN_DATA_PATH, Constants.CURRENT_CONFIG);

		_currentConfigInstance.changed += OnCurrentConfigChanged;
		_currentConfigInstance.renamedFrom += OnCurrentConfigRenamedFrom;
		_currentConfigInstance.renamedTo += OnCurrentConfigRenamedTo;
		_currentConfigInstance.deleted += OnCurrentConfigDeleted;
		_currentConfigInstance.error += OnCurrentConfigError;

		ActivateConfig(_currentConfigInstance.data.configName);

		LogManager.Info("ConfigManager: Current config loaded!");
	}

	private void LoadAllConfigs()
	{
		try
		{
			LogManager.Info("ConfigManager: Loading all configs...");

			Directory.CreateDirectory(Path.GetDirectoryName(Constants.CONFIGS_PATH));

			string[] allConfigFilePathNames = Directory.GetFiles(Constants.CONFIGS_PATH);

			if(allConfigFilePathNames.Length == 0)
			{
				InitializeConfig(Constants.DEFAULT_CONFIG);
				return;
			}

			foreach(var configFilePathName in allConfigFilePathNames)
			{
				string name = Path.GetFileNameWithoutExtension(configFilePathName);
				InitializeConfig(name);
			}

			LogManager.Info("ConfigManager: Loading all configs is done!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private void OnCurrentConfigChanged(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Current config file changed.");
		ActivateConfig(_currentConfigInstance.data.configName);
	}

	private void OnCurrentConfigCreated(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Current config file created.");
		_currentConfigInstance.Load();
		ActivateConfig(_currentConfigInstance.data.configName);
	}

	private void OnCurrentConfigRenamedFrom(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Current config file renamed from.");
		_currentConfigInstance.Save();
	}

	private void OnCurrentConfigRenamedTo(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Current config file renamed to.");
		_currentConfigInstance.Load();
		ActivateConfig(_currentConfigInstance.data.configName);
	}

	private void OnCurrentConfigDeleted(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Current config file deleted.");
		_currentConfigInstance.Save();
	}

	private void OnCurrentConfigError(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Current config file throw an error.");
		_currentConfigInstance.Save();
	}

	private void OnConfigFileChanged(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Config file changed.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileCreated(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Config file created.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileRenamedFrom(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Config file renamed from.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileRenamedTo(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Config file renamed to.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileDeleted(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Config file deleted.");
		EmitAnyConfigChanged();
	}

	private void OnConfigFileError(object sender, EventArgs eventArgs)
	{
		LogManager.Info("ConfigManager: Config file throw an error.");
	}

	private void EmitActiveConfigChanged()
	{
		Utils.EmitEvents(this, activeConfigChanged);
	}

	private void EmitAnyConfigChanged()
	{
		Utils.EmitEvents(this, anyConfigChanged);
	}
}
