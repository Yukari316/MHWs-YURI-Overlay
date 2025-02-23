namespace YURI_Overlay;

internal class Config
{
	public string localization = Constants.DefaultLocalization;

	public GlobalSettingsCustomization globalSettings = new();
	public LargeMonsterUiCustomization largeMonsterUI = new();
	public LabelElementCustomization LaMoStaHealthValueLabelCustomization = new();
	public LabelElementCustomization LaMoStaHealthPercentageLabelCustomization = new();
	public BarElementCustomization LaMoStaHealthBarCustomization = new();
}
