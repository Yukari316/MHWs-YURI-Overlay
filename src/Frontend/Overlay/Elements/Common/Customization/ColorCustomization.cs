using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YURI_Overlay;

internal class ColorCustomization : Customization
{
	[JsonIgnore]
	public ColorInfo colorInfo = new();
	public string color
	{
		get => colorInfo.RgbaHex;
		set => colorInfo.RgbaHex = value;
	}

	public ColorCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-color";

		if(ImGui.TreeNode($"{localization.color}##${customizationName}"))
		{
			var isColorChanged = ImGui.ColorPicker4($"##${customizationName}", ref colorInfo.vector);
			isChanged |= isColorChanged;
			if(isColorChanged) colorInfo.Vector = colorInfo.vector;

			ImGui.TreePop();
		}

		return isChanged;
	}
}
