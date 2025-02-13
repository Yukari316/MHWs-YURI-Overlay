﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal static class DefaultConfig
{
	public static void ResetTo(Config config)
	{
		config.localization = Constants.DEFAULT_LOCALIZATION;

		config.largeMonsterUI.dynamic.enabled = true;
		config.largeMonsterUI.dynamic.settings.hideDeadOrCaptured = true;
		config.largeMonsterUI.dynamic.settings.renderHighlightedMonster = true;
		config.largeMonsterUI.dynamic.settings.renderNotHighlightedMonsters = true;
		config.largeMonsterUI.dynamic.settings.opacityFalloff = true;
		config.largeMonsterUI.dynamic.settings.maxDistance = 300f;

		config.largeMonsterUI.dynamic.worldOffset.x = 0f;
		config.largeMonsterUI.dynamic.worldOffset.y = 500f;
		config.largeMonsterUI.dynamic.worldOffset.z = 0f;

		config.largeMonsterUI.dynamic.offset.x = 0f;
		config.largeMonsterUI.dynamic.offset.y = 0f;

		config.largeMonsterUI.dynamic.nameLabel.visible = true;
		config.largeMonsterUI.dynamic.nameLabel.format = "{0}";
		config.largeMonsterUI.dynamic.nameLabel.settings.rightAlignmentShift = 0;
		config.largeMonsterUI.dynamic.nameLabel.offset.x = -96f + 3f;
		config.largeMonsterUI.dynamic.nameLabel.offset.y = 0f;
		config.largeMonsterUI.dynamic.nameLabel.color.colorInfo.Rgba = 0xFFFFFFFF;

		config.largeMonsterUI.dynamic.nameLabel.shadow.visible = true;
		config.largeMonsterUI.dynamic.nameLabel.shadow.offset.x = 2f;
		config.largeMonsterUI.dynamic.nameLabel.shadow.offset.y = 2f;
		config.largeMonsterUI.dynamic.nameLabel.shadow.color.colorInfo.Rgba = 0x000000FF;

		config.largeMonsterUI.dynamic.health.healthValueLabel.visible = true;
		config.largeMonsterUI.dynamic.health.healthValueLabel.format = "{0:F0}/{1:F0}";
		config.largeMonsterUI.dynamic.health.healthValueLabel.settings.rightAlignmentShift = 13;
		config.largeMonsterUI.dynamic.health.healthValueLabel.offset.x = 4f;
		config.largeMonsterUI.dynamic.health.healthValueLabel.offset.y = 16f;
		config.largeMonsterUI.dynamic.health.healthValueLabel.color.colorInfo.Rgba = 0xFFFFFFFF;

		config.largeMonsterUI.dynamic.health.healthValueLabel.shadow.visible = true;
		config.largeMonsterUI.dynamic.health.healthValueLabel.shadow.offset.x = 2f;
		config.largeMonsterUI.dynamic.health.healthValueLabel.shadow.offset.y = 2f;
		config.largeMonsterUI.dynamic.health.healthValueLabel.shadow.color.colorInfo.Rgba = 0x000000FF;

		config.largeMonsterUI.dynamic.health.healthPercentageLabel.visible = false;
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.format = "{0:P1}";
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.settings.rightAlignmentShift =6;
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.offset.x = 49f;
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.offset.y = 28f;
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.color.colorInfo.Rgba = 0xFFFFFFFF;

		config.largeMonsterUI.dynamic.health.healthPercentageLabel.shadow.visible = true;
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.shadow.offset.x = 2f;
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.shadow.offset.y = 2f;
		config.largeMonsterUI.dynamic.health.healthPercentageLabel.shadow.color.colorInfo.Rgba = 0x000000FF;

		config.largeMonsterUI.dynamic.health.healthBar.visible = true;
		config.largeMonsterUI.dynamic.health.healthBar.settings.fillDirection = "LeftToRight";
		config.largeMonsterUI.dynamic.health.healthBar.offset.x = -96f;
		config.largeMonsterUI.dynamic.health.healthBar.offset.y = 16f;
		config.largeMonsterUI.dynamic.health.healthBar.size.width = 192f;
		config.largeMonsterUI.dynamic.health.healthBar.size.height = 7f;

		config.largeMonsterUI.dynamic.health.healthBar.colors.background.StartInfo.Rgba = 0xFFFFFF59;
		config.largeMonsterUI.dynamic.health.healthBar.colors.background.EndInfo.Rgba = 0xFFFFFF59;
		config.largeMonsterUI.dynamic.health.healthBar.colors.foreground.StartInfo.Rgba = 0x164D35FF;
		config.largeMonsterUI.dynamic.health.healthBar.colors.foreground.EndInfo.Rgba = 0x75A55EFF;

		config.largeMonsterUI.dynamic.health.healthBar.outline.visible = true;
		config.largeMonsterUI.dynamic.health.healthBar.outline.thickness = 3f;
		config.largeMonsterUI.dynamic.health.healthBar.outline.offset = 0f;
		config.largeMonsterUI.dynamic.health.healthBar.outline.style = "Outside";
		config.largeMonsterUI.dynamic.health.healthBar.outline.Color.colorInfo.Rgba = 0x00000059;
	}
}
