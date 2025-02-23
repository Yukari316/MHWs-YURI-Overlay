using System.Numerics;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElement
{
	private readonly Func<BarElementCustomization> _customizationAccessor;

	private (string, float, float, float, float, float, float, float, float) _cashingKeyByPosition1 = ("", 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
	private (string, float, float, float) _cashingKeyByProgress2 = ("", 0f, 0f, 0f);

	private float _outlinePositionX = 0f;
	private float _outlinePositionY = 0f;

	private float _outlineWidth = 0f;
	private float _outlineHeight = 0f;

	private float _positionX = 0f;
	private float _positionY = 0f;

	private float _width = 0f;
	private float _height = 0f;

	private float _foregroundWidth = 0f;
	private float _foregroundHeight = 0f;

	private float _backgroundWidth = 0f;
	private float _backgroundHeight = 0f;

	private float _foregroundShiftX = 0f;
	private float _foregroundShiftY = 0f;

	private float _backgroundShiftX = 0f;
	private float _backgroundShiftY = 0f;

	private uint _backgroundColorStart = 0;
	private uint _backgroundColorEnd = 0;

	private uint _foregroundColorStart = 0;
	private uint _foregroundColorEnd = 0;

	private uint _outlineColor = 0;

	private Vector2 _backgroundTopLeft = Vector2.Zero;
	private Vector2 _backgroundBottomRight = Vector2.Zero;

	private Vector2 _foregroundTopLeft = Vector2.Zero;
	private Vector2 _foregroundBottomRight = Vector2.Zero;

	private Vector2 _outlineTopLeft = Vector2.Zero;
	private Vector2 _outlineBottomRight = Vector2.Zero;

	public BarElement()
	{
		_customizationAccessor = () => new BarElementCustomization();
	}

	public BarElement(Func<BarElementCustomization> customizationAccessor)
	{
		_customizationAccessor = customizationAccessor;
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float progress = 0.5f, float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		if(!customization.visible)
		{
			return;
		}

		progress = Utils.Clamp(progress, 0f, 1f);

		var outline = customization.outline;
		var outlineThickness = outline.thickness;

		UpdateByPosition1(position);
		UpdateByProgress2(progress);
		UpdateByOpacity3(opacityScale);
		Update4();

		// Background

		backgroundDrawList.AddRectFilledMultiColor(
			_backgroundTopLeft,
			_backgroundBottomRight,
			_backgroundColorStart,
			_backgroundColorEnd,
			_backgroundColorEnd,
			_backgroundColorStart);

		// Foreground

		backgroundDrawList.AddRectFilledMultiColor(
			_foregroundTopLeft,
			_foregroundBottomRight,
			_foregroundColorStart,
			_foregroundColorEnd,
			_foregroundColorEnd,
			_foregroundColorStart);

		// Outline

		if(outline.visible && outlineThickness > 0f)
		{
			backgroundDrawList.AddRect(
				_outlineTopLeft,
				_outlineBottomRight,
				_outlineColor,
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

		if(!disableCaching && cachingKey == _cashingKeyByPosition1)
		{
			return;
		}

		_cashingKeyByPosition1 = cachingKey;

		var halfOutlineThickness = outlineThickness / 2f;
		var halfOutlineOffset = outlineOffset / 2f;

		switch(outlineStyle)
		{
			case "Inside":
				_outlinePositionX = position.X + offset.x + halfOutlineThickness;
				_outlinePositionY = position.Y + offset.y + halfOutlineThickness;

				_outlineWidth = size.width - outlineThickness;
				_outlineHeight = size.height - outlineThickness;

				_positionX = _outlinePositionX + halfOutlineThickness + outlineOffset;
				_positionY = _outlinePositionY + halfOutlineThickness + outlineOffset;

				_width = _outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				_height = _outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;
			case "Center":
				_outlinePositionX = position.X + offset.x - halfOutlineOffset;
				_outlinePositionY = position.Y + offset.y - halfOutlineOffset;

				_outlineWidth = size.width + outlineOffset;
				_outlineHeight = size.height + outlineOffset;

				_positionX = _outlinePositionX + halfOutlineThickness + outlineOffset;
				_positionY = _outlinePositionY + halfOutlineThickness + outlineOffset;

				_width = _outlineWidth - outlineThickness - outlineOffset - outlineOffset;
				_height = _outlineHeight - outlineThickness - outlineOffset - outlineOffset;

				break;
			case "Outside":
			default:
				_positionX = position.X + offset.x;
				_positionY = position.Y + offset.y;

				_width = size.width;
				_height = size.height;

				_outlinePositionX = _positionX - halfOutlineThickness - outlineOffset;
				_outlinePositionY = _positionY - halfOutlineThickness - outlineOffset;

				_outlineWidth = _width + outlineThickness + outlineOffset + outlineOffset;
				_outlineHeight = _height + outlineThickness + outlineOffset + outlineOffset;

				break;
		}
	}

	private void UpdateByProgress2(float progress = 0.5f, bool disableCaching = false)
	{
		var customization = _customizationAccessor();

		var fillDirection = customization.settings.fillDirection;

		var cachingKey = (fillDirection, _width, _height, progress);

		if(!disableCaching && cachingKey == _cashingKeyByProgress2)
		{
			return;
		}

		_cashingKeyByProgress2 = cachingKey;

		switch(fillDirection)
		{
			case "RightToLeft":
				_foregroundWidth = _width * progress;
				_foregroundHeight = _height;

				_backgroundWidth = _width - _foregroundWidth;
				_backgroundHeight = _height;

				_foregroundShiftX = _backgroundWidth;
				break;
			case "TopToBottom":
				_foregroundWidth = _width;
				_foregroundHeight = _height * progress;

				_backgroundWidth = _width;
				_backgroundHeight = _height - _foregroundHeight;

				_backgroundShiftY = _foregroundHeight;

				break;
			case "BottomToTop":
				_foregroundWidth = _width;
				_foregroundHeight = _height * progress;

				_backgroundWidth = _width;
				_backgroundHeight = _height - _foregroundHeight;

				_foregroundShiftY = _backgroundHeight;

				break;
			case "LeftToRight":
			default:
				_foregroundWidth = _width * progress;
				_foregroundHeight = _height;

				_backgroundWidth = _width - _foregroundWidth;
				_backgroundHeight = _height;

				_backgroundShiftX = _foregroundWidth;

				break;
		}
	}

	private void UpdateByOpacity3(float opacityScale = 1f)
	{
		var customization = _customizationAccessor();

		var colors = customization.colors;
		var backgroundColor = colors.background;
		var foregroundColor = colors.foreground;

		_backgroundColorStart = backgroundColor.StartInfo.Abgr;
		_backgroundColorEnd = backgroundColor.EndInfo.Abgr;

		_foregroundColorStart = foregroundColor.StartInfo.Abgr;
		_foregroundColorEnd = foregroundColor.EndInfo.Abgr;

		_outlineColor = customization.outline.Color.colorInfo.Abgr;

		if(Utils.IsApproximatelyEqual(opacityScale, 1f))
		{
			return;
		}

		_backgroundColorStart = Utils.ScaleColorOpacityAbgr(_backgroundColorStart, opacityScale);
		_backgroundColorEnd = Utils.ScaleColorOpacityAbgr(_backgroundColorEnd, opacityScale);

		_foregroundColorStart = Utils.ScaleColorOpacityAbgr(_foregroundColorStart, opacityScale);
		_foregroundColorEnd = Utils.ScaleColorOpacityAbgr(_foregroundColorEnd, opacityScale);

		_outlineColor = Utils.ScaleColorOpacityAbgr(_outlineColor, opacityScale);
	}

	private void Update4()
	{
		var customization = _customizationAccessor();

		// Background

		_backgroundTopLeft = new Vector2(
				_positionX + _backgroundShiftX,
				_positionY + _backgroundShiftY
			);

		_backgroundBottomRight = new Vector2(
			_backgroundTopLeft.X + _backgroundWidth,
			_backgroundTopLeft.Y + _backgroundHeight
		);

		// Foreground

		_foregroundTopLeft = new Vector2(
				_positionX + _foregroundShiftX,
				_positionY + _foregroundShiftY
			);

		_foregroundBottomRight = new Vector2(
			_foregroundTopLeft.X + _foregroundWidth,
			_foregroundTopLeft.Y + _foregroundHeight
		);

		// Outline

		if(customization.outline.thickness > 0f)
		{
			_outlineTopLeft = new Vector2(
				_outlinePositionX,
				_outlinePositionY
			);

			_outlineBottomRight = new Vector2(
				_outlinePositionX + _outlineWidth,
				_outlinePositionY + _outlineHeight
			);
		}
	}
}
