using System.Numerics;

namespace YURI_Overlay;

internal static class AnchorPositionCalculator
{
	public static Vector2 Convert(AnchoredPositionCustomization anchoredPositionCustomization, float positionScaleModifier = 1f)
	{
		var displaySize = ScreenManager.Instance.DisplaySize;

		var displayWidth = displaySize.X;
		var displayHeight = displaySize.Y;

		return anchoredPositionCustomization.anchor switch
		{
			Anchors.TopCenter => new Vector2(
								(displayWidth / 2f) + (anchoredPositionCustomization.x * positionScaleModifier),
								anchoredPositionCustomization.y * positionScaleModifier
							),
			Anchors.TopRight => new Vector2(
								displayWidth + (anchoredPositionCustomization.x * positionScaleModifier),
								anchoredPositionCustomization.y * positionScaleModifier
							),
			Anchors.CenterLeft => new Vector2(
								anchoredPositionCustomization.x * positionScaleModifier,
								(displayHeight / 2f) + (anchoredPositionCustomization.y * positionScaleModifier)
							),
			Anchors.Center => new Vector2(
								(displayWidth / 2f) + (anchoredPositionCustomization.x * positionScaleModifier),
								(displayHeight / 2f) + (anchoredPositionCustomization.y * positionScaleModifier)
							),
			Anchors.CenterRight => new Vector2(
								displayWidth + (anchoredPositionCustomization.x * positionScaleModifier),
								(displayHeight / 2f) + (anchoredPositionCustomization.y * positionScaleModifier)
							),
			Anchors.BottomLeft => new Vector2(
								anchoredPositionCustomization.x * positionScaleModifier,
								displayHeight + (anchoredPositionCustomization.y * positionScaleModifier)
							),
			Anchors.BottomCenter => new Vector2(
								(displayWidth / 2f) + (anchoredPositionCustomization.x * positionScaleModifier),
								displayHeight + (anchoredPositionCustomization.y * positionScaleModifier)
							),
			Anchors.BottomRight => new Vector2(
								displayWidth + (anchoredPositionCustomization.x * positionScaleModifier),
								displayHeight + (anchoredPositionCustomization.y * positionScaleModifier)
							),
			_ => new Vector2(
								anchoredPositionCustomization.x * positionScaleModifier,
								anchoredPositionCustomization.y * positionScaleModifier
							),
		};
	}
}
