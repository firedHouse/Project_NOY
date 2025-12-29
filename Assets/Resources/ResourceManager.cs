using UnityEngine;
using System.Collections.Generic;

//CSV에 적힌 파일명을 기반하여 스프라이트를 찾아오는 기능
public class ResourceManager : Singleton<ResourceManager>
{
    //경로 선언
    private const string IMAGE_PATH = "Image/";
    //스프라이트 캐싱
    private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

    //CSV 파일명 받고 스프라이트 리턴
    public Sprite LoadSprite(string fileName)
    {
        //안전장치
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.Log($"{fileName} 스프라이트 데이터 없읆,,,");
            return null;
        }
        //캐싱된거있나확인
        if (spriteCache.ContainsKey(fileName))
        {
            return spriteCache[fileName];
        }

        //확장자 제거
        string cleanName = fileName;
        if (cleanName.Contains(".png"))
        {
            cleanName = cleanName.Replace(".png", "");
        }
        //혹시몰라 jpg도
        else if (cleanName.Contains(".jpg"))
        {
            cleanName = cleanName.Replace(".jpg", "");
        }
        //최종경로
        string fullPath = $"{IMAGE_PATH}{cleanName}";

        //로드
        Sprite sprite = Resources.Load<Sprite>(fullPath);

        if (sprite != null)
        {
            spriteCache.Add(fileName, sprite);
            return sprite;
        }
        else
        {
            Debug.Log($"ResourceManager => 이미지 찾을 수 없읆,,, {fullPath}");
            return null;
        }
    }

    //메모리 정리용 메서드, 필요 시 사용(씬전환 등에서 전신샷 필요없거나 할 때)
    public void ClearCache()
    {
        spriteCache.Clear();
    }
}
