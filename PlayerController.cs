using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    private bool isOnGround = true;
    public bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio; 
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip coinSound;
    public AudioClip crouchSound;
    public GameManager gameManager;
    private BoxCollider boxCollider;
    private Vector3 originalSize, crouchSize, originalCenter, crouchCenter;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f * gravityModifier, 0);
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        boxCollider = GetComponent<BoxCollider>();
        originalSize = boxCollider.size;
        originalCenter = boxCollider.center;
        crouchSize = new Vector3(originalSize.x, originalSize.y / 2f, originalSize.z);
        crouchCenter = new Vector3(originalCenter.x, originalCenter.y - originalSize.y / 4f, originalCenter.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop(); 
            playerAudio.PlayOneShot(jumpSound, 1.0f); 
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround && !gameOver)
        {
            isOnGround = false;
            boxCollider.size = crouchSize;
            boxCollider.center = crouchCenter;
            playerAnim.SetTrigger("Crouch_b");
            playerAudio.PlayOneShot(crouchSound, 1.0f); 
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isOnGround = true;
            boxCollider.size = originalSize;
            boxCollider.center = originalCenter;
            playerAnim.ResetTrigger("Crouch_b");
        }
    }
       

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play(); 
        }else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            explosionParticle.Play(); 
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop(); 
            playerAudio.PlayOneShot(crashSound, 1.0f);
            gameManager.GameOver();
            GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Coin"))
        {
            playerAudio.PlayOneShot(coinSound, 1.0f);
            gameManager.UpdateScore(1);
            Destroy(collider.gameObject);  
        }
    }
}
