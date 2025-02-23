using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ConfigCustomization : Customization
{
	private int _activeConfigIndex = 0;
	private string _configNameInput = string.Empty;

	private string[] _configNames = [];

	public ConfigCustomization()
	{
		var configManager = ConfigManager.Instance;

		configManager.ActiveConfigChanged += OnAnyConfigChanged;
		configManager.AnyConfigChanged += OnAnyConfigChanged;

		OnAnyConfigChanged(configManager, EventArgs.Empty);
	}

	public override bool RenderImGui(string parentName = "")
	{
		var configManager = ConfigManager.Instance;
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{localization.config}##{parentName}"))
		{
			var isActiveConfigChanged = ImGui.Combo(localization.activeConfig, ref _activeConfigIndex, _configNames, _configNames.Length);
			if(isActiveConfigChanged)
			{
				isChanged |= isActiveConfigChanged;

				configManager.ActivateConfig(_configNames[_activeConfigIndex]);
			}

			ImGui.InputText($"{localization.newConfigName}##{parentName}", ref _configNameInput, Constants.MaxConfigNameLength);

			if(ImGui.Button($"{localization.@new}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !_configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.NewConfig(_configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.duplicate}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !_configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.DuplicateConfig(_configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.rename}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !_configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.RenameConfig(_configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.reset}##{parentName}"))
			{
				isChanged = true;

				configManager.ResetConfig();
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	private void OnAnyConfigChanged(object sender, EventArgs eventArgs)
	{
		var configManager = ConfigManager.Instance;

		_configNames = configManager.Configs.Values.Select(config => config.Name).ToArray();
		_activeConfigIndex = Array.IndexOf(_configNames, configManager.ActiveConfig.Name);
	}
}
