using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectResource : MonoBehaviour
{
    private GameManager manager;
    private PlayerMovement player;

    public bool recolecatble;

    private void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
        player = FindAnyObjectByType<PlayerMovement>();
        recolecatble = false;
    }


    public void AddOutline(GameObject resorce)
    {
        
        if(Vector3.Distance(player.rb.position,resorce.transform.position) < 2)
        {
            resorce.AddComponent<Outline>();
            resorce.GetComponent<Outline>().OutlineColor = Color.yellow;
            resorce.GetComponent<Outline>().OutlineWidth = 7.0f;
            manager.recolecting = true;
        }
        else
        {
            resorce.AddComponent<Outline>();
            resorce.GetComponent<Outline>().OutlineColor = Color.grey;
            resorce.GetComponent<Outline>().OutlineWidth = 7.0f;
            manager.recolecting = false;
        }
    }

    public void RemoveOutline(GameObject resorce)
    {
        Outline outline = resorce.GetComponent<Outline>();
        if (outline != null)
        {
            Destroy(outline);
        }
        manager.recolecting = false;
    }

    public void Recolect(GameObject resorce)
    {
        if (player!= null && manager.recolecting==true)
        {
            if (resorce.CompareTag("Tree"))
            {
                manager.woodResources += 1;
                Destroy(resorce);
            }
            if (resorce.CompareTag("Stone"))
            {
                manager.stoneResources += 1;
                Destroy(resorce);
            }
            if (resorce.CompareTag("Food"))
            {
                manager.foodResources += 1;
                Destroy(resorce);
            }
        }
    }

}
