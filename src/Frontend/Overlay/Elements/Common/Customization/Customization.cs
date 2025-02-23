namespace YURI_Overlay;

internal abstract class Customization
{
	protected Customization() { }

	public abstract bool RenderImGui(string parentName = "");
}
