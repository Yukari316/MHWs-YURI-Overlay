using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal static class Utils
{
	public static int Clamp(int value, int min, int max)
	{
		if(value < min)
		{
			return min;
		}
		else if(value > max)
		{
			return max;
		}

		return value;
	}

	public static float Clamp(float value, float min, float max)
	{
		if(value < min)
		{
			return min;
		}
		else if(value > max)
		{
			return max;
		}

		return value;
	}

	public static bool IsApproximatelyEqual(float a, float b)
	{
		return Math.Abs(a - b) <= Constants.EPSILON;
	}

	public static void OpenLink(string url)
	{
		try
		{
			Process.Start(url);
		}
		catch(Exception exception)
		{
			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				url = url.Replace("&", "^&");
				Process.Start(new ProcessStartInfo(url) { FileName = url, UseShellExecute = true });
			}
			else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				Process.Start("xdg-open", url);
			}
			else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				Process.Start("open", url);
			}
			else
			{
				LogManager.Warn(exception);
			}
		}
	}

	public static void ImGuiEndRect(float additionalSize = 0f, float rounding = -1f, float thickness = 1f)
	{
		ImGui.EndGroup();

		float _rounding = rounding < 0f ? ImGui.GetStyle().FrameRounding : rounding;

		var itemRectMin = ImGui.GetItemRectMin();
		itemRectMin.X -= additionalSize;
		itemRectMin.Y -= additionalSize;

		var itemRectMax = ImGui.GetItemRectMax();
		itemRectMax.X += additionalSize;
		itemRectMax.Y += additionalSize;

		ImGui.GetWindowDrawList().AddRect(itemRectMin, itemRectMax, ImGui.GetColorU32(ImGuiCol.Border), _rounding, ImDrawFlags.RoundCornersAll, thickness);
		ImGui.NewLine();
	}

	public static void EmitEvents(object sender, EventHandler eventHandler)
	{
		foreach(Delegate subscriber in eventHandler.GetInvocationList())
		{
			try
			{
				subscriber.DynamicInvoke(sender, EventArgs.Empty);
			}
			catch(Exception exception)
			{
				LogManager.Error(exception);
			}
		}
	}

	public static string Stringify<T>(T value)
	{
		try
		{
			return JsonSerializer.Serialize(value, Constants.JSON_SERIALIZER_OPTIONS_INSTANCE);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
			return string.Empty;
		}
	}

	public static uint AbgrToRgba(uint argb)
	{
		var red = (argb & 0x000000FF);
		var green = (argb & 0x0000FF00) >> 8;
		var blue = argb & 0x00FF0000 >> 16;
		var alpha = (argb & 0xFF000000) >> 24;
		return (uint) ((red << 24) | (green << 16) | (blue << 8) | blue);
	}

	public static uint RgbaToAbgr(uint rgba)
	{
		var red = (rgba & 0xFF000000) >> 24;
		var green = (rgba & 0x00FF0000) >> 16;
		var blue = (rgba & 0x0000FF00) >> 8;
		var alpha = rgba & 0x000000FF;
		return (uint) ((alpha << 24) | (blue << 16) | (green << 8) | red);
	}
}
