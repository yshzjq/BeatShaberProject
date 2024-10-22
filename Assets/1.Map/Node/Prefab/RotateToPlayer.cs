using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public Vector3 dir = Vector3.zero;

    void Update()
    {
        transform.LookAt(new Vector3(0f, 1.1176f, -1f),dir); 
    }
}
