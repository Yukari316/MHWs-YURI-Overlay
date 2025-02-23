using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementCustomization : Customization
{
	public bool visible = true;
	public BarElementSettingsCustomization settings = new();
	public OffsetCustomization offset = new();
	public SizeCustomization size = new();
	public BarElementColorsCustomization colors = new();
	public BarElementOutlineCustomization outline = new();

	public BarElementCustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "bar")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{visibleName}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref visible);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= size.RenderImGui(customizationName);
			isChanged |= colors.RenderImGui(customizationName);
			isChanged |= outline.RenderImGui(customizationName);
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
