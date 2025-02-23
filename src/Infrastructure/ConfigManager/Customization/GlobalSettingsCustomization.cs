using ImGuiNET;

namespace YURI_Overlay;

internal sealed class GlobalSettingsCustomization : Customization
{
	public float updateDelay = 0.1f;

	public bool calculationCaching = true;

	public GlobalSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-global-settings";

		if(ImGui.TreeNode($"{localization.globalSettings}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.updateDelaySeconds}##{customizationName}", ref updateDelay, 0.001f, 0.001f, 10f, "%.3f");

			isChanged |= ImGui.Checkbox($"{localization.calculationCaching}##{customizationName}", ref calculationCaching);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
