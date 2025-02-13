using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUI
{
	//private LargeMonster _largeMonster;
	private Func<LargeMonsterStaticUICustomization> _customizationAccessor;

	private LabelElement _nameLabelElement;
	private LargeMonsterHealthComponent _healthComponent;

	public LargeMonsterStaticUI(/*LargeMonster largeMonster*/)
	{
		//_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.activeConfig.data.largeMonsterUI.@static;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().nameLabel);
		_healthComponent = new LargeMonsterHealthComponent(/*largeMonster, */() => _customizationAccessor().health);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, int locationIndex)
	{
		var customization = _customizationAccessor();
		var settings = customization.settings;

		//if(settings.hideDeadOrCaptured && !_largeMonster.isAlive) return;

		//var spacing = customization.spacing;

		//var anchoredPosition = customization.position;
		//var position = AnchorPositionCalculator.Convert(anchoredPosition);

		//position.X += spacing.x * locationIndex;
		//position.Y += spacing.y * locationIndex;

		//_healthComponent.Draw(backgroundDrawList, position);
		//_nameLabelElement.Draw(backgroundDrawList, position, 1f, _largeMonster.monsterRef.Name);
	}
}
