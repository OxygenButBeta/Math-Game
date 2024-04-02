using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : PanelBehaviour
{

    public void Quit()
    {
        OpenPanelSingle("DialogBox", new DialogBox.DialogBoxData
        {
            dialogText = LanguageManager.GetText("QuitGame"),
            OnYes = () => Application.Quit(),
            OnCancel = () => { }
        });
    }
}
