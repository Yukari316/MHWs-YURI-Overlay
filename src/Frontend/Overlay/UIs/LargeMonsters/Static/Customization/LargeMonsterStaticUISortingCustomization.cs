using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSortingCustomization : Customization
{
	private int _sortingIndex = (int) Sortings.Normal;
	[JsonIgnore]
	public Sortings SortingEnum { get => (Sortings) _sortingIndex; set => _sortingIndex = (int) value; }
	public string sorting
	{
		get => LocalizationHelper.Instance.DefaultSortings[_sortingIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.DefaultSortings, value);
			if(index != -1)
			{
				_sortingIndex = index;
			}
		}
	}

	public bool reversedOrder = false;

	public LargeMonsterStaticUiSortingCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.sorting}##{customizationName}"))
		{
			isChanged |= ImGui.Combo($"{localization.type}##{customizationName}", ref _sortingIndex, localizationHelper.Sortings, localizationHelper.Sortings.Length);

			isChanged |= ImGui.Checkbox($"{localization.reversedOrder}##{customizationName}", ref reversedOrder);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
