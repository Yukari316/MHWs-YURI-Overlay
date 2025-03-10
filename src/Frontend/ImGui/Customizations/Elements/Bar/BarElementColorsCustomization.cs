using ImGuiNET;

namespace YURI_Overlay;

internal class BarElementColorsCustomization : Customization
{
	public GradientColorCustomization Foreground = new();
	public GradientColorCustomization Background = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-colors";

		if(ImGui.TreeNode($"{localization.Colors}##{customizationName}"))
		{
			isChanged |= Foreground.RenderImGui(customizationName, localization.Foreground);
			isChanged |= Background.RenderImGui(customizationName, localization.Background);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
