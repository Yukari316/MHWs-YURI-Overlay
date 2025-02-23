using ImGuiNET;

namespace YURI_Overlay;

internal class BarElementColorsCustomization : Customization
{
	public GradientColorCustomization foreground = new();
	public GradientColorCustomization background = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-colors";

		if(ImGui.TreeNode($"{localization.colors}##{customizationName}"))
		{
			isChanged |= foreground.RenderImGui(customizationName, localization.foreground);
			isChanged |= background.RenderImGui(customizationName, localization.background);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
