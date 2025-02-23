using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PositionCustomization : Customization
{
	public float x = 0f;
	public float y = 0f;

	public PositionCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-position";

		if(ImGui.TreeNode($"{localization.position}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.x}##${customizationName}", ref x, 0.1f, 0f, 8192f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.y}##${customizationName}", ref y, 0.1f, 0f, 8192f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
