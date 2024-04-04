using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using Assets.Scripts.SharedLibs;

public static class QuestionManager
{
    static List<Question> m_questions;
    public static int QuestionCount => Questions.Count;
    private static bool isEncrypted = false;
    public static int[] ComplatedQuestions
    {
        get
        {
            if (string.IsNullOrEmpty(ComplatedQuestionsKey))
                return new int[0];

            return Array.ConvertAll(ComplatedQuestionsKey.Split(';'), int.Parse);
        }
        set
        {
            ComplatedQuestionsKey = string.Join(';', value);
        }
    }
    public static List<Question> Questions
    {
        get
        {
            if (m_questions == null)
            {
                if (isEncrypted)
                    m_questions = JsonConvert.DeserializeObject<List<Question>>(ReadJson());
                else
                    m_questions = JsonConvert.DeserializeObject<List<Question>>(SumCrypto.DecryptString(ReadJson()));

                Debug.Log("Total Number Of Questions " + m_questions.Count);
            }
            return m_questions;

        }
    }
    public static void AddCompletedQuestion(int questionIndex)
    {
        var complatedQuestions = ComplatedQuestions.ToList();
        complatedQuestions.Add(questionIndex);
        ComplatedQuestions = complatedQuestions.ToArray();
    }
    public static bool IsQuestionUnlocked(int questionIndex)
    {
        if (questionIndex == 0) return true;
        return ComplatedQuestions.Contains(questionIndex - 1);
    }
    private static string ComplatedQuestionsKey
    {
        get => PlayerPrefs.GetString("ComplatedQuestions", string.Empty);
        set => PlayerPrefs.SetString("ComplatedQuestions", value);
    }
    static string ReadJson()
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
}
