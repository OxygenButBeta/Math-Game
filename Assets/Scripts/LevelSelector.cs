using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : PanelBehaviour
{
    [SerializeField] Image OutlineField;
    [SerializeField] Color UnlockedClr;
    [SerializeField] Color LockedClr;
    [SerializeField] Color ComplatedClr;
    [SerializeField] TMP_Text LevelText;
    [SerializeField] int QuestionID;
    Button btn;
    private void Start()
    {
        if (QuestionManager.QuestionCount - 1 < QuestionID)
        {
            Destroy(gameObject);
            return;
        }
        LevelText.text = (QuestionID + 1).ToString();
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
        if (QuestionManager.IsQuestionUnlocked(QuestionID))
        {
            btn.interactable = true;
            OutlineField.color = QuestionManager.ComplatedQuestions.Contains(QuestionID) ? ComplatedClr : UnlockedClr;
        }
        else
        {
            btn.interactable = false;
            OutlineField.color = LockedClr;
        }

    }






}
