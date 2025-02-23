using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiCustomization : Customization
{
	public bool enabled = true;
	public LargeMonsterStaticUiSettingsCustomization settings = new();
	public AnchoredPositionCustomization position = new();
	public SpacingCustomization spacing = new();
	public LargeMonsterStaticUiSortingCustomization sorting = new();


	public LabelElementCustomization nameLabel = new();
	public LargeMonsterHealthComponentCustomization health = new();


	public LargeMonsterStaticUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-static";

		if(ImGui.TreeNode($"{localization.@static}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.enabled}##{customizationName}", ref enabled);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= position.RenderImGui(customizationName);
			isChanged |= spacing.RenderImGui(customizationName);
			isChanged |= sorting.RenderImGui(customizationName);
			isChanged |= nameLabel.RenderImGui(localization.nameLabel, $"{customizationName}-name-label");
			isChanged |= health.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
