using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //A plataforma está encostando no personagem?
        if ( collision.gameObject.tag == "Player" )
        {
            //Executando um método após im tempo:
            Invoke("Falling", fallingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Destruindo a plataforma se ela encostar no gameover:
        if ( collider.gameObject.layer == 9 )
        {
            Destroy(gameObject);
        }
    }

    private void Falling()
    {
        AudioManager.instance.fallingplatformFX.Play();
        target.enabled = false; //Para a plataforma cair;
        boxColl.isTrigger = true; //Para a plataforma atravessar o chão;
    }

}
