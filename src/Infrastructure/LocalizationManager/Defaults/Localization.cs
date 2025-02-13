using System.Text.Json.Serialization;

namespace YURI_Overlay;

internal class LocalizationInfo
{
	public string name = "English";
	public string translators = "GreenComfyTea";
}

internal class ImGuiLocalization
{
	// Mod Info
	public string modInfo = "Mod Info";

	public string madeBy = "Made by:";
	public string nexusMods = "Nexus Mods";
	public string gitHubRepo = "GitHub Repo";
	public string twitch = "Twitch";
	public string twitter = "Twitter";
	public string artStation = "ArtStation";
	public string donationMessage1 = "If you like the mod, please consider making a small donation!";
	public string donationMessage2 = "It would help me maintain existing mods and create new ones in the future!";
	public string donate = "Donate";
	public string payPal = "PayPal";
	public string buyMeATea = "Buy Me a Tea";

	public string config = "Config";

	public string activeConfig = "Active Config";
	public string newConfigName = "New Config Name";
	public string @new = "New";
	public string duplicate = "Duplicate";
	public string delete = "Delete";
	public string reset = "Reset";
	public string rename = "Rename";

	// Bar

	public string bar = "Bar";
	public string visible = "Visible";
	public string settings = "Settings";
	public string fillDirection = "Fill Direction";
	public string leftToRight = "Left to Right";
	public string rightToLeft = "Right to Left";
	public string topToBottom = "Top to Bottom";
	public string bottomToTop = "Bottom to Top";
	public string offset = "Offset";
	public string x = "X";
	public string y = "Y";
	public string z = "Z";
	public string size = "Size";
	public string width = "Width";
	public string height = "Height";
	public string outline = "Outline";
	public string thickness = "Thickness";
	public string style = "Style";
	public string inside = "Inside";
	public string center = "Center";
	public string outside = "Outside";
	public string colors = "Colors";
	public string background = "Background";
	public string foreground = "Foreground";
	public string color = "Color";
	public string start = "Start";
	public string end = "End";
	public string healthBar = "Health Bar";

	// Label

	public string label = "Label";
	public string format = "Format";
	public string rightAlignmentShift = "Right Alignment Shift";
	public string shadow = "Shadow";
	public string healthValueLabel = "Health Value Label";
	public string healthPercentageLabel = "Health Percentage Label";

	// Large Monsters
	public string largeMonstersUI = "Large Monsters UI";
	public string @static = "Static";
	public string dynamic = "Dynamic";
	public string highlighted = "Highlighted";
	public string spacing = "Spacing";
	public string position = "Position";

	public string enabled = "Enabled";
	public string hideDeadOrCaptured = "Hide Dead or Captured";
	public string renderHighlightedMonster = "Render Highlighted Monster";
	public string renderNotHighlightedMonsters = "Render Not Highlighted Monsters";
	public string opacityFalloff = "Opacity Falloff";
	public string maxDistance = "Max Distance";
	public string worldOffset = "World Offset";

	public string name = "Name";
	public string health = "Health";

	public string highlightedMonsterLocation = "Highlighted Monster Location";
	public string normal = "Normal";
	public string first = "First";
	public string last = "Last";

	public string sorting = "Sorting";
	public string type = "Type";
	public string id = "ID";
	public string healthPercentage = "Health Percentage";
	public string distance = "Distance";

	public string reversedOrder = "Reversed Order";

	public string anchor = "Anchor";
	public string topLeft = "Top-Left";
	public string topCenter = "Top-Center";
	public string topRight = "Top-Right";
	public string centerLeft = "Center-Left";
	public string centerRight = "Center-Right";
	public string bottomLeft = "Bottom-Left";
	public string bottomCenter = "Bottom-Center";
	public string bottomRight = "Bottom-Right";

	public string nameLabel = "Name Label";

	public string globalSettings = "Global Settings";
	public string performance = "Performance";
	public string updateDelaySeconds = "Update Delay (seconds)";
	public string calculationCaching = "Calculation Caching";
}

internal class Localization
{
	[JsonIgnore]
	public string isoCode = Constants.DEFAULT_LOCALIZATION;

	public LocalizationInfo localizationInfo = new();
	public ImGuiLocalization imGui = new();



}
