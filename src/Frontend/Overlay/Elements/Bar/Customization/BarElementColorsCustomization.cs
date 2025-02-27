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

		//if(ImGui.TreeNode($"{localization.colors}##{customizationName}"))
		//{
		//	isChanged |= Foreground.RenderImGui(customizationName, localization.foreground);
		//	isChanged |= Background.RenderImGui(customizationName, localization.background);

		//	ImGui.TreePop();
		//}

		return isChanged;
	}
}
