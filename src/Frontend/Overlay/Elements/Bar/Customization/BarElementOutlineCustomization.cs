using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class BarElementOutlineCustomization : Customization
{
	public bool visible = true;
	public float thickness = 1f;
	public float offset = 0f;

	private int _styleIndex = (int) OutlineStyles.Outside;
	[JsonIgnore]
	public OutlineStyles StyleEnum { get => (OutlineStyles) _styleIndex; set => _styleIndex = (int) value; }
	public string style
	{
		get => LocalizationHelper.Instance.defaultOutlineStyles[_styleIndex];
		set {
			var index = Array.IndexOf(LocalizationHelper.Instance.defaultOutlineStyles, value);
			if(index != -1) _styleIndex = index;
		}
	}

	public ColorCustomization Color { get; set; } = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-outline";

		if(ImGui.TreeNode($"{localization.outline}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref visible);
			isChanged |= ImGui.DragFloat($"{localization.thickness}##{customizationName}", ref thickness, 0.1f, 0, 1024f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.offset}##{customizationName}", ref offset, 0.1f, -1024f, 1024f, "%.1f");

			isChanged |= ImGui.Combo($"{localization.style}##{customizationName}", ref _styleIndex, localizationHelper.outlineStyles, localizationHelper.fillDirections.Length);

			isChanged |= Color.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
