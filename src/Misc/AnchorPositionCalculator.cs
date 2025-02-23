using System.Numerics;

using ImGuiNET;

namespace YURI_Overlay;

internal static class AnchorPositionCalculator
{
	public static Vector2 Convert(AnchoredPositionCustomization anchoredPositionCustomization)
	{
		var screenRegion = ImGui.GetWindowSize();

		return anchoredPositionCustomization.AnchorEnum switch
		{
			Anchors.TopCenter => new Vector2(
								(screenRegion.X / 2f) + anchoredPositionCustomization.x,
								anchoredPositionCustomization.y
							),
			Anchors.TopRight => new Vector2(
								screenRegion.X + anchoredPositionCustomization.x,
								anchoredPositionCustomization.y
							),
			Anchors.CenterLeft => new Vector2(
								anchoredPositionCustomization.x,
								(screenRegion.Y / 2f) + anchoredPositionCustomization.y
							),
			Anchors.Center => new Vector2(
								(screenRegion.X / 2f) + anchoredPositionCustomization.x,
								(screenRegion.Y / 2f) + anchoredPositionCustomization.y
							),
			Anchors.CenterRight => new Vector2(
								screenRegion.X + anchoredPositionCustomization.x,
								(screenRegion.Y / 2f) + anchoredPositionCustomization.y
							),
			Anchors.BottomLeft => new Vector2(
								anchoredPositionCustomization.x,
								screenRegion.Y + anchoredPositionCustomization.y
							),
			Anchors.BottomCenter => new Vector2(
								(screenRegion.X / 2f) + anchoredPositionCustomization.x,
								screenRegion.Y + anchoredPositionCustomization.y
							),
			Anchors.BottomRight => new Vector2(
								screenRegion.X + anchoredPositionCustomization.x,
								screenRegion.Y + anchoredPositionCustomization.y
							),
			_ => new Vector2(
								anchoredPositionCustomization.x,
								anchoredPositionCustomization.y
							),
		};
	}
}
