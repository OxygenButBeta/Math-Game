using Newtonsoft.Json;

[System.Serializable]
public sealed class Question
{
    [JsonProperty] public string QuestionString { get; set; }
    [JsonProperty] public string Answer { get; set; }
    [JsonProperty] public string Formula { get; set; }
}