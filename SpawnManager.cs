using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float startDelay = 2;
    [SerializeField] private float repeatRate = 2;
    private PlayerController playerControllerScript;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            GameObject selectedObstacle;
            Vector3 coinPos;

            int i = Random.Range(0, 2);
            if (i == 0)
            {
                selectedObstacle = obstacle1;
                coinPos = new Vector3(25, 5, 0);
            }
            else
            {
                selectedObstacle = obstacle2;
                coinPos = new Vector3(25, 1, 0);
            }
                
            
            Instantiate(selectedObstacle, spawnPos, selectedObstacle.transform.rotation); 
            Instantiate(coinPrefab, coinPos, coinPrefab.transform.rotation);
        }
    }
}
