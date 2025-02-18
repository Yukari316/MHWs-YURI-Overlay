using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YURI_Overlay;

internal class ImGuiManager
{
	private static readonly Lazy<ImGuiManager> _lazy = new(() => new ImGuiManager());

	public static ImGuiManager Instance => _lazy.Value;

	public float comboBoxWidth = 100f;

	public bool IsOpened { get => _isOpened; set => _isOpened = value; }
	private bool _isOpened = false;

	private bool IsForceModInfoOpen = true;

	private ImGuiManager() {}

	public ImGuiManager Initialize()
	{
		LogManager.Info("ImGuiManager: Initializing...");

		LogManager.Info("ImGuiManager: Initialized!");

		return this;
	}

	public void Draw()
	{
		try
		{
			if(!IsOpened) return;

			ConfigManager configManager = ConfigManager.Instance;
			LocalizationManager localizationManager = LocalizationManager.Instance;

			var activeLocalization = localizationManager.activeLocalization.data;

			var changed = false;

			ImGui.SetNextWindowPos(Constants.DEFAULT_WINDOW_POSITION, ImGuiCond.FirstUseEver);
			ImGui.SetNextWindowSize(Constants.DEFAULT_WINDOW_SIZE, ImGuiCond.FirstUseEver);

			ImGui.Begin($"{Constants.MOD_NAME} v{Constants.VERSION}", ref _isOpened);

			comboBoxWidth = Constants.COMBOBOX_WIDTH_MULTIPLIER * ImGui.GetWindowSize().X;

			if(IsForceModInfoOpen) ImGui.SetNextItemOpen(true);

			if(ImGui.TreeNode(activeLocalization.imGui.modInfo))
			{
				ImGui.Text(activeLocalization.imGui.madeBy);
				ImGui.SameLine();
				ImGui.TextColored(Constants.MOD_AUTHOR_COLOR, Constants.MOD_AUTHOR);

				if(ImGui.Button(activeLocalization.imGui.nexusMods)) Utils.OpenLink(Constants.NEXUSMODS_LINK);
				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.gitHubRepo)) Utils.OpenLink(Constants.GITHUB_REPO_LINK);

				if(ImGui.Button(activeLocalization.imGui.twitch)) Utils.OpenLink(Constants.TWITCH_LINK);
				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.twitter)) Utils.OpenLink(Constants.TWITTER_LINK);
				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.artStation)) Utils.OpenLink(Constants.ARTSTATION_LINK);

				ImGui.Text(activeLocalization.imGui.donationMessage1);
				ImGui.Text(activeLocalization.imGui.donationMessage2);

				if(ImGui.Button(activeLocalization.imGui.donate)) Utils.OpenLink(Constants.STREAMELEMENTS_TIP_LINK);
				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.payPal)) Utils.OpenLink(Constants.PAYPAL_LINK);
				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.buyMeATea)) Utils.OpenLink(Constants.KOFI_LINK);

				ImGui.TreePop();
			}
			else
			{
				IsForceModInfoOpen = false;
			}

			ImGui.Separator();
			ImGui.NewLine();
			ImGui.Separator();

			changed |= configManager.customization.RenderImGui("config-settings");
			changed |= localizationManager.customization.RenderImGui("localization");
			changed |= configManager.activeConfig.data.globalSettings.RenderImGui("global-settings");
			changed |= configManager.activeConfig.data.largeMonsterUI.RenderImGui("large-monster-ui");

			foreach(var localization in localizationManager.localizations)
			{
				ImGui.Text($"{localization.Key} - {localization.Value.name}");
			}

			if(changed)
			{
				LogManager.Info("ImGuiManager: CONFIGURATION CHANGED");
				configManager.activeConfig.Save();
			}
		}
		catch(Exception e)
		{
			LogManager.Error(e);
		}
	}
}
