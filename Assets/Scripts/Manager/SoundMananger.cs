using UnityEngine;

//1. 오디오 컴포넌트 2개 생성 SFX BGM
//2. 볼륨 조절 기능
//3. 클립 로드하는 기능
public class SoundManager : Singleton<SoundManager>
{
    [Header("오디오소스 자동연결")]
    private AudioSource bgmSource;
    private AudioSource sfxSource;


    [Range(0f, 1f)]
    [SerializeField]
    private float masterVolume = 1.0f;
    private const string SOUND_PATH = "Sound/";

    protected override void Awake()
    {
        base.Awake();
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.volume = masterVolume; //초기 볼륨 적용

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
    }

    private void Start()
    {
        //게임 시작 시 Lobby BGM 재생
        PlayBGM("Lobby");
    }

    //값을 바꿨을 때만 실행
    private void OnValidate()
    {
        //에디터에서 슬라이더를 드래그할 때 실시간 반영
        if (bgmSource != null)
        {
            bgmSource.volume = masterVolume;
        }
    }

    public void PlayBGM(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return;
        }

        AudioClip clip = LoadAudioClip(fileName);
        if (clip == null)
        {
            return;
        }

        //이미 같은 곡이 재생 중이라면 끊지 않고 유지
        if (bgmSource.clip == clip && bgmSource.isPlaying)
        {
            return;
        }
        bgmSource.clip = clip;
        bgmSource.volume = masterVolume;
        bgmSource.Play();
    }

    
    public void PlaySFX(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return;
        }
        AudioClip clip = LoadAudioClip(fileName);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, masterVolume);
        }
    }

    //볼륨조절
    public void SetVolume(float vol)
    {
        masterVolume = vol;
        if (bgmSource != null)
        {
            bgmSource.volume = masterVolume;
        }
    }

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
