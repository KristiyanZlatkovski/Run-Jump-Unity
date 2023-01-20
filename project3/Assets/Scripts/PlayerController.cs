using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float gravityModifier;
    public float jumpForce = 10;
    public bool gameOver=false;
    private Animator playeAnim;
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtSplatter;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public bool isOhGround=true;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playeAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        dirtSplatter.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&isOhGround&&!gameOver)
        {
            playeAnim.SetTrigger("Jump_trig");
            isOhGround = false;
            playerRB.AddForce(Vector3.up*jumpForce,ForceMode.Impulse); 
            dirtSplatter.Stop();
        
            playerAudio.PlayOneShot(jumpSound,0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOhGround = true;  
            dirtSplatter.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playeAnim.SetBool("Death_b",true);
            playeAnim.SetInteger("DeathType_int",1);
            explosionParticles.Play();
            playerAudio.PlayOneShot(crashSound,0.5f);
            dirtSplatter.Stop();
        }
    }
}
