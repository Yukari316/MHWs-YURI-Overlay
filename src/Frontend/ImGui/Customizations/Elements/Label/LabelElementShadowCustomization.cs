using ImGuiNET;

namespace YURI_Overlay;

internal class LabelElementShadowCustomization : Customization
{
	public bool Visible = true;
	public OffsetCustomization Offset = new();
	public ColorCustomization Color = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-shadow";

		if(ImGui.TreeNode($"{localization.Shadow}##{parentName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.Visible}##{parentName}", ref Visible);

			isChanged |= Offset.RenderImGui(customizationName);
			isChanged |= Color.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
