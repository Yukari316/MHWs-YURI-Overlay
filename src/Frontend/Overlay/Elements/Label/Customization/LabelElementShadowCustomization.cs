using ImGuiNET;

namespace YURI_Overlay;

internal class LabelElementShadowCustomization : Customization
{
	public bool Visible = true;
	public OffsetCustomization Offset = new();
	public ColorCustomization Color = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-shadow";

		if(ImGui.TreeNode($"{localization.shadow}##{parentName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{parentName}", ref Visible);

			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= Color.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
