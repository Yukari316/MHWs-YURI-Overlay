using ImGuiNET;

using FontObject = (string, ImGuiNET.ImFontPtr?);

namespace YURI_Overlay;

internal sealed partial class FontManager
{
	private static readonly Lazy<FontManager> Lazy = new(() => new FontManager());
	public static FontManager Instance => Lazy.Value;

	private bool IsInitialized { get; set; } = false;
	public FontCustomization Customization;

	private ushort[] FullGlyphRange { get; } = [0x0020, ushort.MaxValue, 0];
	private ushort[] EmojiGlyphRange { get; } = [0x2122, 0x2B55, 0];

	public Dictionary<string, FontObject> Fonts { get; set; } = [];

	private List<string> FontNames { get; set; } = [];

	public FontObject ActiveFont = (string.Empty, null);

	private FontManager() { }



	public FontManager Initialize()
	{
		LogManager.Info("[FontManager] Initializing...");

		IsInitialized = true;

		LoadAllFonts();
		SetCurrentFont(LocalizationManager.Instance.ActiveLocalization);

		LogManager.Info("[FontManager] Initialization Done!");

		return this;
	}

	public FontManager LoadAllFonts()
	{
		LogManager.Info("[FontManager] Loading All Fonts...");

		foreach(var localizationPair in LocalizationManager.Instance.Localizations)
		{
			var localizationIsoName = localizationPair.Key;
			var localization = localizationPair.Value;

			if(Fonts.TryGetValue(localizationIsoName, out _)) continue;

			Fonts[localizationIsoName] = LoadFont(localization);
		}

		LogManager.Info("[FontManager] Loading All Fonts Done!");
		return this;
	}

	public unsafe FontObject LoadFont(JsonDatabase<Localization> localization)
	{
		var fontConfig = ConfigManager.Instance.ActiveConfig.Data.Fonts;

		var fontInfo = localization.Data.FontInfo;
		var fontName = fontInfo.Name;

		LogManager.Info($"[FontManager] {fontName}: Loading...");

		if(FontNames.Contains(fontName))
		{
			LogManager.Info($"[FontManager] {fontName}: Already Loaded. Skipping.");
			Fonts.TryGetValue(fontName, out var foundFont);
			return foundFont;
		}

		var glyphRanges = GetGlyphRanges(localization);

		var isFound = fontConfig.TryGetValue(fontName, out var customization);

		if(!isFound)
		{
			customization = new FontCustomization();
			fontConfig[fontName] = customization;
		}

		var newFont = RegisterFont(
			$"{Constants.FontsPath}{fontName}",
			customization!.FontSize,
			glyphRanges,
			false,
			customization.VerticalOversample,
			customization.HorizontalOversample
		);

		RegisterFont(
			$"{Constants.FontsPath}{Constants.EmojiFont}",
			customization.FontSize,
			EmojiGlyphRange,
			true,
			customization.VerticalOversample,
			customization.HorizontalOversample
		);

		FontNames.Add(fontName);

		LogManager.Info($"[FontManager] {fontName}: Loading Done!");
		return (fontName, newFont);
	}

	public void SetCurrentFont(JsonDatabase<Localization> localization)
	{
		Fonts.TryGetValue(localization.Name, out ActiveFont);

		if(!IsInitialized) return;

		ConfigManager.Instance.ActiveConfig.Data.Fonts.TryGetValue(localization.Data.FontInfo.Name, out Customization);

		return;
	}

	public FontManager RecreateFontCustomizations()
	{
		var fontConfig = ConfigManager.Instance.ActiveConfig.Data.Fonts;

		foreach(var fontName in FontNames)
		{
			fontConfig[fontName] = new FontCustomization();
		}

		return this;
	}

	private static ushort[] GetGlyphRanges(JsonDatabase<Localization> localization)
	{
		var glyphRangeStringArray = localization.Data.FontInfo.GlyphRanges;

		var glyphRanges = new ushort[glyphRangeStringArray.Length + 1];

		for(var i = 0; i < glyphRangeStringArray.Length; i += 2)
		{
			var glyph = Convert.ToUInt16(glyphRangeStringArray[i], 16);

			glyphRanges[i] = glyph;
		}

		glyphRanges[^1] = 0;

		return glyphRanges;
	}

	private static unsafe ImFontPtr RegisterFont(string filePathName, float fontSize, ushort[] glyphRanges, bool mergeMode = false, int horizontalOversample = 2, int verticalOversample = 2)
	{
		ImFontConfigPtr imFontConfig = ImGuiNative.ImFontConfig_ImFontConfig();
		imFontConfig.MergeMode = mergeMode;
		imFontConfig.OversampleH = horizontalOversample;
		imFontConfig.OversampleV = verticalOversample;

		return ImGui.GetIO().Fonts.AddFontFromFileTTF(filePathName, fontSize, imFontConfig);
	}
}