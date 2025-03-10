using System.Text.Json.Serialization;

using ImGuiNET;

namespace YURI_Overlay;

internal class GradientColorCustomization : Customization
{
	[JsonIgnore]
	public ColorInfo StartInfo1 { get; set; } = new();
	public string Start1
	{
		get => StartInfo1.RgbaHex;
		set => StartInfo1.RgbaHex = value;
	}

	[JsonIgnore]
	public ColorInfo StartInfo2 { get; set; } = new();
	public string Start2
	{
		get => StartInfo2.RgbaHex;
		set => StartInfo2.RgbaHex = value;
	}

	[JsonIgnore]
	public ColorInfo EndInfo1 { get; set; } = new();
	public string End1
	{
		get => EndInfo1.RgbaHex;
		set => EndInfo1.RgbaHex = value;
	}

	[JsonIgnore]
	public ColorInfo EndInfo2 { get; set; } = new();
	public string End2
	{
		get => EndInfo2.RgbaHex;
		set => EndInfo2.RgbaHex = value;
	}

	public GradientColorCustomization() { }


	public bool RenderImGui(string parentName = "", string name = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		var isChanged = false;
		var customizationName = $"{parentName}-gradient-color";

		if(ImGui.TreeNode($"{name}##${customizationName}"))
		{
			if(ImGui.TreeNode($"{localization.Start1}##${customizationName}"))
			{
				var isStart1Changed = ImGui.ColorPicker4($"##${customizationName}-start1", ref StartInfo1.vector);
				isChanged |= isStart1Changed;

				if(isStart1Changed)
				{
					StartInfo1.Vector = StartInfo1.vector;
				}

				ImGui.TreePop();
			}

			if(ImGui.TreeNode($"{localization.Start2}##${customizationName}"))
			{
				var isStart2Changed = ImGui.ColorPicker4($"##${customizationName}-start2", ref StartInfo2.vector);
				isChanged |= isStart2Changed;

				if(isStart2Changed)
				{
					StartInfo2.Vector = StartInfo2.vector;
				}

				ImGui.TreePop();
			}


			if(ImGui.TreeNode($"{localization.End1}##${parentName}"))
			{
				var isEnd1Changed = ImGui.ColorPicker4($"##${customizationName}-end1", ref EndInfo1.vector);
				isChanged |= isEnd1Changed;

				if(isEnd1Changed)
				{
					EndInfo1.Vector = EndInfo1.vector;
				}

				ImGui.TreePop();
			}

			if(ImGui.TreeNode($"{localization.End2}##${parentName}"))
			{
				var isEnd2Changed = ImGui.ColorPicker4($"##${customizationName}-end2", ref EndInfo2.vector);
				isChanged |= isEnd2Changed;

				if(isEnd2Changed)
				{
					EndInfo2.Vector = EndInfo2.vector;
				}

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return isChanged;
	}

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.ActiveLocalization.Data.ImGui;

		return RenderImGui(parentName, localization.Color);
	}
}
