using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal sealed class AnchoredPositionCustomization : Customization
{
	public float x = 0f;
	public float y = 0f;

	[JsonIgnore]
	private int _anchorIndex = (int) Anchors.TopLeft;
	[JsonIgnore]
	public Anchors AnchorEnum { get => (Anchors) _anchorIndex; set => _anchorIndex = (int) value; }
	public string Anchor
	{
		get => LocalizationHelper.Instance.DefaultAnchors[_anchorIndex];
		set
		{
			var index = Array.IndexOf(LocalizationHelper.Instance.DefaultAnchors, value);
			if(index != -1)
			{
				_anchorIndex = index;
			}
		}
	}

	public AnchoredPositionCustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.imGui;
		var localizationHelper = LocalizationHelper.Instance;

		var isChanged = false;
		var customizationName = $"{parentName}-anchored-position";

		if(ImGui.TreeNode($"{localization.position}##${customizationName}"))
		{
			isChanged |= ImGui.DragFloat($"{localization.x}##${customizationName}", ref x, 0.1f, -8192f, 8192f, "%.1f");
			isChanged |= ImGui.DragFloat($"{localization.y}##${customizationName}", ref y, 0.1f, -8192f, 8192f, "%.1f");

			isChanged |= ImGui.Combo($"{localization.anchor}##{customizationName}", ref _anchorIndex, localizationHelper.Anchors, localizationHelper.Anchors.Length);

			ImGui.TreePop();
		}

		return isChanged;
	}
}
