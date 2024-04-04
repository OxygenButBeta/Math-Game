using Assets.Scripts.SharedLibs.Registry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseLoader : MonoBehaviour
{
    [SerializeField] GameObject Loading;
    RegString IsFirst;
    private void Start()
    {
        IsFirst = new RegString("GGGG");
        if (IsFirst == RegString.Empty)
        {
            IsFirst.Set("1");
            LanguageManager.SetLanguage(Application.systemLanguage);
            Debug.Log("First Time");
        }
        Invoke("ToMainScene", 1.1f);
    }
    private void Update()
    {
        Application.targetFrameRate = 60;
    }
    void EnableLoader() => Loading.SetActive(true);
    void ToMainScene() => SceneManager.LoadScene("Main");
}
