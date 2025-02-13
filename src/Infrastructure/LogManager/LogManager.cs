using REFrameworkNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal static class LogManager
{

	public static void Info(object value)
	{
		API.LogInfo($"[{DateTime.Now.ToLongTimeString()}] {{{Constants.MOD_NAME_ABBREVIATION}}} {value}");
	}

	public static void Warn(object value)
	{
		API.LogWarning($"[{DateTime.Now.ToLongTimeString()}] {{{Constants.MOD_NAME_ABBREVIATION}}} {value}");
	}

	public static void Error(object value)
	{
		API.LogError($"[{DateTime.Now.ToLongTimeString()}] {{{Constants.MOD_NAME_ABBREVIATION}}} {value}");
	}
}
