using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public float jumpForce;
    public bool isUp;
    public int points;
    public GameObject collected;
    public SpriteRenderer srender;
    public BoxCollider2D cbox;


    public int hits;
    //public GameObject box;

    public Animator anim;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetTrigger("hit");

        if ( collision.gameObject.tag == "Player" )
        {
            AudioManager.instance.boxFX.Play();

            if ( isUp )
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce( new Vector2(0f, jumpForce), ForceMode2D.Impulse );
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce( new Vector2(0f, -jumpForce), ForceMode2D.Impulse );
            }

            hits--;

            if ( hits <= 0 )
            {
                //Destroy(box);
                GameController.instance.score += points;
                collected.SetActive(true);
                srender.enabled = false;
                cbox.enabled = false;
                
                Destroy(transform.parent.gameObject, 0.30f);
            }

        }
    }

}
