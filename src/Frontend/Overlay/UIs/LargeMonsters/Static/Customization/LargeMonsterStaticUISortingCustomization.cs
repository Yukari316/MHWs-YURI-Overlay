using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterStaticUiSortingCustomization : Customization
{
	private int _typeIndex = (int) Sortings.Name;
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public Sortings Type { get => (Sortings) _typeIndex; set => _typeIndex = (int) value; }

	public bool ReversedOrder = false;

	public LargeMonsterStaticUiSortingCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.sorting}##{customizationName}"))
		{
			isChanged |= ImGui.Combo($"{localization.type}##{customizationName}", ref _typeIndex, localizationHelper.Sortings, localizationHelper.Sortings.Length);

			isChanged |= ImGui.Checkbox($"{localization.reversedOrder}##{customizationName}", ref ReversedOrder);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
