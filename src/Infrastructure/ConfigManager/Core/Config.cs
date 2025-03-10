namespace YURI_Overlay;

internal class Config
{
	public GlobalSettingsCustomization GlobalSettings = new();
	public LargeMonsterUiCustomization LargeMonsterUI = new();
	public Dictionary<string, FontCustomization> Fonts = [];
}
