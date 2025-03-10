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
		_customizationAccessor = () => ConfigManager.Instance.ActiveConfig.Data.LargeMonsterUI.Static;

		_nameLabelElement = new LabelElement(() => _customizationAccessor().NameLabel);
		_healthComponent = new LargeMonsterHealthComponent(largeMonster, () => _customizationAccessor().Health);
	}

	public void Draw(ImDrawListPtr backgroundDrawList, int locationIndex)
	{
		var customization = _customizationAccessor();

		var spacing = customization.Spacing;

		var anchoredPosition = customization.Position;

		var positionScaleModifier = ConfigManager.Instance.ActiveConfig.Data.GlobalSettings.GlobalScale.PositionScaleModifier;

		// TODO: Can be cached
		var position = AnchorPositionCalculator.Convert(anchoredPosition, positionScaleModifier);

		position.X += spacing.X * positionScaleModifier * locationIndex;
		position.Y += spacing.Y * positionScaleModifier * locationIndex;

		_healthComponent.Draw(backgroundDrawList, position);
		_nameLabelElement.Draw(backgroundDrawList, position, 1f, _largeMonster.Name);
	}
}
