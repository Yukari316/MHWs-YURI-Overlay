using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementSettingsCustomization : Customization
{
	[JsonIgnore]
	private int _fillDirectionIndex = (int) FillDirections.LeftToRight;
	[JsonIgnore]
	public FillDirections FillDirectionEnum { get => (FillDirections) _fillDirectionIndex; set => _fillDirectionIndex = (int) value; }
	public string fillDirection
	{
		get => LocalizationHelper.Instance.DefaultOutlineStyles[_fillDirectionIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.DefaultFillDirections, value);
			if(index != -1)
			{
				_fillDirectionIndex = index;
			}
		}
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.Combo($"{localization.fillDirection}##{customizationName}", ref _fillDirectionIndex, localizationHelper.FillDirections, localizationHelper.FillDirections.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
