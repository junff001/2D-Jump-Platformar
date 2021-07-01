using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundlePlatform : MonoBehaviour
{
    public Transform endPos;
    public Transform reStartPos;
    public Transform player;
    public Transform bundleStartPos;
    public float speed;
    private bool onPlayer;
    

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (onPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos.position, Time.deltaTime * speed);
        }

        if (reStartPos.position == player.position)
        {
            onPlayer = false;
            transform.position = bundleStartPos.position;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlayer = true;
            collision.transform.SetParent(transform);
        }
    }
   
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    
}
