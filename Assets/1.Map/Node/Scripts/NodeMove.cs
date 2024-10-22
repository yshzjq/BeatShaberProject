using System.Collections;

using UnityEngine;

public class NodeMove : MonoBehaviour
{

    public float buster = 10f;
    public float busterTime = 1f;

    public float speed = -2f;

    IEnumerator Start()
    {
        Invoke("SelfDestroy", 3.1f);
        speed *= buster;
        yield return new WaitForSeconds(busterTime);
        speed /= buster;


    }
    void Update()
    {
        transform.position +=  Time.deltaTime * transform.forward * speed;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
