using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSettingsCustomization : Customization
{
	public bool hideDeadOrCaptured = true;
	public bool renderHighlightedMonster = true;
	public bool renderNotHighlightedMonsters = true;

	private int _highlightedMonsterLocationIndex = (int) SortingLocations.Normal;
	[JsonIgnore]
	public OutlineStyles HighlightedMonsterLocationEnum { get => (OutlineStyles) _highlightedMonsterLocationIndex; set => _highlightedMonsterLocationIndex = (int) value; }
	public string highlightedMonsterLocation
	{
		get => LocalizationHelper.Instance.DefaultOutlineStyles[_highlightedMonsterLocationIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.DefaultOutlineStyles, value);
			if(index != -1)
			{
				_highlightedMonsterLocationIndex = index;
			}
		}
	}

	public LargeMonsterStaticUiSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.hideDeadOrCaptured}##{customizationName}", ref hideDeadOrCaptured);
			isChanged |= ImGui.Checkbox($"{localization.renderHighlightedMonster}##{customizationName}", ref renderHighlightedMonster);
			isChanged |= ImGui.Checkbox($"{localization.renderNotHighlightedMonsters}##{customizationName}", ref renderNotHighlightedMonsters);

			isChanged |= ImGui.Combo($"{localization.highlightedMonsterLocation}##{customizationName}", ref _highlightedMonsterLocationIndex, localizationHelper.SortingLocations, localizationHelper.SortingLocations.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
