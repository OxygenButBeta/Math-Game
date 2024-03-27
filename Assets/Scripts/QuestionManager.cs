using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;

public static class QuestionManager
{
    static List<Question> m_questions;
    public static List<Question> Questions
    {
        get
        {
            if (m_questions == null)
                m_questions = JsonConvert.DeserializeObject<List<Question>>(ReadJson());
            return m_questions;

        }
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
