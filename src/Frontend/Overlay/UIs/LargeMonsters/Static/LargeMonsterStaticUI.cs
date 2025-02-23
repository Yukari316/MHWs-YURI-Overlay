using ImGuiNET;

namespace YURI_Overlay;

internal sealed class LargeMonsterStaticUi
{
	private readonly LargeMonster _largeMonster;
	private readonly Func<LargeMonsterStaticUiCustomization> _customizationAccessor;

	private readonly LabelElement _nameLabelElement;
	private readonly LargeMonsterHealthComponent _healthComponent;

	public LargeMonsterStaticUi(LargeMonster largeMonster)
	{
		_largeMonster = largeMonster;
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.largeMonsterUI.@static;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().nameLabel);
		_healthComponent = new LargeMonsterHealthComponent(largeMonster, () => _customizationAccessor().health);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, int locationIndex)
	{
		var customization = _customizationAccessor();
		var settings = customization.settings;

		if(settings.hideDeadOrCaptured && !_largeMonster.IsAlive)
		{
			return;
		}

		var spacing = customization.spacing;

		var anchoredPosition = customization.position;
		var position = AnchorPositionCalculator.Convert(anchoredPosition);

		position.X += spacing.x * locationIndex;
		position.Y += spacing.y * locationIndex;

		_healthComponent.Draw(backgroundDrawList, position);
		_nameLabelElement.Draw(backgroundDrawList, position, 1f, _largeMonster.Name);
	}
}
