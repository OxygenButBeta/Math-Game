using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SumStudio;
using Assets.Scripts.SharedLibs.Language;

[System.Serializable]
public static class LanguageManager
{
    [System.Serializable]
    public class LanguagePackage
    {
        [JsonProperty] public SystemLanguage PackageLanguage { get; set; }
        [JsonProperty] public Dictionary<string, string> PackageStrings { get; set; }
        public LanguagePackage() => PackageStrings = new Dictionary<string, string>();
    }

    #region Fields
    static List<LanguagePackage> LanguagePackages;
    static bool isInitialized = false;
    #endregion

    #region Properties
    public static SystemLanguage currentLanguageName
    {
        get
        {
            Initialize();
            return LanguagePackages[currentLanguageIndex].PackageLanguage;
        }
    }
    public static LanguagePackage CurrentLanguage { get { return LanguagePackages[currentLanguageIndex]; } }
    public static int currentLanguageIndex { get; private set; } = 0; // en-EN
    public static SystemLanguage SavedLanguage
    {
        get => (SystemLanguage)PlayerPrefs.GetInt("CurrentLanguage", 10);
        set => PlayerPrefs.SetInt("CurrentLanguage", (int)value);
    }
    #endregion

    public static void Initialize()
    {
        if (isInitialized)
            return;
        LanguagePackages = JsonConvert.DeserializeObject<List<LanguagePackage>>(IO.ReadJson("LanguagePackage.json"));
        if (LanguagePackages == null)
            throw new NullReferenceException("Language packages are null");
        Debug.Log("Saved " +SavedLanguage);
        isInitialized = true;
        SetLanguage(SavedLanguage);
        FetchAllInScene();
    }
    public static string GetText(string key)
    {
        Initialize();

        if (CurrentLanguage.PackageStrings.TryGetValue(key, out string text))
            return text;
        else
            return "Could not find the key: " + key + " in the language package.";

    }
    public static void SetLanguage(int LanguageIndex)
    {
        if (LanguageIndex > LanguagePackages.Count || LanguageIndex < 0)
            throw new IndexOutOfRangeException("Language index is out of range");

        currentLanguageIndex = LanguageIndex;
        SavedLanguage = currentLanguageName;
        FetchAllInScene();
    }
    static void FetchAllInScene() => GameObject.FindObjectsOfType<LanguageSyncable>(true).FastEach(x => x.Sync());
    public static void SetLanguage(SystemLanguage language)
    {
        Initialize();

        currentLanguageIndex = LanguagePackages.FindIndex(x => x.PackageLanguage == language);
        if (currentLanguageIndex == -1)
        {
            currentLanguageIndex = 0;
            Debug.Log("Could not find the language: " + language + " in the language packages.");
        }
        SavedLanguage = language;
        FetchAllInScene();
    }
}
public class LanguageSyncable : MonoBehaviour
{
    public virtual void Sync() { }
}