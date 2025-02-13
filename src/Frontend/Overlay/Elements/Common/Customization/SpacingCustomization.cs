﻿using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class SpacingCustomization : Customization
{
	public float x = 0f;
	public float y = 0f;

	public SpacingCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-spacing";

		if(ImGui.TreeNode($"{localization.spacing}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.x}##${customizationName}", ref x, 0.1f, -4096f, 4096f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.y}##${customizationName}", ref y, 0.1f, -4096f, 4096f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
