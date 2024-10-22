using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRotateScripts : MonoBehaviour
{
    public float rotateValue = 0f;
    public float rotatingValue = 0.01f;

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f,rotatingValue,0f));
    }
}
