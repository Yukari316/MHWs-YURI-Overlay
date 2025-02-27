using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUiSettingsCustomization : Customization
{
	public bool HideDeadOrCaptured = true;
	public bool RenderHighlightedMonster = true;
	public bool RenderNotHighlightedMonsters = true;
	public bool OpacityFalloff = true;
	public float MaxDistance = 3000f;

	public LargeMonsterDynamicUiSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.hideDeadOrCaptured}##{customizationName}", ref HideDeadOrCaptured);
			isChanged |= ImGui.Checkbox($"{localization.renderHighlightedMonster}##{customizationName}", ref RenderHighlightedMonster);
			isChanged |= ImGui.Checkbox($"{localization.renderNotHighlightedMonsters}##{customizationName}", ref RenderNotHighlightedMonsters);
			isChanged |= ImGui.Checkbox($"{localization.opacityFalloff}##{customizationName}", ref OpacityFalloff);
			isChanged |= ImGui.DragFloat($"{localization.maxDistance}##{customizationName}", ref MaxDistance, 0.1f, 0, 65536f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
