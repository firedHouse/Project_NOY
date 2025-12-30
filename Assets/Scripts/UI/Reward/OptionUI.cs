using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [Header("볼륨 조절 슬라이더")]
    public Slider volumeSlider;

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            volumeSlider.value = 1.0f;
        }

        //이벤트 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    //슬라이더를 움직일 때 실행되는 함수
    public void OnVolumeChanged(float value)
    {
        SoundManager.Instance.SetVolume(value);
    }
}