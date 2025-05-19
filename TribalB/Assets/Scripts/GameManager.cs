using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodResorcesT;
    [SerializeField] TextMeshProUGUI stoneResourcesT;
    [SerializeField] TextMeshProUGUI foodResourcesT;

    public int woodResources;
    public int stoneResources;
    public int foodResources;

    public bool recolecting;

    private void Awake()
    {
        woodResources = 0;
        stoneResources = 0;
        foodResources = 0;
        recolecting = false;
    }

    // Update is called once per frame
    void Update()
    {
        woodResorcesT.text = woodResources.ToString();
        stoneResourcesT.text = stoneResources.ToString();
        foodResourcesT.text = foodResources.ToString();
    }
}
