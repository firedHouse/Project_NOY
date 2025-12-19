using UnityEngine;
using UnityEngine.EventSystems;

public class UnitIPointerInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Skill skill;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"[UnitIPointerInteraction] 클릭됨");
        //skill.TryUse();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
