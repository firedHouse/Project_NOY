using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

//배틀로그 출력할 매니저
public class BattleLogManager : MonoBehaviour
{
    public static BattleLogManager Instance;

    [Header("UI 연결(TXT_")]
    public Text logText;

    [Header("줄 설정")]
    public int maxLines = 5;

    //queue사용
    private Queue<string> logQueue = new Queue<string>();

    //스트링빌더 슛
    private StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //시작 시 로그 초기화
        logText.text = "";
    }

    public void AddLog(string message)
    {
        logQueue.Enqueue(message);
        if (logQueue.Count > maxLines)
        {
            logQueue.Dequeue();
        }
        UpdateLogUI();
    }
    private void UpdateLogUI()
    {
        //지우고
        sb.Clear();
        //다시 채우고
        foreach (string line in logQueue)
        {
            sb.AppendLine(line);
        }
        //전달
        logText.text = sb.ToString();
    }
}
