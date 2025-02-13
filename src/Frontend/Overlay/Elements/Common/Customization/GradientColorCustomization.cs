﻿using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class GradientColorCustomization : Customization
{
	[JsonIgnore]
	public ColorInfo StartInfo { get; set; } = new();
	public string start
	{
		get => StartInfo.RgbaHex;
		set => StartInfo.RgbaHex = value;
	}

	[JsonIgnore]
	public ColorInfo EndInfo { get; set; } = new();
	public string end
	{
		get => EndInfo.RgbaHex;
		set => EndInfo.RgbaHex = value;
	}

	public GradientColorCustomization() { }


	public bool RenderImGui(string parentName = "", string name = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-gradient-color";

		if(ImGui.TreeNode($"{name}##${customizationName}"))
		{
			if(ImGui.TreeNode($"{localization.start}##${customizationName}"))
			{
				var isStartChanged = ImGui.ColorPicker4($"##${customizationName}-start", ref StartInfo.vector);
				isChanged |= isStartChanged;
				if(isStartChanged) StartInfo.Vector = StartInfo.vector;

				ImGui.TreePop();
			}


			if(ImGui.TreeNode($"{localization.end}##${parentName}"))
			{
				var isEndChanged = ImGui.ColorPicker4($"##${customizationName}-end", ref EndInfo.vector);
				isChanged |= isEndChanged;
				if(isEndChanged) EndInfo.Vector = EndInfo.vector;

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		return RenderImGui(parentName, localization.color);
	}
}
