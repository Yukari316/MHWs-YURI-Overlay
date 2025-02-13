using ImGuiNET;
using REFrameworkNET;
using REFrameworkNET.Attributes;

namespace YURI_Overlay;

public class YURI_Overlay_Plugin
{
	public static bool IsInitialized { get; private set; }

	[PluginEntryPoint]
	public static void Main()
	{
		// Your plugin code goes here
		API.LogInfo("Entering the void...");

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

		LogManager.Info("Managers: Disposed!");
	}

	private static void Init()
	{
		try
		{
			LogManager.Info("Managers: Initializing...");

			ConfigManager configManager = ConfigManager.Instance;
			LocalizationManager localizationManager = LocalizationManager.Instance;
			LocalizationHelper localizationHelper = LocalizationHelper.Instance;

			ImGuiManager imGuiManager = ImGuiManager.Instance;
			OverlayManager overlayManager = OverlayManager.Instance;

			//SmallMonsterManager smallMonsterManager = SmallMonsterManager.Instance;
			//LargeMonsterManager largeMonsterManager = LargeMonsterManager.Instance;
			//MonsterManager monsterManager = MonsterManager.Instance;


			configManager.Initialize();
			localizationManager.Initialize();
			localizationHelper.Initialize();

			imGuiManager.Initialize();
			overlayManager.Initialize();

			//smallMonsterManager.Initialize();
			//largeMonsterManager.Initialize();
			//monsterManager.Initialize();

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
			if(!IsInitialized) return;

			ImGuiManager imGuiManager = ImGuiManager.Instance;

			if(ImGui.Button($"{Constants.MOD_NAME} v{Constants.VERSION}"))
			{
				imGuiManager.IsOpened = !imGuiManager.IsOpened;
			}

			if(imGuiManager.IsOpened)
			{
				ImGuiManager.Instance.Draw();
				OverlayManager.Instance.Draw();
			}
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}
}
