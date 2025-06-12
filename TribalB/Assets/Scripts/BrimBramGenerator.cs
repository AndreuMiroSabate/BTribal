using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimBramGenerator : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] List<GameObject> BrimBrams;
    [SerializeField] GameObject BrimBram;

    private List<GameObject> instantiateBrimsBrams = new List<GameObject>();

    [SerializeField] GameObject map;

    private void Awake()
    {

    }
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    public void GenerateBrimBramsRandom()
    {

        for (int i = 0; i < 50; i++)
        {

            float x = Random.Range(-map.transform.localScale.x, map.transform.localScale.x);
            float z = Random.Range(-map.transform.localScale.z, map.transform.localScale.z);
            Vector3 position = new Vector3(x, 1.3f, z);

            GameObject newBrimBram = Instantiate(BrimBram, position, new Quaternion(0, -1f, 0, 1f));
            instantiateBrimsBrams.Add(newBrimBram);
        }
    }

    public void EliminateBrimBrams()
    {
        for (int i = 0; i < instantiateBrimsBrams.Count; i++)
        {
            Destroy(instantiateBrimsBrams[i]);
        }
        instantiateBrimsBrams.Clear();
    }
}
