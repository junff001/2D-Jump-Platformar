using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlatform : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            collision.transform.SetParent(transform.parent);
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
