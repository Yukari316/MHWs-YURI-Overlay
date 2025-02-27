﻿using ImGuiNET;

namespace YURI_Overlay;

internal class LabelElementSettingsCustomization : Customization
{
	public int RightAlignmentShift = 0;

	public LabelElementSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.InputInt($"{localization.rightAlignmentShift}##{customizationName}", ref RightAlignmentShift);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
