using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanguageSelector : LanguageSyncable
{
    [SerializeField] SystemLanguage Target;
    bool IsStart = true;
    private void Start()
    {

        gameObject.GetOrAddComponent<Toggle>().onValueChanged.AddListener((x) =>
        {
            if (x)
            {
                if (x == true && IsStart)
                {
                    IsStart = false;
                    return;
                }
                AudioController.Instance.PlayAudio(AudioController.Audio.Click);
                LanguageManager.SetLanguage(Target);
                Debug.Log("Language Changed to " + Target);
            }
        });
        IsStart = false;
        Sync();
    }
    public override void Sync()
    {
        gameObject.GetOrAddComponent<Toggle>().isOn = LanguageManager.currentLanguageName == Target;
    }
}
