using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterUiCustomization : Customization
{
	public LargeMonsterDynamicUiCustomization Dynamic = new();
	public LargeMonsterStaticUiCustomization Static = new();

	public LargeMonsterUiCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monster";

		if(ImGui.TreeNode($"{localization.LargeMonstersUi}##{customizationName}"))
		{
			//isChanged |= Dynamic.RenderImGui(customizationName);
			isChanged |= Static.RenderImGui(customizationName);

			ImGui.TreePop();
		}


		return isChanged;
	}
}
