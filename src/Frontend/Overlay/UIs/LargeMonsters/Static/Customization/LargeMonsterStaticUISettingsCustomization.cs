using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LargeMonsterStaticUISettingsCustomization : Customization
{
	public bool hideDeadOrCaptured = true;
	public bool renderHighlightedMonster = true;
	public bool renderNotHighlightedMonsters = true;

	private int _highlightedMonsterLocationIndex = (int) SortingLocations.Normal;
	[JsonIgnore]
	public OutlineStyles HighlightedMonsterLocationEnum { get => (OutlineStyles) _highlightedMonsterLocationIndex; set => _highlightedMonsterLocationIndex = (int) value; }
	public string highlightedMonsterLocation
	{
		get => LocalizationHelper.Instance.defaultOutlineStyles[_highlightedMonsterLocationIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.defaultOutlineStyles, value);
			if(index != -1) _highlightedMonsterLocationIndex = index;
		}
	}

	public LargeMonsterStaticUISettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.hideDeadOrCaptured}##{customizationName}", ref hideDeadOrCaptured);
			isChanged |= ImGui.Checkbox($"{localization.renderHighlightedMonster}##{customizationName}", ref renderHighlightedMonster);
			isChanged |= ImGui.Checkbox($"{localization.renderNotHighlightedMonsters}##{customizationName}", ref renderNotHighlightedMonsters);

			isChanged |= ImGui.Combo($"{localization.highlightedMonsterLocation}##{customizationName}", ref _highlightedMonsterLocationIndex, localizationHelper.sortingLocations, localizationHelper.sortingLocations.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
