using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : PanelBehaviour
{
    [SerializeField] int QuestionID;
    Button btn;
    private void Start()
    {
        if (QuestionManager.QuestionCount - 1 < QuestionID)
        {
            Destroy(gameObject);
            return;
        }
        btn = gameObject.GetOrAddComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            Debug.Log("Opening Level " + QuestionID);
            OpenSingleOverride("inlevel", "MainMenu", QuestionID);
            PanelController.SetActive("levels", false);
        });
        Sync();

    }
    public void Sync()
    {
        if (btn is not null)
            btn.interactable = QuestionManager.IsQuestionUnlocked(QuestionID);
    }






}
