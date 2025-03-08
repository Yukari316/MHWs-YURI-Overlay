using System.Diagnostics.CodeAnalysis;

using ImGuiNET;

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
		LogManager.Info("Managers: Disposing...");

		ConfigManager.Instance.Dispose();
		LocalizationManager.Instance.Dispose();
		MonsterManager.Instance.Dispose();

		API.LocalFrameGC();

		LogManager.Info("Managers: Disposed!");
	}

	private static void Init()
	{
		try
		{
			LogManager.Info("Managers: Initializing...");

			var configManager = ConfigManager.Instance;
			var localizationManager = LocalizationManager.Instance;
			var localizationHelper = LocalizationHelper.Instance;

			var imGuiManager = ImGuiManager.Instance;
			var overlayManager = OverlayManager.Instance;

			var cameraManager = ScreenManager.Instance;
			var monsterManager = MonsterManager.Instance;

			configManager.Initialize();
			localizationManager.Initialize();
			localizationHelper.Initialize();

			imGuiManager.Initialize();
			overlayManager.Initialize();

			cameraManager.Initialize();
			monsterManager.Initialize();

			LogManager.Info("Managers: Initialized!");
			LogManager.Info("Callbacks: Initializing...");

			REFrameworkNET.Callbacks.ImGuiRender.Post += OnImGuiRender;
			REFrameworkNET.Callbacks.Initialize.Post += () => LogManager.Info("INIT POST");

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
			var imGuiManager = ImGuiManager.Instance;

			if(ImGui.IsKeyPressed(ImGuiKey.Home)) imGuiManager.IsOpened = !imGuiManager.IsOpened;
			if(imGuiManager.IsOpened) ImGuiManager.Instance.Draw();
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
