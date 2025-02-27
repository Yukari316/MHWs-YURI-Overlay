using ImGuiNET;

using REFrameworkNET;
using REFrameworkNET.Attributes;

namespace YURI_Overlay;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1102:Make class Static", Justification = "<Pending>")]
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

			var monsterManager = MonsterManager.Instance;

			configManager.Initialize();
			localizationManager.Initialize();
			localizationHelper.Initialize();

			imGuiManager.Initialize();
			overlayManager.Initialize();

			monsterManager.Initialize();

			IsInitialized = true;

			LogManager.Info("Managers: Initialized!");
			LogManager.Info("Callbacks: Initializing...");

			REFrameworkNET.Callbacks.ImGuiRender.Post += OnImGuiRender;

			LogManager.Info("Callbacks: Initialized!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	private static void OnImGuiRender()
	{
		try
		{
			if(!IsInitialized)
			{
				return;
			}

			var imGuiManager = ImGuiManager.Instance;

			if(ImGui.IsKeyPressed(ImGuiKey.Home))
			{
				imGuiManager.IsOpened = !imGuiManager.IsOpened;
			}

			OverlayManager.Instance.Draw();

			if(imGuiManager.IsOpened)
			{
				ImGuiManager.Instance.Draw();
			}
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
