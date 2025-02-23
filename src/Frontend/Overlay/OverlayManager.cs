using ImGuiNET;

namespace YURI_Overlay;

internal sealed class OverlayManager
{
	private static readonly Lazy<OverlayManager> _lazy = new(() => new OverlayManager());

	public static OverlayManager Instance => _lazy.Value;

	private LargeMonsterUiManager _largeMonsterUIManager;


	private OverlayManager() { }

	public void Initialize()
	{
		_largeMonsterUIManager = new LargeMonsterUiManager();
	}

	public void Draw()
	{
		ImGui.Begin("YURI Overlay",
			ImGuiWindowFlags.NoMove
			| ImGuiWindowFlags.NoBackground
			| ImGuiWindowFlags.NoCollapse
			| ImGuiWindowFlags.NoResize
			| ImGuiWindowFlags.NoTitleBar
			| ImGuiWindowFlags.NoSavedSettings
			| ImGuiWindowFlags.NoScrollbar
		);

		var backgroundDrawList = ImGui.GetBackgroundDrawList();

		_largeMonsterUIManager.Draw(backgroundDrawList);

		ImGui.End();
	}
}
