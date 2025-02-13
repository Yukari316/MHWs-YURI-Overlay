using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LargeMonsterStaticUICustomization : Customization
{
	public bool enabled = true;
	public LargeMonsterStaticUISettingsCustomization settings = new();
	public AnchoredPositionCustomization position = new();
	public SpacingCustomization spacing = new();
	public LargeMonsterStaticUISortingCustomization sorting = new();


	public LabelElementCustomization nameLabel = new();
	public LargeMonsterHealthComponentCustomization health = new();


	public LargeMonsterStaticUICustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-static";

		if(ImGui.TreeNode($"{localization.@static}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.enabled}##{customizationName}", ref enabled);

			isChanged |= settings.RenderImGui(customizationName);
			isChanged |= position.RenderImGui(customizationName);
			isChanged |= spacing.RenderImGui(customizationName);
			isChanged |= sorting.RenderImGui(customizationName);
			isChanged |= nameLabel.RenderImGui(localization.nameLabel, $"{customizationName}-name-label");
			isChanged |= health.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
