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

		// Filter out dead and captured

		foreach(var largeMonsterPair in MonsterManager.Instance.LargeMonsters)
		{
			var largeMonster = largeMonsterPair.Value;

			if(config.settings.hideDeadOrCaptured && !largeMonster.IsAlive)
			{
				continue;
			}

			newLargeMonsters.Add(largeMonster);

			largeMonster.UpdateDistance();
		}

		// Sort


		switch(config.sorting.SortingEnum)
		{
			case Sortings.Id:
				newLargeMonsters.Sort((a, b) =>
				{
					var idComparison = a.Id.CompareTo(b.Id);
					if(idComparison != 0)
					{
						return idComparison;
					}

					var roleIdComparison = a.RoleId.CompareTo(b.RoleId);
					if(roleIdComparison != 0)
					{
						return roleIdComparison;
					}

					var legendaryIdComparison = a.LegendaryId.CompareTo(b.LegendaryId);
					if(legendaryIdComparison != 0)
					{
						return legendaryIdComparison;
					}

					return string.CompareOrdinal(a.Name, b.Name);
				});
				break;
			case Sortings.Health:
				newLargeMonsters.Sort((a, b) =>
				{
					var healthDifference = a.Health - b.Health;
					if(!Utils.IsApproximatelyEqual(healthDifference, 0f))
					{
						return healthDifference < 0f ? -1 : 1;
					}

					return string.CompareOrdinal(a.Name, b.Name);
				});
				break;
			case Sortings.MaxHealth:
				newLargeMonsters.Sort((a, b) =>
				{
					var maxHealthDifference = a.MaxHealth - b.MaxHealth;
					if(!Utils.IsApproximatelyEqual(maxHealthDifference, 0f))
					{
						return maxHealthDifference < 0f ? -1 : 1;
					}

					return string.CompareOrdinal(a.Name, b.Name);
				});
				break;
			case Sortings.HealthPercentage:
				newLargeMonsters.Sort((a, b) =>
				{
					var healthPercentageDifference = a.HealthPercentage - b.HealthPercentage;
					if(!Utils.IsApproximatelyEqual(healthPercentageDifference, 0f))
					{
						return healthPercentageDifference < 0f ? -1 : 1;
					}

					return string.CompareOrdinal(a.Name, b.Name);
				});
				break;

			case Sortings.Distance:
				newLargeMonsters.Sort((a, b) =>
				{
					var distanceDifference = a.Distance - b.Distance;
					if(!Utils.IsApproximatelyEqual(distanceDifference, 0f))
					{
						return distanceDifference < 0f ? -1 : 1;
					}

					return string.CompareOrdinal(a.Name, b.Name);
				});
				break;
			case Sortings.Name:
			default:
				newLargeMonsters.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));
				break;
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
