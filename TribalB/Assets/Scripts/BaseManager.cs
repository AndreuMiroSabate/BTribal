using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI baseWood;
    [SerializeField] public TextMeshProUGUI baseStone;
    [SerializeField] public TextMeshProUGUI baseFood;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        baseWood.text = "Wood: "+ gameManager.woodBase.ToString()+"/"+ gameManager.necessaryWood;
        baseStone.text = "Stone: " + gameManager.stoneBase.ToString() + "/" + gameManager.necessaryStone;
        baseFood.text = "Food: " + gameManager.foodBase.ToString() + "/" + gameManager.brimBrams.Count;
    }

    
}
