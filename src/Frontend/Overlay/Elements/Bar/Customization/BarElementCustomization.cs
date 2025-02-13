using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class BarElementCustomization : Customization
{
	public bool visible = true;
	public BarElementSettingsCustomization settings = new();
	public OffsetCustomization offset = new();
	public SizeCustomization size = new();
	public BarElementColorsCustomization colors = new();
	public BarElementOutlineCustomization outline = new();

	public BarElementCustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "bar")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;

		if (ImGui.TreeNode($"{visibleName}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref visible);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= size.RenderImGui(customizationName);
			isChanged |= colors.RenderImGui(customizationName);
			isChanged |= outline.RenderImGui(customizationName);
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
