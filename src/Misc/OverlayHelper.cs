using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal static class OverlayHelper
{
	public static uint ScaleColorOpacityAbgr(uint colorAbgr, float opacity)
	{
		var red = (colorAbgr & 0x000000FF) >> 0;
		var green = (colorAbgr & 0x0000FF00) >> 8;
		var blue = (colorAbgr & 0x00FF0000) >> 16;
		var alpha = (colorAbgr & 0xFF000000) >> 24;

		alpha = (uint) (alpha * opacity);

		return alpha << 24 | blue << 16 | green << 8 | red;
	}
}
