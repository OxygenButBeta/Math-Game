using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Assets.Scripts.SharedLibs;

public class HintBox : PanelBehaviour
{
    [SerializeField] TMP_Text hintText;
    [SerializeField] GameObject MainCanvas;

    public override void CheckArgument(object args)
    {
        hintText.text = args as string;
    }
    override public void BeforeOpening()
    {
        MainCanvas.SetActive(false);
    }
    override public void BeforeClosing()
    {
        MainCanvas.SetActive(true);
    }


}
