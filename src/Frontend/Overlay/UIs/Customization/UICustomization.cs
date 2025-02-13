using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class UICustomization : Customization
{
	public LargeMonsterUICustomization largeMonsterUI = new();

	public UICustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "ui")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;

		isChanged |= largeMonsterUI.RenderImGui(customizationName);

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
