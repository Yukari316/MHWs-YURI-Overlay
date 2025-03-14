namespace YURI_Overlay
{
	public interface ILocalizationInfo
	{
		string Name { get; }
		string Translators { get; }
	}

	public interface IFontInfo
	{
		string Name { get; set; }
		string[] GlyphRanges { get; set; }
	}

	public interface IImGuiLocalization
	{
		// Mod Info
		string ModInfo { get; }
		string MadeBy { get; }
		string NexusMods { get; }
		string GitHubRepo { get; }
		string Twitch { get; }
		string Twitter { get; }
		string ArtStation { get; }
		string DonationMessage1 { get; }
		string DonationMessage2 { get; }
		string Donate { get; }
		string PayPal { get; }
		string BuyMeATea { get; }

		// Config
		string Config { get; }
		string ActiveConfig { get; }
		string NewConfigName { get; }
		string New { get; }
		string Duplicate { get; }
		string Delete { get; }
		string Reset { get; }
		string Rename { get; }

		// Localization
		string Language { get; }

		// Bar
		string Bar { get; }
		string Visible { get; }
		string Settings { get; }
		string FillDirection { get; }
		string LeftToRight { get; }
		string RightToLeft { get; }
		string TopToBottom { get; }
		string BottomToTop { get; }
		string Offset { get; }
		string X { get; }
		string Y { get; }
		string Z { get; }
		string Size { get; }
		string Width { get; }
		string Height { get; }
		string Outline { get; }
		string Thickness { get; }
		string Style { get; }
		string Inside { get; }
		string Center { get; }
		string Outside { get; }
		string Colors { get; }
		string Background { get; }
		string Foreground { get; }
		string Color { get; }
		string Start1 { get; }
		string Start2 { get; }
		string End1 { get; }
		string End2 { get; }

		// Label
		string Label { get; }
		string Format { get; }
		string RightAlignmentShift { get; }
		string Shadow { get; }
		string ValueLabel { get; }
		string PercentageLabel { get; }

		// Large Monsters
		string LargeMonstersUi { get; }
		string Static { get; }
		string Dynamic { get; }
		string Highlighted { get; }
		string Spacing { get; }
		string Position { get; }
		string Enabled { get; }
		string RenderDeadOrCaptured { get; }
		string RenderHighlightedMonster { get; }
		string RenderNotHighlightedMonsters { get; }
		string AddMissionBeaconOffsetToWorldOffset { get; }
		string AddModelRadiusToWorldOffsetY { get; }
		string OpacityFalloff { get; }
		string MaxDistance { get; }
		string WorldOffset { get; }
		string Name { get; }
		string Health { get; }
		string HighlightedMonsterLocation { get; }
		string Normal { get; }
		string First { get; }
		string Last { get; }
		string Sorting { get; }
		string Type { get; }
		string Id { get; }
		string MaxHealth { get; }
		string HealthPercentage { get; }
		string Distance { get; }
		string ReversedOrder { get; }
		string Anchor { get; }
		string TopLeft { get; }
		string TopCenter { get; }
		string TopRight { get; }
		string CenterLeft { get; }
		string CenterRight { get; }
		string BottomLeft { get; }
		string BottomCenter { get; }
		string BottomRight { get; }
		string NameLabel { get; }
		string GlobalSettings { get; }
		string GlobalScale { get; }
		string PositionScaleModifier { get; }
		string SizeScaleModifier { get; }
		string Performance { get; }
		string UpdateDelaySeconds { get; }
		string CalculationCaching { get; }
		string Font { get; set; }
		string AnyChangesToFontRequireGameRestart { get; set; }
		string FontSize { get; set; }
		string HorizontalOversample { get; set; }
		string VerticalOversample { get; set; }
	}

	public interface ILocalization
	{
		string IsoCode { get; set; }
		ILocalizationInfo LocalizationInfo { get; }
		IFontInfo FontInfo { get; }
		IImGuiLocalization ImGui { get; }
	}
}