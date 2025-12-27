using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class GrowthSkillView : MonoBehaviour
{
    [Header("한마디/정보")] [SerializeField] private Text lineText;
    [SerializeField] private Text infoText;


    private void Start()
    {
        
    }

    private void SetUIComponents()
    {
        
    }
    public void CharacterInfo(CharacterListModel model)
    {
        if (lineText == null || infoText == null)
        {
            Debug.Log("[SkillView] 한마디, 설명 오보젝트가 없습니다.");
            return;
        }

        lineText.text = model.CharacterDialogue;
        infoText.text = model.CharacterInfo;
        Debug.Log($"[SkillView] 한마디 : {model.CharacterDialogue}");
        Debug.Log($"[SkillView] 설명 : {model.CharacterInfo}");
    }
}
