using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] AudioController.Audio audioType = AudioController.Audio.Click;
    private void Start()
    {
        gameObject.GetOrAddComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            AudioController.Instance.PlayAudio(audioType);
        });
    }
}
