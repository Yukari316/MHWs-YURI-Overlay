using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal abstract class Customization
{
	protected Customization() { }

	public abstract bool RenderImGui(string parentName = "");
}
