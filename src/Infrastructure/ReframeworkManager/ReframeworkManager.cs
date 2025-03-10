namespace YURI_Overlay;

internal sealed partial class ReframeworkManager : IDisposable
{
	private static readonly Lazy<ReframeworkManager> Lazy = new(() => new ReframeworkManager());

	public static ReframeworkManager Instance => Lazy.Value;

	public bool IsReframeworkMenuOpen = false;

	private ReframeworkConfigWatcher _reframeworkConfigWatcherInstance;

	private ReframeworkManager() { }

	public void Initialize()
	{
		LogManager.Info("[ReframeworkManager] Initializing...");

		_reframeworkConfigWatcherInstance = new ReframeworkConfigWatcher();

		ReadReframeworkConfig();

		LogManager.Info("[ReframeworkManager] Initialized!");
	}

	public void ReadReframeworkConfig()
	{
		try
		{
			LogManager.Info("[ReframeworkManager] Reading REFramework config...");

			var reframeworkConfig = File.ReadAllText(Constants.ReframeworkConfigWithExtension);

			IsReframeworkMenuOpen = reframeworkConfig.Contains("REFrameworkConfig_MenuOpen=true");

			if(IsReframeworkMenuOpen) ImGuiManager.Instance.IsOpened = true;

			LogManager.Info("[ReframeworkManager] REFramework config reading is done!");
		}
		catch(Exception exception)
		{
			LogManager.Error(exception);
		}
	}

	public void Dispose()
	{
		LogManager.Info("[ReframeworkManager] Disposing...");

		_reframeworkConfigWatcherInstance.Dispose();

		LogManager.Info("[ReframeworkManager] Disposed!");
	}
}
