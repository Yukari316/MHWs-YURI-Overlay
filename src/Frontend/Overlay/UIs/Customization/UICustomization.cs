namespace YURI_Overlay;

internal sealed class UiCustomization : Customization
{
	public LargeMonsterUiCustomization largeMonsterUI = new();

	public UiCustomization() { }

	public bool RenderImGui(string visibleName, string customizationName = "ui")
	{
		var isChanged = false;

		isChanged |= largeMonsterUI.RenderImGui(customizationName);

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		return RenderImGui(parentName);
	}
}
