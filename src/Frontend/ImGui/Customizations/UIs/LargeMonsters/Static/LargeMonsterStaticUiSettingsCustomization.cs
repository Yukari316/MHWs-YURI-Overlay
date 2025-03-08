using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSettingsCustomization : Customization
{
	public bool RenderDeadOrCaptured = false;
	public bool RenderHighlightedMonster = true;
	public bool RenderNotHighlightedMonsters = true;


	public LargeMonsterStaticUiSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.Settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.RenderDeadOrCaptured}##{customizationName}", ref RenderDeadOrCaptured);
			//isChanged |= ImGui.Checkbox($"{localization.renderHighlightedMonster}##{customizationName}", ref renderHighlightedMonster);
			//isChanged |= ImGui.Checkbox($"{localization.renderNotHighlightedMonsters}##{customizationName}", ref renderNotHighlightedMonsters);

			//isChanged |= ImGui.Combo($"{localization.highlightedMonsterLocation}##{customizationName}", ref _highlightedMonsterLocationIndex, localizationHelper.SortingLocations, localizationHelper.SortingLocations.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
