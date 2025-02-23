using ImGuiNET;

namespace YURI_Overlay;

internal sealed class SizeCustomization : Customization
{
	public float width = 100f;
	public float height = 5f;

	public SizeCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-size";

		if(ImGui.TreeNode($"{localization.size}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.width}##${customizationName}", ref width, 0.1f, -8192f, 8192f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.height}##${customizationName}", ref height, 0.1f, -8192f, 8192f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}