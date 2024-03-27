using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;

[System.Serializable]
public sealed class Question
{
    [JsonProperty] public string QuestionString { get; set; }
    [JsonProperty] public string Answer { get; set; }
    [JsonProperty] public string Formula { get; set; }
}