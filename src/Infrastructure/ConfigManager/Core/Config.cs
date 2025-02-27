namespace YURI_Overlay;

internal class Config
{
	public string localization = Constants.DefaultLocalization;

	public GlobalSettingsCustomization GlobalSettings = new();
	public LargeMonsterUiCustomization LargeMonsterUI = new();
	//public LabelElementCustomization LaMoStaHealthValueLabelCustomization = new();
	//public LabelElementCustomization LaMoStaHealthPercentageLabelCustomization = new();
	//public BarElementCustomization LaMoStaHealthBarCustomization = new();
}
