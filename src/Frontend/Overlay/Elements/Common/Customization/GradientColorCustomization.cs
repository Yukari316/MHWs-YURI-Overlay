using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal class GradientColorCustomization : Customization
{
	[JsonIgnore]
	public ColorInfo StartInfo { get; set; } = new();
	public string Start
	{
		get => StartInfo.RgbaHex;
		set => StartInfo.RgbaHex = value;
	}

	[JsonIgnore]
	public ColorInfo EndInfo { get; set; } = new();
	public string End
	{
		get => EndInfo.RgbaHex;
		set => EndInfo.RgbaHex = value;
	}

	public GradientColorCustomization() { }


	public bool RenderImGui(string parentName = "", string name = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-gradient-color";

		//if(ImGui.TreeNode($"{name}##${customizationName}"))
		//{
		//	if(ImGui.TreeNode($"{localization.start}##${customizationName}"))
		//	{
		//		var isStartChanged = ImGui.ColorPicker4($"##${customizationName}-start", ref StartInfo.vector);
		//		isChanged |= isStartChanged;

		//		if(isStartChanged)
		//		{
		//			StartInfo.Vector = StartInfo.vector;
		//		}

		//		ImGui.TreePop();
		//	}


		//	if(ImGui.TreeNode($"{localization.end}##${parentName}"))
		//	{
		//		var isEndChanged = ImGui.ColorPicker4($"##${customizationName}-end", ref EndInfo.vector);
		//		isChanged |= isEndChanged;

		//		if(isEndChanged)
		//		{
		//			EndInfo.Vector = EndInfo.vector;
		//		}

		//		ImGui.TreePop();
		//	}

		//	ImGui.TreePop();
		//}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		return RenderImGui(parentName, localization.Color);
	}
}
