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
                LanguageManager.SetLanguage(Target);
                Debug.Log("Language Changed to " + Target);
            }
        });
        Sync();
    }
    public override void Sync()
    {
        gameObject.GetOrAddComponent<Toggle>().isOn = LanguageManager.currentLanguageName == Target;
    }
}
