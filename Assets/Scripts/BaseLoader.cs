using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseLoader : MonoBehaviour
{
    [SerializeField] GameObject Loading;
    private void Start()
    {
        Invoke("EnableLoader", 0.3f);
        Invoke("ToMainScene", 3f);
    }
    void EnableLoader() => Loading.SetActive(true);
    void ToMainScene() => SceneManager.LoadScene("Main");
}
