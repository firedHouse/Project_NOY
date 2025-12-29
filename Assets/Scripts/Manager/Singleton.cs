using UnityEngine;

//각종 매니저 상속용 제네릭 기반 클래스
//MonoBehaviour 상속받는 클래스로 제한
//12.29 안전장치 추가
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    //외부 호출용 프로퍼티, 해당 타입의 싱글톤이 없으면 찾아보고, 없을 시 새로 생성 후 설정
    public static T Instance
    {
        get
        {
            //12.29 리팩토링, Awake 전 다른 곳에서 불렀을 때 대비용
            if (_instance == null)
            {
                //씬에 있는 오브젝트를 찾아보기
                _instance = FindFirstObjectByType<T>();

                //그래도 없으면? 배치하세용~
                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T)}가 씬에 없읆,,,");
                }
            }
            return _instance;
        }
    }
    //중복체크 및 연결 기능 구현
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        //이미 인스턴스 있고, 서로 다른 경우 (중복)
        //원본 아니니까(이미 있으니까) 파괴
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}