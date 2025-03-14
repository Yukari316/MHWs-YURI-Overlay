﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using REFrameworkNET;
using REFrameworkNET.Attributes;


namespace YURI_Overlay;

[SuppressMessage("Roslynator", "RCS1102:Make class Static", Justification = "<Pending>")]
public class Plugin
{
	public static bool IsInitialized { get; private set; }

	[PluginEntryPoint]
	public static void Main()
	{
		LogManager.Info("Entering the void...");

		try
		{
			Task.Run(Init);
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	[PluginExitPoint]
	public static void Unload()
	{
		LogManager.Info("Disposing...");

		ConfigManager.Instance.Dispose();
		LocalizationManager.Instance.Dispose();
		ReframeworkManager.Instance.Dispose();
		MonsterManager.Instance.Dispose();

		API.LocalFrameGC();

		LogManager.Info("Disposed!");

		LogManager.Info("I permitted it to pass over me and through me. When it had gone past I turned the inner eye to see its path. Where the fear had gone, there was nothing. Only I remained...");
	}

	private static void Init()
	{
		try
		{
			LogManager.Info("Managers: Initializing...");

#if DEBUG
			LogManager.Info("Waiting for debugger...");
			if (!Debugger.IsAttached)
			{
				Debugger.Launch();
			}
#endif

			var configManager = ConfigManager.Instance;
			var localizationManager = LocalizationManager.Instance;
			var localizationHelper = LocalizationHelper.Instance;
			var reframeworkManager = ReframeworkManager.Instance;

			//var fontManager = FontManager.Instance;

			var imGuiManager = ImGuiManager.Instance;
			var overlayManager = OverlayManager.Instance;



			var cameraManager = ScreenManager.Instance;
			var monsterManager = MonsterManager.Instance;

			configManager.Initialize();
			localizationManager.Initialize();
			localizationHelper.Initialize();
			reframeworkManager.Initialize();

			//fontManager.Initialize();

			imGuiManager.Initialize();
			overlayManager.Initialize();

			cameraManager.Initialize();
			monsterManager.Initialize();

			LogManager.Info("Managers: Initialized!");
			LogManager.Info("Callbacks: Initializing...");

			REFrameworkNET.Callbacks.ImGuiRender.Post += OnImGuiRender;

			IsInitialized = true;

			LogManager.Info("Callbacks: Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}


	private static void OnImGuiRender()
	{
		if(!IsInitialized) return;

		try
		{
			ScreenManager.Instance.FrameUpdate();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}

		try
		{
			ScreenManager.Instance.FrameUpdate();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}

		try
		{
			if(ReframeworkManager.Instance.IsReframeworkMenuOpen)
			{
				ImGuiManager.Instance.Draw();
			}
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}

		try
		{
			OverlayManager.Instance.Draw();
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
