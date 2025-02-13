using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class LargeMonsterDynamicUI
{
	//private readonly LargeMonster _largeMonster;
	private readonly Func<LargeMonsterDynamicUICustomization> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly LargeMonsterHealthComponent _healthComponent;

	public LargeMonsterDynamicUI(/*LargeMonster largeMonster*/)
	{
		//_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.activeConfig.data.largeMonsterUI.dynamic;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().nameLabel);
		_healthComponent = new LargeMonsterHealthComponent(/*largeMonster, */() => _customizationAccessor().health);
	}

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		var customization = _customizationAccessor();
		var settings = customization.settings;

		//if(settings.hideDeadOrCaptured/* && !_largeMonster.isAlive*/) return;

		//_largeMonster.UpdateDistance();

		//if(settings.maxDistance > 0f && _largeMonster.distance > settings.maxDistance) return;

		//var monsterPosition = _largeMonster.monsterRef.Position;
		//var worldOffset = customization.worldOffset;

		//var targetWorldPosition = new Vector3(
		//	monsterPosition.X + worldOffset.x,
		//	monsterPosition.Y + worldOffset.y,
		//	monsterPosition.Z + worldOffset.z
		//);

		//var isOnScreen = CameraSystem.MainViewport.WorldToScreen(targetWorldPosition, out var screenPosition);

		//if(!isOnScreen) return;

		//LogManager.Info($"{_largeMonster.monsterRef.Name}:{_largeMonster.distance}");

		//var opacityScale = settings.maxDistance > 0f ? (settings.maxDistance - _largeMonster.distance) / settings.maxDistance : 1f;

		//_healthComponent.Draw(backgroundDrawList, screenPosition, opacityScale);
		//_nameLabelElement.Draw(backgroundDrawList, screenPosition, opacityScale, _largeMonster.monsterRef.Name);
	}
}
