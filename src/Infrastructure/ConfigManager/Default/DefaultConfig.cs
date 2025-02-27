namespace YURI_Overlay;

internal static class DefaultConfig
{
	public static void ResetTo(JsonDatabase<Config> configDatabase)
	{
		var config = configDatabase.Data;

		config.localization = Constants.DefaultLocalization;

		config.LargeMonsterUI.Dynamic.Enabled = true;
		config.LargeMonsterUI.Dynamic.Settings.HideDeadOrCaptured = true;
		config.LargeMonsterUI.Dynamic.Settings.RenderHighlightedMonster = true;
		config.LargeMonsterUI.Dynamic.Settings.RenderNotHighlightedMonsters = true;
		config.LargeMonsterUI.Dynamic.Settings.OpacityFalloff = true;
		config.LargeMonsterUI.Dynamic.Settings.MaxDistance = 300f;

		config.LargeMonsterUI.Dynamic.WorldOffset.X = 0f;
		config.LargeMonsterUI.Dynamic.WorldOffset.Y = 500f;
		config.LargeMonsterUI.Dynamic.WorldOffset.Z = 0f;

		config.LargeMonsterUI.Dynamic.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Offset.Y = 0f;

		config.LargeMonsterUI.Dynamic.NameLabel.Visible = true;
		config.LargeMonsterUI.Dynamic.NameLabel.Format = "{0}";
		config.LargeMonsterUI.Dynamic.NameLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Dynamic.NameLabel.Offset.X = -96f + 3f;
		config.LargeMonsterUI.Dynamic.NameLabel.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.NameLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Format = "{0:F0}/{1:F0}";
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Settings.RightAlignmentShift = 13;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Offset.X = 4f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Offset.Y = 16f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Offset.X = 49f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Offset.Y = 28f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.Bar.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Dynamic.Health.Bar.Offset.X = -96f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Offset.Y = 16f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Size.Width = 192f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Size.Height = 7f;

		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.StartInfo.Rgba = 0xFFFFFF59;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.EndInfo.Rgba = 0xFFFFFF59;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.StartInfo.Rgba = 0x164D35FF;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.EndInfo.Rgba = 0x75A55EFF;

		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Thickness = 3f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Color.colorInfo.Rgba = 0x00000059;

		config.LargeMonsterUI.Static.Enabled = true;
		config.LargeMonsterUI.Static.Settings.HideDeadOrCaptured = true;
		//config.LargeMonsterUI.@Static.settings.renderHighlightedMonster = true;
		//config.LargeMonsterUI.@Static.settings.renderNotHighlightedMonsters = true;

		config.LargeMonsterUI.Static.Position.x = 89f;
		config.LargeMonsterUI.Static.Position.y = -40f;
		config.LargeMonsterUI.Static.Position.anchor = Anchors.BottomLeft;

		config.LargeMonsterUI.Static.Spacing.X = 320f;
		config.LargeMonsterUI.Static.Spacing.Y = 0f;

		config.LargeMonsterUI.Static.Sorting.Type = Sortings.Name;
		config.LargeMonsterUI.Static.Sorting.ReversedOrder = false;

		config.LargeMonsterUI.Static.NameLabel.Visible = true;
		config.LargeMonsterUI.Static.NameLabel.Format = "{0}";
		config.LargeMonsterUI.Static.NameLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Static.NameLabel.Offset.X = 7f;
		config.LargeMonsterUI.Static.NameLabel.Offset.Y = 0f;
		config.LargeMonsterUI.Static.NameLabel.Color.colorInfo.Rgba = 0xFFFFFFCC;

		config.LargeMonsterUI.Static.NameLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Color.colorInfo.Rgba = 0x000000CC;

		config.LargeMonsterUI.Static.Health.Visible = true;
		config.LargeMonsterUI.Static.Health.Offset.X = 0f;
		config.LargeMonsterUI.Static.Health.Offset.Y = 0f;

		config.LargeMonsterUI.Static.Health.ValueLabel.Visible = true;
		config.LargeMonsterUI.Static.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		config.LargeMonsterUI.Static.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Static.Health.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Offset.Y = 24f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Color.colorInfo.Rgba = 0xFFFFFFCC;

		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Color.colorInfo.Rgba = 0x000000CC;

		config.LargeMonsterUI.Static.Health.PercentageLabel.Visible = true;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Static.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Offset.X = 245f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Offset.Y = 24f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Color.colorInfo.Rgba = 0xFFFFFFCC;

		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Color.colorInfo.Rgba = 0x000000CC;

		config.LargeMonsterUI.Static.Health.Bar.Visible = true;
		config.LargeMonsterUI.Static.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Static.Health.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Offset.Y = 15f;
		config.LargeMonsterUI.Static.Health.Bar.Size.Width = 297f;
		config.LargeMonsterUI.Static.Health.Bar.Size.Height = 12f;

		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.StartInfo.Rgba = 0x004016CC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.EndInfo.Rgba = 0x34FF4ECC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.StartInfo.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.EndInfo.Rgba = 0x0000004A;


		config.LargeMonsterUI.Static.Health.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Color.colorInfo.Rgba = 0x00000080;

		configDatabase.Save();
	}
}
