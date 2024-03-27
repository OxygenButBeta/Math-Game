using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class InLevelScript : PanelBehaviour
{
    public int CurrentquestionIndex
    {
        get => PlayerPrefs.GetInt("currentquestion", 0);
        set => PlayerPrefs.SetInt("currentquestion", value);
    }
    public Question CurrentQuestion => QuestionManager.Questions[CurrentquestionIndex];
    public static InLevelScript instance { get; private set; }
    RuntimeAnimatorController SelfAnim;
    #region Editor Fields
    [SerializeField] AnswerField inputfield;
    [SerializeField] TMP_Text QuestionText;
    [SerializeField] Animator animator;
    #endregion

    private void Start()
    {
        instance = this;
        SelfAnim = Resources.Load<RuntimeAnimatorController>("inlevel");
    }
    public override void BeforeOpening()
    {
        inputfield.ResetAnswer();
        QuestionText.text = CurrentQuestion.QuestionString;
    }

    public void ApplyAnswer()
    {
        if (inputfield.Answer == CurrentQuestion.Answer)
        {
            CurrentquestionIndex++;
            if (CurrentquestionIndex == QuestionManager.Questions.Count)
            {
                Debug.Log("Soru Bitti");
                CurrentquestionIndex = QuestionManager.Questions.Count - 1;
                return;
            }
            else
            {
                StartCoroutine(NextQuestion());
                IEnumerator NextQuestion()
                {
                    animator.Play("Trans");
                    yield return new WaitForSeconds(0.3f);
                    ReFreshPanel();
                }
            }
        }
        else
        {

            StartCoroutine(WrongAnswer());

            IEnumerator WrongAnswer()
            {
                inputfield.Shake();
                yield return new WaitForSeconds(0.3f);
                inputfield.ResetAnswer();
            }
        }
    }

    public override void ReFreshPanel()
    {

        QuestionText.text = CurrentQuestion.QuestionString;
        inputfield.ResetAnswer();
    }
    public override void AfterOpening()
    {
        StartCoroutine(SwapRuntimeAnimator());
        IEnumerator SwapRuntimeAnimator()
        {
            yield return new WaitForSeconds(0.2f);
            animator.runtimeAnimatorController = SelfAnim;
        }
    }
}
