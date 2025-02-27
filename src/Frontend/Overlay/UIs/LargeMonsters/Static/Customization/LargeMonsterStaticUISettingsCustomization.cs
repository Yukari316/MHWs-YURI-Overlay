using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSettingsCustomization : Customization
{
	public bool HideDeadOrCaptured = true;
	//public bool renderHighlightedMonster = true;
	//public bool renderNotHighlightedMonsters = true;


	public LargeMonsterStaticUiSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.hideDeadOrCaptured}##{customizationName}", ref HideDeadOrCaptured);
			//isChanged |= ImGui.Checkbox($"{localization.renderHighlightedMonster}##{customizationName}", ref renderHighlightedMonster);
			//isChanged |= ImGui.Checkbox($"{localization.renderNotHighlightedMonsters}##{customizationName}", ref renderNotHighlightedMonsters);

			//isChanged |= ImGui.Combo($"{localization.highlightedMonsterLocation}##{customizationName}", ref _highlightedMonsterLocationIndex, localizationHelper.SortingLocations, localizationHelper.SortingLocations.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
