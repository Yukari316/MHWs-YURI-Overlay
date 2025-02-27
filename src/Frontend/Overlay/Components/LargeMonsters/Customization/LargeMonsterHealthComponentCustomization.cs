using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterHealthComponentCustomization : Customization
{
	public bool Visible = true;
	public OffsetCustomization Offset = new();
	public LabelElementCustomization ValueLabel = new();
	public LabelElementCustomization PercentageLabel = new();
	public BarElementCustomization Bar = new();

	public LargeMonsterHealthComponentCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-health";

		if(ImGui.TreeNode($"{localization.Health}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.Visible}##{customizationName}", ref Visible);
			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= ValueLabel.RenderImGui(localization.ValueLabel, customizationName);
			isChanged |= PercentageLabel.RenderImGui(localization.PercentageLabel, customizationName);
			isChanged |= Bar.RenderImGui(localization.Bar, customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
