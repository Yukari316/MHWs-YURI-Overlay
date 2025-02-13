using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class ColorInfo
{
	public Vector4 vector;
	private uint _rgba;
	private uint _abgr;
	private string _rgbaHex;
	private string _abgrHex;

	public Vector4 Vector
	{
		get => vector;
		set
		{
			vector = value;
			UpdateFromVector(value);
		}
	}

	public uint Rgba
	{
		get => _rgba;
		set
		{
			_rgba = value;
			UpdateFromRgba(value);
		}
	}

	public uint Abgr
	{
		get => _abgr;
		set
		{
			_abgr = value;
			UpdateFromAbgr(value);
		}
	}

	public string RgbaHex
	{
		get => _rgbaHex;
		set
		{
			_rgbaHex = value;
			UpdateFromRgbaHex(value);
		}
	}

	public string AbgrHex
	{
		get => _abgrHex;
		set
		{
			_abgrHex = value;
			UpdateFromAbgrHex(value);
		}
	}

	public ColorInfo()
	{
		Vector = new Vector4(0f, 0f, 0f, 1f);
	}

	public ColorInfo(Vector4 vector)
	{
		Vector = vector;
	}

	private void UpdateFromVector(Vector4 newVector)
	{
		byte red = (byte) (newVector.X * 255f);
		byte green = (byte) (newVector.Y * 255f);
		byte blue = (byte) (newVector.Z * 255f);
		byte alpha = (byte) (newVector.W * 255f);

		_rgba = (uint) red << 24 | (uint) green | (uint) blue << 8 | (uint) alpha;
		_abgr = (uint) alpha << 24 | (uint) blue << 16 | (uint) green << 8 | (uint) red;
		_rgbaHex = $"#{_rgba:X8}";
		_abgrHex = $"#{_abgr:X8}";
	}

	private void UpdateFromRgba(uint rgba)
	{
		LogManager.Info($"{rgba:X8}");

		byte red = (byte) (rgba >> 24);
		byte green = (byte) (rgba >> 16);
		byte blue = (byte) (rgba >> 8);
		byte alpha = (byte) rgba;

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_abgr = (uint)alpha << 24 | (uint)blue << 16 | (uint)green << 8 | red;
		_rgbaHex = $"#{rgba:X8}";
		_abgrHex = $"#{_abgr:X8}";
	}

	private void UpdateFromAbgr(uint abgr)
	{
		byte red = (byte)abgr;
		byte green = (byte)(abgr >> 8);
		byte blue = (byte)(abgr >> 16);
		byte alpha = (byte)(abgr >> 24);

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_rgba = (uint)red << 24 | (uint)green << 16 | (uint)blue << 8 | alpha;
		_rgbaHex = $"#{_rgba:X8}";
		_abgrHex = $"#{abgr:X8}";
	}

	private void UpdateFromRgbaHex(string rgbaHex)
	{
		if (rgbaHex.Length != 9) return;

		_rgba = uint.Parse(rgbaHex[1..], System.Globalization.NumberStyles.HexNumber);

		byte red = (byte)(Rgba >> 24);
		byte green = (byte)(Rgba >> 16);
		byte blue = (byte)(Rgba >> 8);
		byte alpha = (byte)Rgba;

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_abgr = (uint)alpha << 24 | (uint)blue << 16 | (uint)green << 8 | red;
		_abgrHex = $"#{_abgr:X8}";
	}

	private void UpdateFromAbgrHex(string abgrHex)
	{
		if (abgrHex.Length != 9) return;

		_abgr = uint.Parse(abgrHex[1..], System.Globalization.NumberStyles.HexNumber);

		byte red = (byte)Abgr;
		byte green = (byte)(Abgr >> 8);
		byte blue = (byte)(Abgr >> 16);
		byte alpha = (byte)(Abgr >> 24);

		vector = new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		_rgba = (uint)red << 24 | (uint)green << 16 | (uint)blue << 8 | alpha;
		_rgbaHex = $"#{_rgba:X8}";
	}

	public override string ToString()
	{
		return $"Vector4: {vector} | rgba: {_rgba} | abgr: {_abgr} | rgbaHex: {_rgbaHex} | abgrHex: {_abgrHex}";
	}
}
