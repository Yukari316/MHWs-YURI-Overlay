using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterUiCustomization : Customization
{
	public LargeMonsterDynamicUiCustomization dynamic = new();
	public LargeMonsterStaticUiCustomization @static = new();

	public LargeMonsterUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monster";

		if(ImGui.TreeNode($"{localization.largeMonstersUI}##{customizationName}"))
		{
			isChanged |= dynamic.RenderImGui(customizationName);
			isChanged |= @static.RenderImGui(customizationName);

			ImGui.TreePop();
		}


		return isChanged;
	}
}
