using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogBox : PanelBehaviour
{
    [SerializeField] TMP_Text dialogText;
    [SerializeField] Button yesButton;
    [SerializeField] Button cancelButton;
    static UnityAction OnCancel;
    static UnityAction OnYes;
    public override void CheckArgument(object args)
    {

        if (args is DialogBoxData)
        {
            DialogBoxData data = (DialogBoxData)args;
            dialogText.text = data.dialogText;
            OnCancel = data.OnCancel;
            OnYes = data.OnYes;
        }
        else
            throw new System.Exception("Invalid Argument");
    }

    private void Start()
    {
        yesButton.onClick.AddListener(() =>
        {
            OnYes?.Invoke();
            BackToTheCaller();
        });
        cancelButton.onClick.AddListener(() =>
        {
            OnCancel?.Invoke();
            BackToTheCaller();
        });
    }
    public class DialogBoxData
    {
        public string dialogText;
        public UnityAction OnYes;
        public UnityAction OnCancel;
    }
}
