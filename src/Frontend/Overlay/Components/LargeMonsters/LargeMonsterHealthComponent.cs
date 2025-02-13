using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LargeMonsterHealthComponent
{
	//private readonly LargeMonster _largeMonster;
	private readonly Func<LargeMonsterHealthComponentCustomization> _customizationAccessor;

	private readonly LabelElement _healthValueLabelElement;
	private readonly LabelElement _healthPercentageLabelElement;
	private readonly BarElement _healthBarElement;

	public LargeMonsterHealthComponent(Func<LargeMonsterHealthComponentCustomization> customizationAccessor)
	{
		//_largeMonster = largeMonster;
		_customizationAccessor = customizationAccessor;

		_healthValueLabelElement = new LabelElement(() => _customizationAccessor().healthValueLabel);
		_healthPercentageLabelElement = new LabelElement(() => _customizationAccessor().healthPercentageLabel);
		_healthBarElement = new BarElement(() => _customizationAccessor().healthBar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		//var monsterRef = _largeMonster.monsterRef;

		var health = -1; // monsterRef.Health;
		var maxHealth = -2; // monsterRef.MaxHealth;
		var healthPercentage = health / maxHealth;

		_healthBarElement.Draw(backgroundDrawList, position, healthPercentage, opacityScale);
		_healthPercentageLabelElement.Draw(backgroundDrawList, position, opacityScale, healthPercentage);
		_healthValueLabelElement.Draw(backgroundDrawList, position, opacityScale, health, maxHealth);


	}
}
