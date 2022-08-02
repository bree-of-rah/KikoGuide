namespace KikoGuide.UI.DutyInfo;

using System;
using KikoGuide.Base;
using KikoGuide.Managers;

class DutyInfoPresenter : IDisposable
{
    public DutyInfoPresenter()
    {
        Service.ClientState.TerritoryChanged += this.OnTerritoryChange;
    }

    public void Dispose()
    {
        Service.ClientState.TerritoryChanged -= this.OnTerritoryChange;
    }

    public bool isVisible = false;
    public Duty? selectedDuty = null;

    /// <summary>
    ///     Handles territory change even and changes the UI state accordingly.
    /// </summary>
    public void OnTerritoryChange(object? sender, ushort e)
    {
        // Skip if the config says to ignore this.
        if (!Service.Configuration.autoOpenDuty) return;

        // Get the player duty and check if it has valid, if so then display it. (Typically on duty enter)
        var playerDuty = DutyManager.GetPlayerDuty();
        if (playerDuty != null || playerDuty?.Bosses != null)
        {
            this.isVisible = true;
            this.selectedDuty = DutyManager.GetPlayerDuty();
        }

        // If the player duty does not have any valid data, hide the UI & clear it (typically on duty exit)
        else
        {
            this.isVisible = false;
            this.selectedDuty = null;
        }
    }
}