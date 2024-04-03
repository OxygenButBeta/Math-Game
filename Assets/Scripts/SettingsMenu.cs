using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : PanelBehaviour
{
    public override void BeforeOpening() => AudioController.Instance.SyncSliders();
    public override void BeforeClosing() => AudioController.Instance.SyncSliders();
}
