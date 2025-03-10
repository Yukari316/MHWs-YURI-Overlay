namespace YURI_Overlay;

internal static class DefaultConfig
{
	public static void ResetTo(JsonDatabase<Config> configDatabase)
	{
		var config = configDatabase.Data;

		config.GlobalSettings.Localization = Constants.DefaultLocalization;

		config.GlobalSettings.GlobalScale.PositionScaleModifier = 1f;
		config.GlobalSettings.GlobalScale.SizeScaleModifier = 1f;

		config.GlobalSettings.Performance.UpdateDelay = 0.1f;
		config.GlobalSettings.Performance.CalculationCaching = true;

		config.LargeMonsterUI.Dynamic.Settings.RenderDeadOrCaptured = false;
		config.LargeMonsterUI.Dynamic.Settings.RenderHighlightedMonster = true;
		config.LargeMonsterUI.Dynamic.Settings.RenderNotHighlightedMonsters = true;
		config.LargeMonsterUI.Dynamic.Settings.AddMissionBeaconOffsetToWorldOffset = false;
		config.LargeMonsterUI.Dynamic.Settings.AddModelRadiusToWorldOffsetY = true;
		config.LargeMonsterUI.Dynamic.Settings.OpacityFalloff = true;
		config.LargeMonsterUI.Dynamic.Settings.MaxDistance = 200f;

		config.LargeMonsterUI.Dynamic.WorldOffset.X = 0f;
		config.LargeMonsterUI.Dynamic.WorldOffset.Y = 0f;
		config.LargeMonsterUI.Dynamic.WorldOffset.Z = 0f;

		config.LargeMonsterUI.Dynamic.Offset.X = -148.5f;
		config.LargeMonsterUI.Dynamic.Offset.Y = 0f;

		config.LargeMonsterUI.Dynamic.NameLabel.Visible = true;
		config.LargeMonsterUI.Dynamic.NameLabel.Format = "{0}";
		config.LargeMonsterUI.Dynamic.NameLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Dynamic.NameLabel.Offset.X = 7f;
		config.LargeMonsterUI.Dynamic.NameLabel.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.NameLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.NameLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.Visible = true;

		config.LargeMonsterUI.Dynamic.Health.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Health.Offset.Y = 15f;

		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Health.ValueLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Visible = false;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Offset.X = 245f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Dynamic.Health.PercentageLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Dynamic.Health.Bar.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Dynamic.Health.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Size.Width = 297f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Size.Height = 12f;

		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.StartInfo1.Rgba = 0x004016CC;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.StartInfo2.Rgba = 0x004016CC;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.EndInfo1.Rgba = 0x34FF4ECC;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Foreground.EndInfo2.Rgba = 0x34FF4ECC;

		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.StartInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.StartInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.EndInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Dynamic.Health.Bar.Colors.Background.EndInfo2.Rgba = 0x0000004A;


		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Dynamic.Health.Bar.Outline.Color.colorInfo.Rgba = 0x00000080;

		config.LargeMonsterUI.Static.Settings.RenderDeadOrCaptured = false;
		config.LargeMonsterUI.Static.Settings.RenderHighlightedMonster = true;
		config.LargeMonsterUI.Static.Settings.RenderNotHighlightedMonsters = true;

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
		config.LargeMonsterUI.Static.NameLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.NameLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.NameLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Health.Visible = true;

		config.LargeMonsterUI.Static.Health.Offset.X = 0f;
		config.LargeMonsterUI.Static.Health.Offset.Y = 15f;

		config.LargeMonsterUI.Static.Health.ValueLabel.Visible = true;
		config.LargeMonsterUI.Static.Health.ValueLabel.Format = "{0:F1}/{1:F0}";
		config.LargeMonsterUI.Static.Health.ValueLabel.Settings.RightAlignmentShift = 0;
		config.LargeMonsterUI.Static.Health.ValueLabel.Offset.X = 7f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Health.ValueLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Health.PercentageLabel.Visible = true;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Format = "{0:P1}";
		config.LargeMonsterUI.Static.Health.PercentageLabel.Settings.RightAlignmentShift = 6;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Offset.X = 245f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Offset.Y = 9f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Color.colorInfo.Rgba = 0xFFFFFFFF;

		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Visible = true;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Offset.X = 2f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Offset.Y = 2f;
		config.LargeMonsterUI.Static.Health.PercentageLabel.Shadow.Color.colorInfo.Rgba = 0x000000FF;

		config.LargeMonsterUI.Static.Health.Bar.Visible = true;
		config.LargeMonsterUI.Static.Health.Bar.Settings.FillDirection = FillDirections.LeftToRight;
		config.LargeMonsterUI.Static.Health.Bar.Offset.X = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Offset.Y = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Size.Width = 297f;
		config.LargeMonsterUI.Static.Health.Bar.Size.Height = 12f;

		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.StartInfo1.Rgba = 0x004016CC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.StartInfo2.Rgba = 0x004016CC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.EndInfo1.Rgba = 0x34FF4ECC;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Foreground.EndInfo2.Rgba = 0x34FF4ECC;

		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.StartInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.StartInfo2.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.EndInfo1.Rgba = 0x0000004A;
		config.LargeMonsterUI.Static.Health.Bar.Colors.Background.EndInfo2.Rgba = 0x0000004A;


		config.LargeMonsterUI.Static.Health.Bar.Outline.Visible = true;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Thickness = 2f;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Offset = 0f;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Style = OutlineStyles.Inside;
		config.LargeMonsterUI.Static.Health.Bar.Outline.Color.colorInfo.Rgba = 0x00000080;

		configDatabase.Save();
	}
}
