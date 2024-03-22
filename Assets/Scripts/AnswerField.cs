using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerField : MonoBehaviour
{
    [SerializeField] TMP_Text answerenter;
    [SerializeField] int maxlenght;

    public string Answer
    {
        get
        {
            return answerenter.text;
        }
    }
    public void AddDigit(int entereddigit)
    {
        
        if (answerenter.text.Length + 1> maxlenght)
        {

        }

        else
        {
            answerenter.text += entereddigit;
        }
    }

    public void RemoveDigit()
    {
        string lastanswer = "";
        for (int i = 0; i < answerenter.text.Length - 1; i++)
        {
            lastanswer += answerenter.text[i];
        }
        answerenter.text = lastanswer;
    }

    public void ResetAnswer()
    {
        answerenter.text = "";
    }
}
