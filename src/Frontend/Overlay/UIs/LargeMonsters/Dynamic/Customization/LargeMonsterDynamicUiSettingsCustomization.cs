using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUiSettingsCustomization : Customization
{
	public bool hideDeadOrCaptured = true;
	public bool renderHighlightedMonster = true;
	public bool renderNotHighlightedMonsters = true;
	public bool opacityFalloff = true;
	public float maxDistance = 3000f;

	public LargeMonsterDynamicUiSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.hideDeadOrCaptured}##{customizationName}", ref hideDeadOrCaptured);
			isChanged |= ImGui.Checkbox($"{localization.renderHighlightedMonster}##{customizationName}", ref renderHighlightedMonster);
			isChanged |= ImGui.Checkbox($"{localization.renderNotHighlightedMonsters}##{customizationName}", ref renderNotHighlightedMonsters);
			isChanged |= ImGui.Checkbox($"{localization.opacityFalloff}##{customizationName}", ref opacityFalloff);
			isChanged |= ImGui.DragFloat($"{localization.maxDistance}##{customizationName}", ref maxDistance, 0.1f, 0, 65536f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
