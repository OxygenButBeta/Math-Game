using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HintBox : PanelBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text hintText;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    public override void CheckArgument(object args)
    {
        if (args is not HintBoxBuilder)
            throw new System.ArgumentException("HintBoxBuilder expected", nameof(args));

        var builder = args as HintBoxBuilder;

        if (builder.LeftButtonType != HintboxButtonType.None)
            leftButton.onClick.AddListener(() =>
            {
                builder.OnLeftButtonClick?.Invoke();
            });
        else
            leftButton.gameObject.SetActive(false);

        if (builder.RightButton != HintboxButtonType.None)
            rightButton.onClick.AddListener(() =>
            {
                builder.OnRightButtonClick?.Invoke();
            });
        else
            rightButton.gameObject.SetActive(false);

    }
    public class HintBoxBuilder
    {
        public string HintTitle;
        public string HintText;
        public HintboxButtonType LeftButtonType;
        public HintboxButtonType RightButton;
        public Action OnLeftButtonClick;
        public Action OnRightButtonClick;

    }
    public enum HintboxButtonType
    {
        None,
        Close,
        Next,
        Previous,
        CloseNext,
        ClosePrevious,
        NextPrevious,
        CloseNextPrevious
    }
}
