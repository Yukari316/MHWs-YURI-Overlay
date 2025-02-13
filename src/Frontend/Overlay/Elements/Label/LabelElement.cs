using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class LabelElement
{
	private Func<LabelElementCustomization> _customizationAccessor;

	public LabelElement(Func<LabelElementCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f, params object[] args)
	{
		var customization = _customizationAccessor();

		if(!customization.visible) return;
		if(args.Length == 0) return;

		var text = string.Format(customization.format, args);

		if(string.IsNullOrEmpty(text)) return;

		var rightAlignmentShift = customization.settings.rightAlignmentShift;

		if(rightAlignmentShift != 0) text = text.PadLeft(rightAlignmentShift);

		var offset = customization.offset;
		var shadowOffset = customization.shadow.offset;

		var textPositionX = position.X + offset.x;
		var textPositionY = position.Y + offset.y;

		var shadowPositionX = textPositionX + shadowOffset.x;
		var shadowPositionY = textPositionY + shadowOffset.y;

		var textPosition = new Vector2(textPositionX, textPositionY);
		var shadowPosition = new Vector2(shadowPositionX, shadowPositionY);

		if(customization.shadow.visible)
		{
			var shadowColor = customization.shadow.color.colorInfo.Abgr;

			if(opacityScale < 1)
			{
				shadowColor = OverlayHelper.ScaleColorOpacityAbgr(shadowColor, opacityScale);
			}


			backgroundDrawList.AddText(shadowPosition, shadowColor, text);
		}

		var color = customization.color.colorInfo.Abgr;

		if(opacityScale < 1)
		{
			color = OverlayHelper.ScaleColorOpacityAbgr(color, opacityScale);
		}

		backgroundDrawList.AddText(textPosition, color, text);
	}
}
