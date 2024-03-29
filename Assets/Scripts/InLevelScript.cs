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
    [SerializeField] TMP_Text LevelText;
    #endregion

    private void Start()
    {
        instance = this;
        SelfAnim = Resources.Load<RuntimeAnimatorController>("inlevel");
    }
    public override void BeforeOpening()
    {
        inputfield.ResetAnswer();
        if (CurrentquestionIndex > QuestionManager.QuestionCount)
            CurrentquestionIndex = 0;

        QuestionText.text = CurrentQuestion.QuestionString;
    }

    public void ApplyAnswer()
    {
        if (inputfield.Answer == CurrentQuestion.Answer)
        {
            QuestionManager.AddCompletedQuestion(CurrentquestionIndex);
            CurrentquestionIndex++;
            if (CurrentquestionIndex == QuestionManager.Questions.Count)
            {
                Debug.Log("Soru Bitti");
                CurrentquestionIndex = QuestionManager.Questions.Count - 1;
                return;
            }
            else
            {
                QuestionManager.AddCompletedQuestion(CurrentquestionIndex);
                StartCoroutine(NextQuestion());

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
    IEnumerator NextQuestion()
    {
        animator.Play("Trans");
        yield return new WaitForSeconds(0.3f);
        ReFreshPanel();
    }
    public override void CheckArgument(object args)
    {
        CurrentquestionIndex = (int)args;
        Debug.Log("Target Level " + CurrentquestionIndex);
        ReFreshPanel();
    }
    public override void ReFreshPanel()
    {
        LevelText.text = $"Question {CurrentquestionIndex + 1}";
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
