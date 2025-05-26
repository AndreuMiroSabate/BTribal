using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResorcesGenerate : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] List<GameObject> ResourcesList;
    private Dictionary<string,GameObject> resources = new Dictionary<string,GameObject>();

    private List<GameObject> instantiateResources = new List<GameObject>();

    [SerializeField] GameObject map;

    private void Awake()
    {
        foreach (var resourceList in ResourcesList)
        {
            if (!resources.ContainsKey(resourceList.name))
            {
                resources.Add(resourceList.name, resourceList.gameObject);
            }
        }
    }
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    public void GenerateResourcesRandom()
    {
        List<string> keys = new List<string>(resources.Keys);

        for (int i = 0; i < 500; i++)
        {
            // 1. Elegir una clave aleatoria
            string selcetedKey = keys[Random.Range(0, keys.Count)];
            GameObject prefab = resources[selcetedKey];

            // 2. Generar una posición aleatoria dentro del área
            float x = Random.Range(-map.transform.localScale.x, map.transform.localScale.x);
            float z = Random.Range(-map.transform.localScale.z, map.transform.localScale.z);
            Vector3 position = new Vector3(x, 0, z);

            // 3. Instanciar
            GameObject newResource = Instantiate(prefab, position, Quaternion.identity);
            instantiateResources.Add(newResource);
        }
    }

    public void EliminateResources()
    {
        for(int i = 0; i< instantiateResources.Count; i++)
        {
            Destroy(instantiateResources[i]);
        }
        instantiateResources.Clear();
    }
}

