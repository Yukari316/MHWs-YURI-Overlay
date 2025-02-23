using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LocalizationCustomization : Customization
{
	private int _activeLocalizationIndex = 0;

	private string[] _localizationNames = [];
	private string[] _localizationIsoCodes = [];

	public LocalizationCustomization()
	{
		var localizationManager = LocalizationManager.Instance;

		_localizationNames = localizationManager.Localizations.Values.Select(localization => localization.Data.localizationInfo.name).ToArray();
		_localizationIsoCodes = localizationManager.Localizations.Values.Select(localization => localization.Name).ToArray();

		_activeLocalizationIndex = Array.IndexOf(_localizationIsoCodes, localizationManager.ActiveLocalization.Name);

		localizationManager.ActiveLocalizationChanged += OnActiveLocalizationChanged;
		localizationManager.AnyLocalizationChanged += OnAnyLocalizationChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localizationManager = LocalizationManager.Instance;
		var configManager = ConfigManager.Instance;
		var localization = localizationManager.ActiveLocalization.Data.imGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{localization.language}##{parentName}"))
		{
			var isActiveConfigChanged = ImGui.Combo(localization.activeConfig, ref _activeLocalizationIndex, _localizationNames, _localizationNames.Length);
			if(isActiveConfigChanged)
			{
				isChanged |= isActiveConfigChanged;

				configManager.ActiveConfig.Data.localization = _localizationIsoCodes[_activeLocalizationIndex];
				localizationManager.ActivateLocalization(_localizationIsoCodes[_activeLocalizationIndex]);
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	private void OnAnyLocalizationChanged(object sender, EventArgs eventArgs)
	{
		var localizationManager = LocalizationManager.Instance;

		_localizationNames = localizationManager.Localizations.Values.Select(localization => localization.Data.localizationInfo.name).ToArray();
		_localizationIsoCodes = localizationManager.Localizations.Values.Select(localization => localization.Name).ToArray();
	}

	private void OnActiveLocalizationChanged(object sender, EventArgs eventArgs)
	{
		var localizationManager = LocalizationManager.Instance;

		_activeLocalizationIndex = Array.IndexOf(_localizationIsoCodes, localizationManager.ActiveLocalization.Name);
	}
}
