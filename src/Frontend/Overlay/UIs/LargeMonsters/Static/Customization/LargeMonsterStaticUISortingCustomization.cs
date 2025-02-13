using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LargeMonsterStaticUISortingCustomization : Customization
{
	private int _sortingIndex = (int) Sortings.Normal;
	[JsonIgnore]
	public Sortings SortingEnum { get => (Sortings) _sortingIndex; set => _sortingIndex = (int) value; }
	public string sorting
	{
		get => LocalizationHelper.Instance.defaultSortings[_sortingIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.defaultSortings, value);
			if(index != -1) _sortingIndex = index;
		}
	}

	public bool reversedOrder = false;

	public LargeMonsterStaticUISortingCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.sorting}##{customizationName}"))
		{
			isChanged |= ImGui.Combo($"{localization.type}##{customizationName}", ref _sortingIndex, localizationHelper.sortings, localizationHelper.sortings.Length);

			isChanged |= ImGui.Checkbox($"{localization.reversedOrder}##{customizationName}", ref reversedOrder);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
