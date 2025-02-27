using ImGuiNET;

namespace YURI_Overlay;

internal sealed class PositionCustomization : Customization
{
	public float X = 0f;
	public float Y = 0f;

	public PositionCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-position";

		if(ImGui.TreeNode($"{localization.Position}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.X}##${customizationName}", ref X, 0.1f, 0f, 8192f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.Y}##${customizationName}", ref Y, 0.1f, 0f, 8192f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
