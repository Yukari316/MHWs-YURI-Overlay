using System.Numerics;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LabelElement
{
	private readonly Func<LabelElementCustomization> _customizationAccessor;

	public LabelElement(Func<LabelElementCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f, params object[] args)
	{
		var customization = _customizationAccessor();

		if(!customization.Visible)
		{
			return;
		}

		if(args.Length == 0)
		{
			return;
		}

		var text = string.Format(customization.Format, args);

		if(string.IsNullOrEmpty(text))
		{
			return;
		}

		var rightAlignmentShift = customization.Settings.RightAlignmentShift;

		if(rightAlignmentShift != 0)
		{
			text = text.PadLeft(rightAlignmentShift);
		}

		var offset = customization.Offset;
		var shadowOffset = customization.Shadow.Offset;

		var textPositionX = position.X + offset.X;
		var textPositionY = position.Y + offset.Y;

		var shadowPositionX = textPositionX + shadowOffset.X;
		var shadowPositionY = textPositionY + shadowOffset.Y;

		Vector2 textPosition = new(textPositionX, textPositionY);
		Vector2 shadowPosition = new(shadowPositionX, shadowPositionY);

		if(customization.Shadow.Visible)
		{
			var shadowColor = customization.Shadow.Color.colorInfo.Abgr;
			if(opacityScale < 1)
			{
				shadowColor = Utils.ScaleColorOpacityAbgr(shadowColor, opacityScale);
			}

			backgroundDrawList.AddText(shadowPosition, shadowColor, text);
		}

		var color = customization.Color.colorInfo.Abgr;
		if(opacityScale < 1)
		{
			color = Utils.ScaleColorOpacityAbgr(color, opacityScale);
		}

		backgroundDrawList.AddText(textPosition, color, text);
	}
}
