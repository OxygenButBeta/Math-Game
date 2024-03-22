using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class InLevelScripty : PanelBehaviour
{
    [SerializeField] AnswerField inputfield;
    [SerializeField] TMPro.TMP_Text QuestionText;
    public static InLevelScripty instance { get; private set; }
    int currentquestion = 0;
    List<Question> questions;
    private void Start()
    {
        instance = this;
        questions = JsonConvert.DeserializeObject<List<Question>>(ReadJson());
    }
    public override void BeforeOpening()
    {
        inputfield.ResetAnswer();
        QuestionText.text = questions[currentquestion].QuestionString;
    }
    public static string ReadJson()
    {

        string sFilePath = Path.Combine(Application.streamingAssetsPath, "Questions.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(sFilePath);
            www.SendWebRequest();
            while (!www.isDone) ;
            return www.downloadHandler.text;
        }
        return File.ReadAllText(sFilePath);

    }
    public void ApplyAnswer()
    {
        if (inputfield.Answer == questions[currentquestion].Answer)
        {
            currentquestion++;
            if (currentquestion == questions.Count)
            {
                Debug.Log("Soru Bitti");
            }
            else
            {
                ReFreshPanel();
            }
        }
        else
        {
            Debug.Log("Yanlis Cevap");
        }
    }
    public override void ReFreshPanel()
    {
        QuestionText.text = questions[currentquestion].QuestionString;
        inputfield.ResetAnswer();
    }
}
