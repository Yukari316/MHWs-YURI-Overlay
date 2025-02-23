using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUiCustomization : Customization
{
	public bool enabled = true;
	public LargeMonsterDynamicUiSettingsCustomization settings = new();
	public WorldOffsetCustomization worldOffset = new();
	public OffsetCustomization offset = new();

	public LabelElementCustomization nameLabel = new();
	public LargeMonsterHealthComponentCustomization health = new();


	public LargeMonsterDynamicUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dynamic";

		if(ImGui.TreeNode($"{localization.dynamic}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.enabled}##{customizationName}", ref enabled);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= worldOffset.RenderImGui(customizationName);
			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= nameLabel.RenderImGui(localization.nameLabel, $"{customizationName}-name-label");
			isChanged |= health.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
