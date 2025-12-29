using UnityEngine;

//1. 오디오 컴포넌트 2개 생성 SFX BGM
//2. 볼륨 조절 기능
//3. 클립 로드하는 기능
public class SoundMananger : MonoBehaviour
{
    [Header("오디오소스 자동연결")]
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    [Header("Volume")]
    public float masterVolume = 1.0f;
    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    private const string SOUND_PATH = "Sound/";


    //리소스 로드
    private AudioClip LoadAudioClip(string fileName)
    {
        string cleanName = fileName;

        //확장자 제거
        if (cleanName.Contains("."))
        {
            cleanName = cleanName.Split('.')[0];
        }

        //최종 경로 리소스랑 마찬가지로 Assets/Resources/Sound/파일명
        string fullPath = $"{SOUND_PATH}{cleanName}";

        AudioClip clip = Resources.Load<AudioClip>(fullPath);
        if (clip == null)
        {
            Debug.LogWarning($"사운드 파일을 찾을 수 없읆,,, {fullPath}");
        }
        return clip;
    }
}
