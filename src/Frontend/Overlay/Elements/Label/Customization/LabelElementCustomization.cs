using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LabelElementCustomization : Customization
{
	public bool visible = true;
	public string format = "{0}";
	public LabelElementSettingsCustomization settings = new();
	public OffsetCustomization offset = new();
	public ColorCustomization color = new();
	public LabelElementShadowCustomization shadow = new();

	public LabelElementCustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "label")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{visibleName}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref visible);

			isChanged |= ImGui.InputText($"{localization.format}##{customizationName}", ref format, 256);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= color.RenderImGui(customizationName);
			isChanged |= shadow.RenderImGui(customizationName);
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
