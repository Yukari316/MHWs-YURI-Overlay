using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal abstract class Updater
{
	protected DateTime lastUpdateTime;

	protected Updater()
	{
		lastUpdateTime = DateTime.Now; //- TimeSpan.FromSeconds(ConfigManager.Instance.activeConfig.data.globalSettings.updateDelay);

		Update();
		UpdatePrio();
	}

	public void CheckAndUpdate()
	{
		var currentTime = DateTime.Now;
		var updateDelay = 30;// TimeSpan.FromSeconds(ConfigManager.Instance.activeConfig.data.globalSettings.updateDelay);

		//if(currentTime - lastUpdateTime < updateDelay) return;

		Update();
	}

	public abstract void Update();
	public abstract void UpdatePrio();
}
