using ImGuiNET;

namespace YURI_Overlay;

internal class LabelElementSettingsCustomization : Customization
{
	public int RightAlignmentShift = 0;

	public LabelElementSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.Settings}##{customizationName}"))
		{
			isChanged |= ImGui.InputInt($"{localization.RightAlignmentShift}##{customizationName}", ref RightAlignmentShift);

			if(isChanged && RightAlignmentShift < 0)
			{
				RightAlignmentShift = 0;
			}

			ImGui.TreePop();
		}

		return isChanged;
	}
}
