using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float jumpForce;

    private Animator anim;
    private BoxCollider2D cbox;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cbox = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            anim.SetTrigger("jump");
            AudioManager.instance.trampolineFX.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce( new Vector2(0f, jumpForce), ForceMode2D.Impulse );
        }

        if ( collision.gameObject.layer == 6 )
        {
            cbox.isTrigger = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.gameObject.layer == 6 )
        {
            cbox.isTrigger = false;
        }
    }



}
