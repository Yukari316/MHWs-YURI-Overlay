using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterUiManager : IDisposable
{

	private List<LargeMonster> _staticLargeMonsters = [];

	private System.Timers.Timer _updateTimer;

	public LargeMonsterUiManager()
	{
		Initialize();
	}

	~LargeMonsterUiManager()
	{
		Dispose();
	}

	public void Initialize()
	{
		LogManager.Info("LargeMonsterUIManager: Initializing...");

		_updateTimer = Timers.SetInterval(Update, 100);

		LogManager.Info("LargeMonsterUIManager: Initialized!");
	}

	public void Update()
	{
		//UpdateDynamic();
		UpdateStatic();
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawDynamicUi(backgroundDrawList);
		DrawStaticUi(backgroundDrawList);
	}
	public void Dispose()
	{
		LogManager.Info($"LargeMonsterUIManager: Disposing...");
		_updateTimer.Dispose();
		LogManager.Info($"LargeMonsterUIManager: Disposed!");
	}

	private void UpdateStatic()
	{
		var config = ConfigManager.Instance.ActiveConfig.Data.largeMonsterUI.@static;

		List<LargeMonster> newLargeMonsters = [];

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(config.settings.hideDeadOrCaptured && !largeMonster.IsAlive)
			{
				continue;
			}

			newLargeMonsters.Add(largeMonster);

			largeMonster.UpdatePosition();
			largeMonster.UpdateDistance();
		}

		_staticLargeMonsters = newLargeMonsters;
	}

	private void DrawDynamicUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.largeMonsterUI.dynamic;

		if(!customization.enabled)
		{
			return;
		}

		//foreach(var largeMonsterPair in LargeMonsterManager.Instance.LargeMonsters)
		//{
		//	largeMonsterPair.Value.DrawDynamic(backgroundDrawList);
		//}
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.largeMonsterUI.@static;

		if(!customization.enabled)
		{
			return;
		}

		for(var locationIndex = 0; locationIndex < _staticLargeMonsters.Count; locationIndex++)
		{
			_staticLargeMonsters[locationIndex].StaticUi.Draw(backgroundDrawList, locationIndex);
		}
	}
}
