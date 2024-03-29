using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : PanelBehaviour
{

    LevelSelector[] Selecters;
    private void Start() => Selecters = GetComponentsInChildren<LevelSelector>();

    public override void BeforeOpening()
    {
        Debug.Log("Syncing..");
        foreach (var item in Selecters)
            item.Sync();
    }
}
