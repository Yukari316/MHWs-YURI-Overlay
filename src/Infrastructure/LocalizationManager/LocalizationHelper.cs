using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class LocalizationHelper
{
	private static readonly Lazy<LocalizationHelper> _lazy = new(() => new LocalizationHelper());

	public static LocalizationHelper Instance => _lazy.Value;

	public string[] defaultFillDirections = new string[4];
	public string[] fillDirections = new string[4];

	public string[] defaultOutlineStyles = new string[3];
	public string[] outlineStyles = new string[3];

	public string[] defaultSortingLocations = new string[3];
	public string[] sortingLocations = new string[3];

	public string[] defaultSortings = new string[6];
	public string[] sortings = new string[6];

	public string[] defaultAnchors  = new string[9];
	public string[] anchors = new string[9];

	public LocalizationHelper() { }

	public void Initialize()
	{
		var localizationManager = LocalizationManager.Instance;

		localizationManager.activeLocalizationChanged += OnActiveLocalizationChanged;

		var defaultLocalization = localizationManager.defaultLocalization.data.imGui;


		defaultFillDirections =
		[
			defaultLocalization.leftToRight,
			defaultLocalization.rightToLeft,
			defaultLocalization.topToBottom,
			defaultLocalization.bottomToTop
		];

		defaultOutlineStyles =
		[
			defaultLocalization.inside,
			defaultLocalization.center,
			defaultLocalization.outside
		];

		defaultSortingLocations =
		[
			defaultLocalization.normal,
			defaultLocalization.first,
			defaultLocalization.last
		];

		defaultSortings =
		[
			defaultLocalization.normal,
			defaultLocalization.id,
			defaultLocalization.name,
			defaultLocalization.health,
			defaultLocalization.healthPercentage,
			defaultLocalization.distance
		];

		defaultAnchors =
		[
			defaultLocalization.topLeft,
			defaultLocalization.topCenter,
			defaultLocalization.topRight,
			defaultLocalization.centerLeft,
			defaultLocalization.center,
			defaultLocalization.centerRight,
			defaultLocalization.bottomLeft,
			defaultLocalization.bottomCenter,
			defaultLocalization.bottomRight
		];

		Update();
	}

	public void Update()
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		fillDirections =
		[
			localization.leftToRight,
			localization.rightToLeft,
			localization.topToBottom,
			localization.bottomToTop
		];

		outlineStyles =
		[
			localization.inside,
			localization.center,
			localization.outside
		];

		sortingLocations =
		[
			localization.normal,
			localization.first,
			localization.last
		];

		sortings =
		[
			localization.normal,
			localization.id,
			localization.name,
			localization.health,
			localization.healthPercentage,
			localization.distance
		];

		anchors =
		[
			localization.topLeft,
			localization.topCenter,
			localization.topRight,
			localization.centerLeft,
			localization.center,
			localization.centerRight,
			localization.bottomLeft,
			localization.bottomCenter,
			localization.bottomRight
		];
	}

	private void OnActiveLocalizationChanged(object sender, EventArgs e)
	{
		Update();
	}
}
