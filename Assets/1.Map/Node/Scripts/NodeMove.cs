using System.Collections;

using UnityEngine;

public class NodeMove : MonoBehaviour
{

    public float buster = 10f;
    public float busterTime = 1f;

    public float speed = -2f;

    public bool isCutted = false;

    
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
        if(isCutted == false)
        {
            GameManager.instance.HitFaild();
            Destroy(gameObject);
        }
        else
        {
            
            Invoke("SelfDestroy2", 2.5f);
        }
        
    }

    public void SelfDestroy2()
    {
        Destroy(gameObject);
    }
}
