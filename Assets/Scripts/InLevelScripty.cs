using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelScripty : PanelBehaviour
{
    [SerializeField] AnswerField inputfield;

    public override void BeforeOpening()
    {
        inputfield.ResetAnswer();
    }

}
