using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class ConfigCustomization: Customization
{
	private int _activeConfigIndex = 0;
	private string _configNameInput = string.Empty;

	private string[] configNames = [];

	public ConfigCustomization()
	{

		ConfigManager configManager = ConfigManager.Instance;
		configManager.activeConfigChanged += OnAnyConfigChanged;
		configManager.anyConfigChanged += OnAnyConfigChanged;

		OnAnyConfigChanged(configManager, EventArgs.Empty);
	}

	public override bool RenderImGui(string parentName = "")
	{
		ConfigManager configManager = ConfigManager.Instance;
		ImGuiLocalization localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		bool isChanged = false;

		if(ImGui.TreeNode($"{localization.config}##{parentName}"))
		{
			bool isActiveConfigChanged = ImGui.Combo(localization.activeConfig, ref _activeConfigIndex, configNames, configNames.Length);
			if(isActiveConfigChanged)
			{
				isChanged |= isActiveConfigChanged;

				configManager.ActivateConfig(configNames[_activeConfigIndex]);
			}
			
			ImGui.InputText($"{localization.newConfigName}##{parentName}", ref _configNameInput, Constants.MAX_CONFIG_NAME_LENGTH);

			if(ImGui.Button($"{localization.@new}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.NewConfig(_configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.duplicate}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !configNames.Contains(_configNameInput))
				{
					isChanged = true;

					configManager.DuplicateConfig(_configNameInput);
				}
			}

			ImGui.SameLine();

			if(ImGui.Button($"{localization.rename}##{parentName}"))
			{
				if(_configNameInput != string.Empty && !configNames.Contains(_configNameInput))
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
		ConfigManager configManager = ConfigManager.Instance;

		LogManager.Info($"ConfigCustomization: Config changed.{Utils.Stringify(configManager.configs.Keys.ToArray())}");

		configNames = configManager.configs.Values.Select(config => config.Name).ToArray();
		_activeConfigIndex = Array.IndexOf(configNames, configManager.activeConfig.Name);
	}
}
