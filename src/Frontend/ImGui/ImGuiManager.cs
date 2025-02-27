using ImGuiNET;

namespace YURI_Overlay;

internal sealed class ImGuiManager
{
	private static readonly Lazy<ImGuiManager> Lazy = new(() => new ImGuiManager());

	public static ImGuiManager Instance => Lazy.Value;

	public float ComboBoxWidth = 100f;
	public bool IsOpened = false;

	private bool _isForceModInfoOpen = true;

	private ImGuiManager() { }

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
			if(!IsOpened)
			{
				return;
			}

			var configManager = ConfigManager.Instance;
			var localizationManager = LocalizationManager.Instance;

			var activeLocalization = localizationManager.ActiveLocalization.Data;

			var changed = false;

			ImGui.SetNextWindowPos(Constants.DefaultWindowPosition, ImGuiCond.FirstUseEver);
			ImGui.SetNextWindowSize(Constants.DefaultWindowSize, ImGuiCond.FirstUseEver);

			ImGui.Begin($"{Constants.ModName} v{Constants.Version}", ref IsOpened);

			ComboBoxWidth = Constants.ComboboxWidthMultiplier * ImGui.GetWindowSize().X;

			if(_isForceModInfoOpen)
			{
				ImGui.SetNextItemOpen(true);
			}

			if(ImGui.TreeNode(activeLocalization.imGui.modInfo))
			{
				ImGui.Text(activeLocalization.imGui.madeBy);
				ImGui.SameLine();
				ImGui.TextColored(Constants.ModAuthorColor, Constants.ModAuthor);

				if(ImGui.Button(activeLocalization.imGui.nexusMods))
				{
					Utils.OpenLink(Constants.NexusModsLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.gitHubRepo))
				{
					Utils.OpenLink(Constants.GithubRepoLink);
				}

				if(ImGui.Button(activeLocalization.imGui.twitch))
				{
					Utils.OpenLink(Constants.TwitchLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.twitter))
				{
					Utils.OpenLink(Constants.TwitterLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.artStation))
				{
					Utils.OpenLink(Constants.ArtStationLink);
				}

				ImGui.Text(activeLocalization.imGui.donationMessage1);
				ImGui.Text(activeLocalization.imGui.donationMessage2);

				if(ImGui.Button(activeLocalization.imGui.donate))
				{
					Utils.OpenLink(Constants.StreamElementsTipLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.payPal))
				{
					Utils.OpenLink(Constants.PaypalLink);
				}

				ImGui.SameLine();
				if(ImGui.Button(activeLocalization.imGui.buyMeATea))
				{
					Utils.OpenLink(Constants.KofiLink);
				}

				ImGui.TreePop();
			}
			else
			{
				_isForceModInfoOpen = false;
			}

			ImGui.Separator();
			ImGui.NewLine();
			ImGui.Separator();

			changed |= configManager.Customization.RenderImGui("config-settings");
			changed |= localizationManager.Customization.RenderImGui("localization");
			changed |= configManager.ActiveConfig.Data.GlobalSettings.RenderImGui("global-settings");
			changed |= configManager.ActiveConfig.Data.LargeMonsterUI.RenderImGui("large-monster-ui");

			if(changed)
			{
				configManager.ActiveConfig.Save();
			}
		}
		catch(Exception e)
		{
			LogManager.Error(e);
		}
	}
}
