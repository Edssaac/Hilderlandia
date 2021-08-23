using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiderPlatform : MonoBehaviour
{
    public float speed;
    public float moveTime;

    private bool dirRight = true; //Começando a se mover pela direita;
    private float timer;


    // Update is called once per frame
    void Update()
    {
        if (dirRight) //Movendo a plataforma para direita:
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else //Movendo a plataforma para a esquerda:
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;

        if (timer >= moveTime)
        {
            dirRight = !dirRight;
            timer = 0;
        }
    }

    //Fazendo com que o personagem não caia da plataforma ao pisar nela:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    //Parando de fazer o personagem ficar automaticamente na plataforma:
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player" )
        {
            collision.gameObject.transform.parent = null;
        }
    }

}
