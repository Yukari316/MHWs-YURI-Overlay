using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class OverlayManager
{
	private static readonly Lazy<OverlayManager> _lazy = new(() => new OverlayManager());

	public static OverlayManager Instance => _lazy.Value;

	private LargeMonsterUIManager _largeMonsterUIManager;


	private OverlayManager() { }

	public void Initialize()
	{
		_largeMonsterUIManager = new LargeMonsterUIManager();
	}

	public void Draw()
	{
		ImGui.Begin("YAOI Overlay",
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
