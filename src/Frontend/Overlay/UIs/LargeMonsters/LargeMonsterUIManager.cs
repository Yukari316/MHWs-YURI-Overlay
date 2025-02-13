using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class LargeMonsterUIManager
{
	public LargeMonsterUIManager() { }

	public void Draw(ImDrawListPtr backgroundDrawList)
	{
		DrawDynamicUI(backgroundDrawList);
		DrawStaticUI(backgroundDrawList);
	}

	private void DrawDynamicUI(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.activeConfig.data.largeMonsterUI.dynamic;

		if(!customization.enabled) return;

		//foreach(var largeMonsterPair in LargeMonsterManager.Instance.LargeMonsters)
		//{
		//	largeMonsterPair.Value.DrawDynamic(backgroundDrawList);
		//}
	}

	private void DrawStaticUI(ImDrawListPtr backgroundDrawList)
	{
		var customization = ConfigManager.Instance.activeConfig.data.largeMonsterUI.@static;

		if(!customization.enabled) return;

		//for(var locationIndex = 0; locationIndex < LargeMonsterManager.Instance.LargeMonsters.Count; locationIndex++)
		//{
		//	LargeMonsterManager.Instance.LargeMonsters.ElementAt(locationIndex).Value.DrawStatic(backgroundDrawList, locationIndex);
		//}
	}
}
