using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float speed;
    private PlayerController playerControllerScript;
    [SerializeField] private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(moveDirection * Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("EndWall"))
        {
            Destroy(gameObject);  
        }
    }
}
