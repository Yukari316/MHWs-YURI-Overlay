using ImGuiNET;

namespace YURI_Overlay;

internal sealed class SpacingCustomization : Customization
{
	public float X = 0f;
	public float Y = 0f;

	public SpacingCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-spacing";

		if(ImGui.TreeNode($"{localization.Spacing}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.X}##${customizationName}", ref X, 0.1f, -4096f, 4096f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.Y}##${customizationName}", ref Y, 0.1f, -4096f, 4096f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
