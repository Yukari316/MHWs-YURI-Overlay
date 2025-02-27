using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LabelElementCustomization : Customization
{
	public bool Visible = true;
	public string Format = "{0}";
	public LabelElementSettingsCustomization Settings = new();
	public OffsetCustomization Offset = new();
	public ColorCustomization Color = new();
	public LabelElementShadowCustomization Shadow = new();

	public LabelElementCustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "label")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{visibleName}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref Visible);

			isChanged |= ImGui.InputText($"{localization.format}##{customizationName}", ref Format, 256);

			isChanged |= Settings.RenderImGui(customizationName);
			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= Color.RenderImGui(customizationName);
			isChanged |= Shadow.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
