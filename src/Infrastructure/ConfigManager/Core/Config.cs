using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class Config
{
	public string localization = Constants.DEFAULT_LOCALIZATION;

	public GlobalSettingsCustomization globalSettings = new();
	public LargeMonsterUICustomization largeMonsterUI = new();
	public LabelElementCustomization LaMoStaHealthValueLabelCustomization = new();
	public LabelElementCustomization LaMoStaHealthPercentageLabelCustomization = new();
	public BarElementCustomization LaMoStaHealthBarCustomization = new();
}
