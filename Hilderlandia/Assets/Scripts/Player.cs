using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public FloatingJoystick joystick;

    private bool isJumping;
    //private bool doubleJump;
    private bool isBlowing;

    private Rigidbody2D rbody;
    private   Animator anim; 

    public static Player instance;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }


    private void Move()
    {
        //Uma outra forma de fazer a movimentação do personagem:
        // Vector3 movement = new Vector3( Input.GetAxis("Horizontal"), 0f, 0f );
        // transform.position += movement * Time.deltaTime * speed;

        //Uma forma melhor:
        //float movement = Input.GetAxis("Horizontal");
        float movement = joystick.Horizontal;

        //Controlando a animação de andar:
        if ( movement > 0 ) //Direita:
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3( 0f, 0f, 0f );
            movement = 1;
        }
        else if ( movement < 0 ) //Esquerda:
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3( 0f, 180f, 0f );
            movement = -1;
        }
        else if ( movement == 0 ) //Parado:
        {
            anim.SetBool("walk", false);
        }

        //Movimentando: 
        rbody.velocity = new Vector2( movement * speed, rbody.velocity.y );
    }


    private void Jump()
    {
        if ( Input.GetButtonDown("Jump") && !isBlowing )
        {
            if ( !isJumping )
            {
                AudioManager.instance.jumpFX.Play();
                rbody.AddForce( new Vector2( 0f, jumpForce ), ForceMode2D.Impulse );
                anim.SetBool("jump", true);
                //doubleJump = true;
            }
            /*else if ( doubleJump )
            {
                rbody.AddForce( new Vector2( 0f, jumpForce + 2f ), ForceMode2D.Impulse );
                doubleJump = false;
            }*/
        }
    }

    public void JumpButton()
    {
        if ( !Player.instance.isJumping )
        {
            AudioManager.instance.jumpFX.Play();
            Player.instance.rbody.AddForce( new Vector2( 0f, jumpForce ), ForceMode2D.Impulse );
            // Player.instance.rbody.velocity = Vector2.up * jumpForce;
            Player.instance.anim.SetBool("jump", true);
        }
    }


    //Métodos nátivos da Unity:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.layer == 8 )
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if ( collision.gameObject.tag == "Spike" )
        {
            AudioManager.instance.dieFX.Play();
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

        if ( collision.gameObject.tag == "Saw" )
        {
            AudioManager.instance.dieFX.Play();
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ( collision.gameObject.layer == 8 )
        {
            isJumping = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if ( collider.gameObject.layer == 10 )
        {
            isBlowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if ( collider.gameObject.layer == 10 && isBlowing )
        {
            isBlowing = false;
        }
    }

}
