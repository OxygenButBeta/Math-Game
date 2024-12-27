using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using Assets.Scripts.SharedLibs;

public static class QuestionManager {
    static List<Question> m_questions;
    public static int QuestionCount => Questions.Count;
    private static readonly bool isEncrypted = false;

    public static int[] ComplatedQuestions {
        get {
            return string.IsNullOrEmpty(CompletedQuestionsKey) ? Array.Empty<int>() : Array.ConvertAll(CompletedQuestionsKey.Split(';'), int.Parse);
        }
        private set => CompletedQuestionsKey = string.Join(';', value);
    }

    public static List<Question> Questions {
        get {
            if (m_questions != null)
                return m_questions;
            m_questions =
                JsonConvert.DeserializeObject<List<Question>>(isEncrypted
                    ? ReadJson()
                    : SumCrypto.DecryptString(ReadJson()));

            Debug.Log("Total Number Of Questions " + m_questions.Count);
            return m_questions;
        }
    }

    public static void AddCompletedQuestion(int questionIndex) {
        var completedQuestions = ComplatedQuestions.ToList();
        completedQuestions.Add(questionIndex);
        ComplatedQuestions = completedQuestions.ToArray();
    }

    public static bool IsQuestionUnlocked(int questionIndex) {
        return questionIndex == 0 || ComplatedQuestions.Contains(questionIndex - 1);
    }

    private static string CompletedQuestionsKey {
        get => PlayerPrefs.GetString("ComplatedQuestions", string.Empty);
        set => PlayerPrefs.SetString("ComplatedQuestions", value);
    }

    static string ReadJson() {
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