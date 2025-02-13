using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LargeMonsterUICustomization : Customization
{
	public LargeMonsterDynamicUICustomization dynamic = new();
	public LargeMonsterStaticUICustomization @static = new();

	public LargeMonsterUICustomization() { }

	public override bool RenderImGui(string parentName = "")
	{
		var localization = LocalizationManager.Instance.activeLocalization.data.imGui;

		var isChanged = false;
		var customizationName = $"{parentName}-large-monster";

		if(ImGui.TreeNode($"{localization.largeMonstersUI}##{customizationName}"))
		{
			isChanged |= dynamic.RenderImGui(customizationName);
			isChanged |= @static.RenderImGui(customizationName);

			ImGui.TreePop();
		}


		return isChanged;
	}
}
