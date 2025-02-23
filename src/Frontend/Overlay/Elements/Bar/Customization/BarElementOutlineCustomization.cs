using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class BarElementOutlineCustomization : Customization
{
	public bool visible = true;
	public float thickness = 1f;
	public float offset = 0f;

	private int _styleIndex = (int) OutlineStyles.Outside;
	[JsonIgnore]
	public OutlineStyles StyleEnum { get => (OutlineStyles) _styleIndex; set => _styleIndex = (int) value; }
	public string style
	{
		get => LocalizationHelper.Instance.DefaultOutlineStyles[_styleIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.DefaultOutlineStyles, value);
			if(index != -1)
			{
				_styleIndex = index;
			}
		}
	}

	public ColorCustomization Color { get; set; } = new();

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-outline";

		if(ImGui.TreeNode($"{localization.outline}##{customizationName}"))
		{
			isChanged |= ImGui.Checkbox($"{localization.visible}##{customizationName}", ref visible);
			isChanged |= ImGui.DragFloat($"{localization.thickness}##{customizationName}", ref thickness, 0.1f, 0, 1024f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.offset}##{customizationName}", ref offset, 0.1f, -1024f, 1024f, "%.1f");

			isChanged |= ImGui.Combo($"{localization.style}##{customizationName}", ref _styleIndex, localizationHelper.OutlineStyles, localizationHelper.FillDirections.Length);

			isChanged |= Color.RenderImGui(customizationName);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
