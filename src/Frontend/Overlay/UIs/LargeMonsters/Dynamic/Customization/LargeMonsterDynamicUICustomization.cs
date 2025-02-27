using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUiCustomization : Customization
{
	public bool Enabled = true;
	public LargeMonsterDynamicUiSettingsCustomization Settings = new();
	public WorldOffsetCustomization WorldOffset = new();
	public OffsetCustomization Offset = new();

	public LabelElementCustomization NameLabel = new();
	public LargeMonsterHealthComponentCustomization Health = new();


	public LargeMonsterDynamicUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-Dynamic";

		if(ImGui.TreeNode($"{localization.dynamic}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.enabled}##{customizationName}", ref Enabled);

			isChanged |= Settings.RenderImGui(customizationName);
			isChanged |= WorldOffset.RenderImGui(customizationName);
			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= NameLabel.RenderImGui(localization.nameLabel, $"{customizationName}-name-label");
			isChanged |= Health.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
