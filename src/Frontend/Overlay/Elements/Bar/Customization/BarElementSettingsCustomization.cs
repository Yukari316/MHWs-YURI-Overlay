using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class BarElementSettingsCustomization : Customization
{
	private int _fillDirectionIndex = (int) FillDirections.LeftToRight;
	[JsonIgnore]
	public FillDirections FillDirectionEnum { get => (FillDirections) _fillDirectionIndex; set => _fillDirectionIndex = (int) value; }
	public string fillDirection
	{
		get => LocalizationHelper.Instance.defaultOutlineStyles[_fillDirectionIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.defaultFillDirections, value);
			if(index != -1) _fillDirectionIndex = index;
		}
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.Combo($"{localization.fillDirection}##{customizationName}", ref _fillDirectionIndex, localizationHelper.fillDirections, localizationHelper.fillDirections.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
