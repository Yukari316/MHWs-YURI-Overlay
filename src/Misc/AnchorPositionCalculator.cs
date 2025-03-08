using System.Numerics;

namespace YURI_Overlay;

internal static class AnchorPositionCalculator
{
	public static Vector2 Convert(AnchoredPositionCustomization anchoredPositionCustomization)
	{
		var displaySize = ScreenManager.Instance.DisplaySize;

		var displayWidth = displaySize.X;
		var displayHeight = displaySize.Y;

		return anchoredPositionCustomization.anchor switch
		{
			Anchors.TopCenter => new Vector2(
								(displayWidth / 2f) + anchoredPositionCustomization.x,
								anchoredPositionCustomization.y
							),
			Anchors.TopRight => new Vector2(
								displayWidth + anchoredPositionCustomization.x,
								anchoredPositionCustomization.y
							),
			Anchors.CenterLeft => new Vector2(
								anchoredPositionCustomization.x,
								(displayHeight / 2f) + anchoredPositionCustomization.y
							),
			Anchors.Center => new Vector2(
								(displayWidth / 2f) + anchoredPositionCustomization.x,
								(displayHeight / 2f) + anchoredPositionCustomization.y
							),
			Anchors.CenterRight => new Vector2(
								displayWidth + anchoredPositionCustomization.x,
								(displayHeight / 2f) + anchoredPositionCustomization.y
							),
			Anchors.BottomLeft => new Vector2(
								anchoredPositionCustomization.x,
								displayHeight + anchoredPositionCustomization.y
							),
			Anchors.BottomCenter => new Vector2(
								(displayWidth / 2f) + anchoredPositionCustomization.x,
								displayHeight + anchoredPositionCustomization.y
							),
			Anchors.BottomRight => new Vector2(
								displayWidth + anchoredPositionCustomization.x,
								displayHeight + anchoredPositionCustomization.y
							),
			_ => new Vector2(
								anchoredPositionCustomization.x,
								anchoredPositionCustomization.y
							),
		};
	}
}
