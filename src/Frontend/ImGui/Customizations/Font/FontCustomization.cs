using ImGuiNET;

namespace YURI_Overlay;

internal sealed class FontCustomization
{
	public float FontSize = 17f;
	public int HorizontalOversample = 2;
	public int VerticalOversample = 2;

	public FontCustomization() { }

	public bool RenderImGui()
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var changed = false;

		if(ImGui.TreeNode(localization.Font))
		{

			ImGui.Text(localization.AnyChangesToFontRequireGameRestart);

			changed |= ImGui.DragFloat(localization.FontSize, ref FontSize, 0.1f, 1f, 128f, "%.1f");
			changed |= ImGui.SliderInt(localization.HorizontalOversample, ref HorizontalOversample, 0, 8);
			changed |= ImGui.SliderInt(localization.VerticalOversample, ref VerticalOversample, 0, 8);

			ImGui.TreePop();
		}

		return changed;
	}
}