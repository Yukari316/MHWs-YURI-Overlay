using ImGuiNET;

namespace YURI_Overlay;

internal sealed class WorldOffsetCustomization : Customization
{
	public float X = 0f;
	public float Y = 0f;
	public float Z = 0f;

	public WorldOffsetCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-world-offset";

		if(ImGui.TreeNode($"{localization.worldOffset}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.x}##${customizationName}", ref X, 0.1f, -4096f, 4096f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.y}##${customizationName}", ref Y, 0.1f, -4096f, 4096f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.z}##${customizationName}", ref Z, 0.1f, -4096f, 4096f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
