using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallalex : MonoBehaviour
{
    public Transform mainCam;

    public Vector2 scale;

    Vector3 beforePosition;

    private void Start()
    {
        beforePosition = mainCam.position;
    }
    
    private void LateUpdate()
    {
        Vector3 delta = mainCam.position - beforePosition;
        transform.Translate(delta.x * scale.x, delta.y * scale.y, 0);
        beforePosition = mainCam.position;
    }
}
