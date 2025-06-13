
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//Original code by VR with Andreu Youtube Link: https://www.youtube.com/watch?v=kkkmX3_fvfQ

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private SelectResource selectResource;

    [SerializeField] Texture2D hacha;
    [SerializeField] Texture2D pico;
    [SerializeField] Texture2D hazada;
    [SerializeField] Texture2D defoult;

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
            Cursor.SetCursor(defoult, new Vector2(0, 0), CursorMode.Auto);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selectResource.AddOutline(gameObject);
        switch (gameObject.tag)
        {
            case "Tree":
                Cursor.SetCursor(hacha, new Vector2(0, 0), CursorMode.Auto);
                break;
            case "Stone":
                Cursor.SetCursor(pico, new Vector2(0, 0), CursorMode.Auto);
                break;
            case "Food":
                Cursor.SetCursor(hazada, new Vector2(0, 0), CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(defoult, new Vector2(0, 0), CursorMode.Auto);
                break;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        selectResource.RemoveOutline(gameObject);
        Cursor.SetCursor(defoult, new Vector2(0, 0), CursorMode.Auto);
    }
}
