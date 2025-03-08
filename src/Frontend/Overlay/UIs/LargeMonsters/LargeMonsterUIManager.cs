using System.Numerics;

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
		var config = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		List<LargeMonster> newLargeMonsters = [];

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(config.Settings.HideDeadOrCaptured && !largeMonster.IsAlive)
			{
				continue;
			}

			newLargeMonsters.Add(largeMonster);

			largeMonster.UpdateDistance();
		}

		// Sort

		if(config.Sorting.ReversedOrder)
		{
			switch(config.Sorting.Type)
			{
				case Sortings.Id:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByIdReversed);
					break;
				case Sortings.Health:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealthReversed);
					break;
				case Sortings.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByMaxHealthReversed);
					break;
				case Sortings.HealthPercentage:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealthPercentageReversed);
					break;
				case Sortings.Distance:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByDistanceReversed);
					break;
				case Sortings.Name:
				default:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByNameReversed);
					break;
			}
		}
		else
		{
			switch(config.Sorting.Type)
			{
				case Sortings.Id:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareById);
					break;
				case Sortings.Health:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealth);
					break;
				case Sortings.MaxHealth:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByMaxHealth);
					break;
				case Sortings.HealthPercentage:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByHealthPercentage);
					break;
				case Sortings.Distance:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByDistance);
					break;
				case Sortings.Name:
				default:
					newLargeMonsters.Sort(LargeMonsterSorting.CompareByName);
					break;
			}
		}

		_staticLargeMonsters = newLargeMonsters;
	}

	private void DrawDynamicUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Dynamic;

		if(!customization.Enabled) return;

		var bar = new BarElement();

		for(var locationIndex = 0; locationIndex < _staticLargeMonsters.Count; locationIndex++)
		{
			var largeMonster = _staticLargeMonsters[locationIndex];

			largeMonster.UpdateScreenPosition();

			if(largeMonster.ScreenPosition == null) continue;

			bar.Draw(backgroundDrawList, (Vector2) largeMonster.ScreenPosition, largeMonster.HealthPercentage);
		}
	}

	private void DrawStaticUi(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		if(!customization.Enabled) return;

		for(var locationIndex = 0; locationIndex < _staticLargeMonsters.Count; locationIndex++)
		{
			_staticLargeMonsters[locationIndex].StaticUi.Draw(backgroundDrawList, locationIndex);
		}
	}
}
