using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementCustomization : Customization
{
	public bool Visible = true;
	public BarElementSettingsCustomization Settings = new();
	public OffsetCustomization Offset = new();
	public SizeCustomization Size = new();
	public BarElementColorsCustomization Colors = new();
	public BarElementOutlineCustomization Outline = new();

	public BarElementCustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "bar")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{visibleName}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.Visible}##{customizationName}", ref Visible);

			isChanged |= Settings.RenderImGui(customizationName);
			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= Size.RenderImGui(customizationName);
			isChanged |= Colors.RenderImGui(customizationName);
			isChanged |= Outline.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
