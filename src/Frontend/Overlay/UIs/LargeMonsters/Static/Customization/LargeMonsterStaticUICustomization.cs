using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiCustomization : Customization
{
	public bool Enabled = true;
	public LargeMonsterStaticUiSettingsCustomization Settings = new();
	public AnchoredPositionCustomization Position = new();
	public SpacingCustomization Spacing = new();
	public LargeMonsterStaticUiSortingCustomization Sorting = new();


	public LabelElementCustomization NameLabel = new();
	public LargeMonsterHealthComponentCustomization Health = new();


	public LargeMonsterStaticUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-Static";

		if(ImGui.TreeNode($"{localization.@static}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.enabled}##{customizationName}", ref Enabled);

			isChanged |= Settings.RenderImGui(customizationName);
			isChanged |= Position.RenderImGui(customizationName);
			isChanged |= Spacing.RenderImGui(customizationName);
			isChanged |= Sorting.RenderImGui(customizationName);
			isChanged |= NameLabel.RenderImGui(localization.nameLabel, $"{customizationName}-name-label");
			isChanged |= Health.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
