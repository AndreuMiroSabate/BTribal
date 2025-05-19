
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//Original code by VR with Andreu Youtube Link: https://www.youtube.com/watch?v=kkkmX3_fvfQ

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private SelectResource selectResource;

    private void Awake()
    {
      selectResource = GetComponent<SelectResource>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            selectResource.Recolect(gameObject);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectResource.AddOutline(gameObject);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        selectResource.RemoveOutline(gameObject);
    }
}
