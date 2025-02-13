using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class LabelElementCustomization : Customization
{
	public bool visible = true;
	public string format = "{0}";
	public LabelElementSettingsCustomization settings = new();
	public OffsetCustomization offset = new();
	public ColorCustomization color = new();
	public LabelElementShadowCustomization shadow = new();

	public LabelElementCustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "label")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;

		if(ImGui.TreeNode($"{visibleName}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref visible);

			LogManager.Info($"[{format}]: {format?.Length}");

			isChanged |= ImGui.InputText($"{localization.format}##{customizationName}", ref format, 256);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= color.RenderImGui(customizationName);
			isChanged |= shadow.RenderImGui(customizationName);
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
