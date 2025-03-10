using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GlobalScaleCustomization : Customization
{
	public float PositionScaleModifier = 1f;
	public float SizeScaleModifier = 1f;

	public GlobalScaleCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-global-settings";

		if(ImGui.TreeNode($"{localization.GlobalScale}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.PositionScaleModifier}##{customizationName}", ref PositionScaleModifier, 0.001f, 0.001f, 10f, "%.3f");
			isChanged |= ImGui.DragFloat($"{localization.SizeScaleModifier}##{customizationName}", ref SizeScaleModifier, 0.001f, 0.001f, 10f, "%.3f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}

