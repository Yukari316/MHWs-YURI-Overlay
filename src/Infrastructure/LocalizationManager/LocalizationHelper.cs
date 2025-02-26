namespace YURI_Overlay;

internal sealed class LocalizationHelper
{
	private static readonly Lazy<LocalizationHelper> _lazy = new(() => new LocalizationHelper());

	public static LocalizationHelper Instance => _lazy.Value;

	public string[] DefaultFillDirections = new string[4];
	public string[] FillDirections = new string[4];

	public string[] DefaultOutlineStyles = new string[3];
	public string[] OutlineStyles = new string[3];

	public string[] DefaultSortingLocations = new string[3];
	public string[] SortingLocations = new string[3];

	public string[] DefaultSortings = new string[6];
	public string[] Sortings = new string[6];

	public string[] DefaultAnchors = new string[9];
	public string[] Anchors = new string[9];

	public LocalizationHelper() { }

	public void Initialize()
	{
		var localizationManager = LocalizationManager.Instance;

		localizationManager.ActiveLocalizationChanged += OnActiveLocalizationChanged;

		var defaultLocalization = localizationManager.DefaultLocalization.Data.imGui;

		DefaultFillDirections =
		[
			defaultLocalization.leftToRight,
			defaultLocalization.rightToLeft,
			defaultLocalization.topToBottom,
			defaultLocalization.bottomToTop
		];

		DefaultOutlineStyles =
		[
			defaultLocalization.inside,
			defaultLocalization.center,
			defaultLocalization.outside
		];

		DefaultSortingLocations =
		[
			defaultLocalization.normal,
			defaultLocalization.first,
			defaultLocalization.last
		];

		DefaultSortings =
		[
			defaultLocalization.name,
			defaultLocalization.id,
			defaultLocalization.health,
			defaultLocalization.maxHealth,
			defaultLocalization.healthPercentage,
			defaultLocalization.distance
		];

		DefaultAnchors =
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
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;

		FillDirections =
		[
			localization.leftToRight,
			localization.rightToLeft,
			localization.topToBottom,
			localization.bottomToTop
		];

		OutlineStyles =
		[
			localization.inside,
			localization.center,
			localization.outside
		];

		SortingLocations =
		[
			localization.normal,
			localization.first,
			localization.last
		];

		Sortings =
		[
			localization.name,
			localization.id,
			localization.health,
			localization.maxHealth,
			localization.healthPercentage,
			localization.distance
		];

		Anchors =
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
