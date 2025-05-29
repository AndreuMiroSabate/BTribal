using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodResorcesT;
    [SerializeField] TextMeshProUGUI stoneResourcesT;
    [SerializeField] TextMeshProUGUI foodResourcesT;

    [SerializeField] GameObject player;
    [SerializeField] GameObject montser;

    [SerializeField] public GameObject Base;

    public List<GameObject> brimBrams;

    public int woodResources;
    public int stoneResources;
    public int foodResources;

    public int woodBase;
    public int stoneBase;
    public int foodBase;

    public int necessaryStone;
    public int necessaryWood;

    public bool recolecting;

    private void Awake()
    {
        woodResources = 0;
        stoneResources = 0;
        foodResources = 0;

        foodBase = 0;
        stoneBase = 0;
        woodBase = 0;
        brimBrams.Add(player);

        necessaryStone = 5;
        necessaryWood = 5;

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
