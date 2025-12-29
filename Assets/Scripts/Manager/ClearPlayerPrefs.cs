using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : MonoBehaviour
{
    [MenuItem("Tools/PlayerPrefs 초기화")]
    private static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs 초기화");
        PlayerPrefs.SetInt("OutGameShilling", 5000);
        Debug.Log($"[ShillingPresenter] 해금 테스트시 실링 소비를 위해 실링 값 조정");
    }
}
