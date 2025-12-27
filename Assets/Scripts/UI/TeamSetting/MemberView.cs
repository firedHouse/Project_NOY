using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class GrowthSkillView : MonoBehaviour
{

    [SerializeField] private GameObject MiddlePanel;
    [SerializeField] private GameObject RightPanel;
    
    private void Awake()
    {
        MiddlePanel = GameObject.Find("MiddlePanel");
        RightPanel = GameObject.Find("RightPanel");
    }

    private void SetUIComponents()
    {
        
    }

    public void SetDetailView(bool isActive)
    {
        Debug.Log($"[GrowthView] 상세 패널 활성화 여부 : {isActive}");
        // 우측 패널 활성화 변경
        MiddlePanel.SetActive(isActive);
        RightPanel.SetActive(isActive);
    }
}
