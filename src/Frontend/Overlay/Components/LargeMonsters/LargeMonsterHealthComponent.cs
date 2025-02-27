using System.Numerics;

using ImGuiNET;

namespace YURI_Overlay;

internal class LargeMonsterHealthComponent
{
	private readonly LargeMonster _largeMonster;

	private readonly LabelElement _healthValueLabelElement;
	private readonly LabelElement _healthPercentageLabelElement;
	private readonly BarElement _healthBarElement;

	public LargeMonsterHealthComponent(LargeMonster largeMonster, Func<LargeMonsterHealthComponentCustomization> customizationAccessor)
	{
		_largeMonster = largeMonster;

		_healthValueLabelElement = new LabelElement(() => customizationAccessor().ValueLabel);
		_healthPercentageLabelElement = new LabelElement(() => customizationAccessor().PercentageLabel);
		_healthBarElement = new BarElement(() => customizationAccessor().Bar);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, Vector2 position, float opacityScale = 1f)
	{
		_healthBarElement.Draw(backgroundDrawList, position, _largeMonster.HealthPercentage, opacityScale);
		_healthPercentageLabelElement.Draw(backgroundDrawList, position, opacityScale, _largeMonster.HealthPercentage);
		_healthValueLabelElement.Draw(backgroundDrawList, position, opacityScale, _largeMonster.Health,
			_largeMonster.MaxHealth);
	}
}
