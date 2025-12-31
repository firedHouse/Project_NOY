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
    public int maxCharsPerLine = 18;//글자수제한

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
        while (message.Length > maxCharsPerLine)
        {
            //18글자 자르기
            string part = message.Substring(0, maxCharsPerLine);
            EnqueueMessage(part);
            
            //앞 제거하고 나머지부터로 지정
            message = message.Substring(maxCharsPerLine);
        }

        if (!string.IsNullOrEmpty(message))
        {
            //나머지 출력
            EnqueueMessage(message);
        }
        UpdateLogUI();
    }

    //큐 등록 + 최대 줄 수 관리
    private void EnqueueMessage(string msg)
    {
        logQueue.Enqueue(msg);

        //최대 줄 수를 넘으면 가장 오래된 로그 삭제
        while (logQueue.Count > maxLines)
        {
            logQueue.Dequeue();
        }
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
