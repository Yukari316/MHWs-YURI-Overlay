﻿using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterDynamicUiSettingsCustomization : Customization
{
	public bool RenderDeadOrCaptured = false;
	public bool RenderHighlightedMonster = true;
	public bool RenderNotHighlightedMonsters = true;

	public bool AddMissionBeaconOffsetToWorldOffset = false;
	public bool AddModelRadiusToWorldOffsetY = true;

	public bool OpacityFalloff = true;
	public float MaxDistance = 3000f;

	public LargeMonsterDynamicUiSettingsCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-settings";

		if(ImGui.TreeNode($"{localization.Settings}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.RenderDeadOrCaptured}##{customizationName}", ref RenderDeadOrCaptured);
			isChanged |= ImGui.Checkbox($"{localization.RenderHighlightedMonster}##{customizationName}", ref RenderHighlightedMonster);
			isChanged |= ImGui.Checkbox($"{localization.RenderNotHighlightedMonsters}##{customizationName}", ref RenderNotHighlightedMonsters);
			isChanged |= ImGui.Checkbox($"{localization.AddMissionBeaconOffsetToWorldOffset}##{customizationName}", ref AddMissionBeaconOffsetToWorldOffset);
			isChanged |= ImGui.Checkbox($"{localization.AddModelRadiusToWorldOffsetY}##{customizationName}", ref AddModelRadiusToWorldOffsetY);
			isChanged |= ImGui.Checkbox($"{localization.OpacityFalloff}##{customizationName}", ref OpacityFalloff);
			isChanged |= ImGui.DragFloat($"{localization.MaxDistance}##{customizationName}", ref MaxDistance, 0.1f, 0, 65536f, "%.1f");

			ImGui.TreePop();
		}

		return isChanged;
	}
}
