using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    [SerializeField] AnswerField input;

    public void AddDigit(int number)
    {
        input.AddChar(number);
    }

    public void ResetAnswer()
    {
        input.ResetAnswer();
    }

    public void RemoveChar()
    {
        input.RemoveChar();
    }
}
