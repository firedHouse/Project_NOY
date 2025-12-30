using UnityEngine;
using UnityEngine.UI;

public class OptionUI : Singleton<OptionUI>
{
    [Header("UI 연결")]
    public Slider volumeSlider;
    public GameObject optionPanel;

    private void Start()
    {
        if (volumeSlider != null)
        {
            //이벤트 연결
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        if (optionPanel != null)
        {
            optionPanel.SetActive(false);
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    //현재 볼륨을 가져와서 슬라이더 위치 동기화
    private void OnEnable()
    {
        if (SoundManager.Instance != null && volumeSlider != null)
        {
            volumeSlider.value = SoundManager.Instance.masterVolume;
        }
    }

    //슬라이더를 움직일 때 실행되는 함수
    public void OnVolumeChanged(float value)
    {
        SoundManager.Instance.SetVolume(value);
    }

    //게임 종료
    public void OnClickQuit()
    {
        Debug.Log("게임 종료!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
    }
    public void OnClickClose()
    {
        optionPanel.SetActive(false);
    }
    //열기 버튼 기능
    public void OpenOption()
    {
        if (SoundManager.Instance != null && volumeSlider != null)
        {
            volumeSlider.value = SoundManager.Instance.masterVolume;
        }
        optionPanel.SetActive(true);
    }

}