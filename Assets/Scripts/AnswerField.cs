using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerField : MonoBehaviour
{
    [SerializeField] TMP_Text AnswerTMP;
    [SerializeField] int maxlenght;
    [SerializeField] Animator animator;
    [SerializeField] Button ApplyButton;
    private void Start() => ResetAnswer();

    public void Shake() => animator.Play("shake");
    public string Answer => AnswerTMP.text;
    public void AddDigit(int entereddigit)
    {
        if (!(AnswerTMP.text.Length + 1 > maxlenght))
            AnswerTMP.text += entereddigit;
        ApplyButton.interactable = AnswerTMP.text.Length > 0;

    }

    public void RemoveDigit()
    {
        if (AnswerTMP.text.Length > 0)
            AnswerTMP.text = AnswerTMP.text.Substring(0, AnswerTMP.text.Length - 1);
        ApplyButton.interactable = AnswerTMP.text.Length > 0;
    }

    public void ResetAnswer()
    {
        AnswerTMP.text = string.Empty;
        ApplyButton.interactable = AnswerTMP.text.Length > 0;

    }
}
