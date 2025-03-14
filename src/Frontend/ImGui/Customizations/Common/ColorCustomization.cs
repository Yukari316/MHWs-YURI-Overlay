﻿using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal class ColorCustomization : Customization
{
	[JsonIgnore]
	public ColorInfo colorInfo = new();
	public string Color
	{
		get => colorInfo.RgbaHex;
		set => colorInfo.RgbaHex = value;
	}

	public ColorCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-color";

		if(ImGui.TreeNode($"{localization.Color}##${customizationName}"))
		{
			var isColorChanged = ImGui.ColorPicker4($"##${customizationName}", ref colorInfo.vector);
			isChanged |= isColorChanged;
			if(isColorChanged)
			{
				colorInfo.Vector = colorInfo.vector;
			}

			ImGui.TreePop();
		}

		return isChanged;
	}
}
