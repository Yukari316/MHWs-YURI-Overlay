using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

class Screen
{
	public int Width { get; set; }
	public int Height { get; set; }

	public Screen(int width, int height)
	{
		Width = width;
		Height = height;
	}
}

internal static class AnchorPositionCalculator
{
	public static Vector2 Convert(AnchoredPositionCustomization anchoredPositionCustomization)
	{
		Screen screenRegion = new(3840, 2160);

		switch(anchoredPositionCustomization.AnchorEnum)
		{
			case Anchors.TopCenter:
				return new Vector2(
					screenRegion.Width / 2 + anchoredPositionCustomization.x,
					anchoredPositionCustomization.y
				);
			case Anchors.TopRight:
				return new Vector2(
					screenRegion.Width + anchoredPositionCustomization.x,
					anchoredPositionCustomization.y
				);
			case Anchors.CenterLeft:
				return new Vector2(
					anchoredPositionCustomization.x,
					screenRegion.Height / 2 + anchoredPositionCustomization.y
				);
			case Anchors.Center:
				return new Vector2(
					screenRegion.Width / 2 + anchoredPositionCustomization.x,
					screenRegion.Height / 2 + anchoredPositionCustomization.y
				);
			case Anchors.CenterRight:
				return new Vector2(
					screenRegion.Width + anchoredPositionCustomization.x,
					screenRegion.Height / 2 + anchoredPositionCustomization.y
				);
			case Anchors.BottomLeft:
				return new Vector2(
					anchoredPositionCustomization.x,
					screenRegion.Height + anchoredPositionCustomization.y
				);
			case Anchors.BottomCenter:
				return new Vector2(
					screenRegion.Width / 2 + anchoredPositionCustomization.x,
					screenRegion.Height + anchoredPositionCustomization.y
				);
			case Anchors.BottomRight:
				return new Vector2(
					screenRegion.Width + anchoredPositionCustomization.x,
					screenRegion.Height + anchoredPositionCustomization.y
				);
			default:
			case Anchors.TopLeft:
				return new Vector2(
					anchoredPositionCustomization.x,
					anchoredPositionCustomization.y
				);
		}
	}
}
