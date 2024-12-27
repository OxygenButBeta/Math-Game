using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelectMenu : PanelBehaviour {
    LevelSelector[] Selecters;

    public override void BeforeOpening() {
        Selecters = GetComponentsInChildren<LevelSelector>();
        Debug.Log("Syncing..");
        foreach (var item in Selecters)
            if (item != null)
                if (!item.IsDestroyed())
                    item?.Sync();
    }
}