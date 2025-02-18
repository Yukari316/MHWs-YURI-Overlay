using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class LocalizationCustomization : Customization
{
	private int _activeLocalizationIndex = 0;

	private string[] localizationNames = [];
	private string[] localizationIsoCodes = [];

	public LocalizationCustomization()
	{
		LocalizationManager localizationManager = LocalizationManager.Instance;

		localizationNames = localizationManager.localizations.Values.Select(localization => localization.data.localizationInfo.name).ToArray();
		localizationIsoCodes = localizationManager.localizations.Values.Select(localization => localization.name).ToArray();

		_activeLocalizationIndex = Array.IndexOf(localizationIsoCodes, localizationManager.activeLocalization.name);

		localizationManager.activeLocalizationChanged += OnActiveLocalizationChanged;
		localizationManager.anyLocalizationChanged += OnAnyLocalizationChanged;

	}

	public override bool RenderImGui(string parentName = "")
	{
		LocalizationManager localizationManager = LocalizationManager.Instance;
		ConfigManager configManager = ConfigManager.Instance;
		ImGuiLocalization localization = localizationManager.activeLocalization.data.imGui;

		bool isChanged = false;

		if(ImGui.TreeNode($"{localization.language}##{parentName}"))
		{
			bool isActiveConfigChanged = ImGui.Combo(localization.activeConfig, ref _activeLocalizationIndex, localizationNames, localizationNames.Length);
			if(isActiveConfigChanged)
			{
				isChanged |= isActiveConfigChanged;

				configManager.activeConfig.data.localization = localizationIsoCodes[_activeLocalizationIndex];
				localizationManager.ActivateLocalization(localizationIsoCodes[_activeLocalizationIndex]);
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	private void OnAnyLocalizationChanged(object sender, EventArgs eventArgs)
	{
		LocalizationManager localizationManager = LocalizationManager.Instance;

		LogManager.Info($"LocalizationCustomization: Localizations changed.{Utils.Stringify(localizationManager.localizations.Keys.ToArray())}");

		localizationNames = localizationManager.localizations.Values.Select(localization => localization.data.localizationInfo.name).ToArray();
		localizationIsoCodes = localizationManager.localizations.Values.Select(localization => localization.name).ToArray();
	}

	private void OnActiveLocalizationChanged(object sender, EventArgs eventArgs)
	{
		LocalizationManager localizationManager = LocalizationManager.Instance;

		_activeLocalizationIndex = Array.IndexOf(localizationIsoCodes, localizationManager.activeLocalization.name);
	}
}
