using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class BarElement
{
	private Func<BarElementCustomization> _customizationAccessor;

	private (string, float, float, float, float, float, float, float, float) cashingKeyByPosition1 = ("", 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
	private (string, float, float, float) cashingKeyByProgress2 = ("", 0f, 0f, 0f);

	private float outlinePositionX = 0f;
	private float outlinePositionY = 0f;

	private float outlineWidth = 0f;
	private float outlineHeight = 0f;

	private float positionX = 0f;
	private float positionY = 0f;

	private float width = 0f;
	private float height = 0f;

	private float foregroundWidth = 0f;
	private float foregroundHeight = 0f;

	private float backgroundWidth = 0f;
	private float backgroundHeight = 0f;

	private float foregroundShiftX = 0f;
	private float foregroundShiftY = 0f;

	private float backgroundShiftX = 0f;
	private float backgroundShiftY = 0f;

	private uint backgrounColorStart = 0;
	private uint backgrounColorEnd = 0;

	private uint foregrounColorStart = 0;
	private uint foregrounColorEnd = 0;

	private uint outlineColor = 0;

	private Vector2 backgroundTopLeft = Vector2.Zero;
	private Vector2 backgroundBottomRight = Vector2.Zero;

	private Vector2 foregroundTopLeft = Vector2.Zero;
	private Vector2 foregroundBottomRight = Vector2.Zero;

	private Vector2 outlineTopLeft = Vector2.Zero;
	private Vector2 outlineBottomRight = Vector2.Zero;

	public BarElement()
	{
		_customizationAccessor = () => new();
	}

	public BarElement(Func<BarElementCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float progress = 0.5f, float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		if(!customization.visible) return;

		progress = Utils.Clamp(progress, 0f, 1f);

		var outline = customization.outline;
		var outlineThickness = outline.thickness;

		UpdateByPosition1(position);
		UpdateByProgress2(progress);
		UpdateByOpacity3(opacityScale);
		Update4();

		// Background

		backgroundDrawList.AddRectFilledMultiColor(
			backgroundTopLeft,
			backgroundBottomRight,
			backgrounColorStart,
			backgrounColorEnd,
			backgrounColorEnd,
			backgrounColorStart);

		// Foreground

		backgroundDrawList.AddRectFilledMultiColor(
			foregroundTopLeft,
			foregroundBottomRight,
			foregrounColorStart,
			foregrounColorEnd,
			foregrounColorEnd,
			foregrounColorStart);

		// Outline

		if(outline.visible && outlineThickness > 0f)
		{
			backgroundDrawList.AddRect(
				outlineTopLeft,
				outlineBottomRight,
				outlineColor,
				0f,
				ImDrawFlags.None,
				outlineThickness);
		}
	}

	private void UpdateByPosition1(Vector2 position, bool disableCaching = false)
	{
		var customization = _customizationAccessor();

		var offset = customization.offset;
		var size = customization.size;
		var outline = customization.outline;

		var outlineThickness = outline.thickness;
		var outlineOffset = outline.offset;
		var outlineStyle = outline.style;

		var cachingKey = (outlineStyle, position.X, position.Y, offset.x, offset.y, size.width, size.height, outlineThickness, outlineOffset);

		if(!disableCaching && cachingKey == cashingKeyByPosition1) return;
		cashingKeyByPosition1 = cachingKey;

		var halfOutlineThickness = outlineThickness / 2f;
		var halfOutlineOffset = outlineOffset / 2f;

		switch(outlineStyle)
		{
			case "Inside":
				outlinePositionX = position.X + offset.x + halfOutlineThickness;
				outlinePositionY = position.Y + offset.y + halfOutlineThickness;

				outlineWidth = size.width - outlineThickness;
				outlineHeight = size.height - outlineThickness;

				positionX = outlinePositionX + halfOutlineThickness + outlineOffset;
				positionY = outlinePositionY + halfOutlineThickness + outlineOffset;

				width = outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				height = outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;
			case "Center":
				outlinePositionX = position.X + offset.x - halfOutlineOffset;
				outlinePositionY = position.Y + offset.y - halfOutlineOffset;

				outlineWidth = size.width + outlineOffset;
				outlineHeight = size.height + outlineOffset;

				positionX = outlinePositionX + halfOutlineThickness + outlineOffset;
				positionY = outlinePositionY + halfOutlineThickness + outlineOffset;

				width = outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				height = outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;
			case "Outside":
			default:
				positionX = position.X + offset.x;
				positionY = position.Y + offset.y;

				width = size.width;
				height = size.height;

				outlinePositionX = positionX - halfOutlineThickness - outlineOffset;
				outlinePositionY = positionY - halfOutlineThickness - outlineOffset;

				outlineWidth = width + outlineThickness + outlineOffset + outlineOffset;
				outlineHeight = height + outlineThickness + outlineOffset + outlineOffset;

				break;
		}
	}

	private void UpdateByProgress2(float progress = 0.5f, bool disableCaching = false)
	{
		var customization = _customizationAccessor();

		var fillDirection = customization.settings.fillDirection;

		var cachingKey = (fillDirection, width, height, progress);

		if(!disableCaching && cachingKey == cashingKeyByProgress2) return;
		cashingKeyByProgress2 = cachingKey;

		switch(fillDirection)
		{
			case "RightToLeft":
				foregroundWidth = width * progress;
				foregroundHeight = height;

				backgroundWidth = width - foregroundWidth;
				backgroundHeight = height;

				foregroundShiftX = backgroundWidth;
				break;
			case "TopToBottom":
				foregroundWidth = width;
				foregroundHeight = height * progress;

				backgroundWidth = width;
				backgroundHeight = height - foregroundHeight;

				backgroundShiftY = foregroundHeight;

				break;
			case "BottomToTop":
				foregroundWidth = width;
				foregroundHeight = height * progress;

				backgroundWidth = width;
				backgroundHeight = height - foregroundHeight;

				foregroundShiftY = backgroundHeight;

				break;
			case "LeftToRight":
			default:
				foregroundWidth = width * progress;
				foregroundHeight = height;

				backgroundWidth = width - foregroundWidth;
				backgroundHeight = height;

				backgroundShiftX = foregroundWidth;

				break;
		}
	}

	private void UpdateByOpacity3(float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		var colors = customization.colors;
		var backgroundColor = colors.background;
		var foregroundColor = colors.foreground;

		backgrounColorStart = backgroundColor.StartInfo.Abgr;
		backgrounColorEnd = backgroundColor.EndInfo.Abgr;

		foregrounColorStart = foregroundColor.StartInfo.Abgr;
		foregrounColorEnd = foregroundColor.EndInfo.Abgr;

		outlineColor = customization.outline.Color.colorInfo.Abgr;

		if(Utils.IsApproximatelyEqual(opacityScale, 1f)) return;

		backgrounColorStart = OverlayHelper.ScaleColorOpacityAbgr(backgrounColorStart, opacityScale);
		backgrounColorEnd = OverlayHelper.ScaleColorOpacityAbgr(backgrounColorEnd, opacityScale);

		foregrounColorStart = OverlayHelper.ScaleColorOpacityAbgr(foregrounColorStart, opacityScale);
		foregrounColorEnd = OverlayHelper.ScaleColorOpacityAbgr(foregrounColorEnd, opacityScale);

		outlineColor = OverlayHelper.ScaleColorOpacityAbgr(outlineColor, opacityScale);
	}

	private void Update4()
	{
		var customization = _customizationAccessor();

		// Background

		backgroundTopLeft = new Vector2(
				positionX + backgroundShiftX,
				positionY + backgroundShiftY
			);

		backgroundBottomRight = new Vector2(
			backgroundTopLeft.X + backgroundWidth,
			backgroundTopLeft.Y + backgroundHeight
		);

		// Foreground

		foregroundTopLeft = new Vector2(
				positionX + foregroundShiftX,
				positionY + foregroundShiftY
			);

		foregroundBottomRight = new Vector2(
			foregroundTopLeft.X + foregroundWidth,
			foregroundTopLeft.Y + foregroundHeight
		);

		// Outline

		if(customization.outline.thickness > 0f)
		{
			outlineTopLeft = new Vector2(
				outlinePositionX,
				outlinePositionY
			);

			outlineBottomRight = new Vector2(
				outlinePositionX + outlineWidth,
				outlinePositionY + outlineHeight
			);
		}
	}
}
