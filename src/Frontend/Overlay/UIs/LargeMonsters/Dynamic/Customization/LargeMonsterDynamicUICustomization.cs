using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUICustomization : Customization
{
	public bool enabled = true;
	public LargeMonsterDynamicUISettingsCustomization settings = new();
	public WorldOffsetCustomization worldOffset = new();
	public OffsetCustomization offset = new();

	public LabelElementCustomization nameLabel = new();
	public LargeMonsterHealthComponentCustomization health = new();


	public LargeMonsterDynamicUICustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-dynamic";

		if(ImGui.TreeNode($"{localization.dynamic}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.enabled}##{customizationName}", ref enabled);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= worldOffset.RenderImGui(customizationName);
			isChanged |= offset.RenderImGui(customizationName);
			isChanged |= nameLabel.RenderImGui(localization.nameLabel, $"{customizationName}-name-label");
			isChanged |= health.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
