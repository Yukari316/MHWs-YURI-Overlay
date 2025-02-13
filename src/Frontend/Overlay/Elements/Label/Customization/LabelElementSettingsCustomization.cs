using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LabelElementSettingsCustomization : Customization
{
	public int rightAlignmentShift = 0;

	public LabelElementSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.InputInt($"{localization.rightAlignmentShift}##{customizationName}", ref rightAlignmentShift);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
