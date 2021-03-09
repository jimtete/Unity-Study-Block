using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private GameManager gameManager;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem runningParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip coinSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    private Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = Vector3.up;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            JumpForce();
            
        }
    }

    
    void JumpForce()
    {
        playerAudio.PlayOneShot(jumpSound, 1.0f);
        playerRb.AddForce(temp * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        runningParticle.Stop();

    }

    public void RestartGame()
    {

        playerRb.mass = 75.0f;
        jumpForce = 750.0f;
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            runningParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
            runningParticle.Stop();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            
            gameOver = true;
            playerAudio.PlayOneShot(crashSound, 1.0f);
            
            
        } else if (collision.gameObject.CompareTag("Coin"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(coinSound);
            Destroy(collision.gameObject);
            gameManager.UpdateScore(10);
        }



    }
}
