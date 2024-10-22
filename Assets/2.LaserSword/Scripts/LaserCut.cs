
using EzySlice;
using System.Collections;
using UnityEngine;

public class LaserCut : MonoBehaviour
{
    public LayerMask layer;
    public float moveDistance = 25f; // 조각이 이동할 거리
    public float destroyDelay = 3f; // 몇 초 후에 사라질지 설정

    Vector3 oldPos;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, 3, layer))
        {
            if (Vector3.Angle(transform.position - oldPos, hit.transform.up) > 100)
            {
                // 부모와 자식 오브젝트 모두 절단
                SliceObjectAndChildren(hit.transform.gameObject, transform.position, transform.forward);
            }
        }
        oldPos = transform.position;
    }

    void SliceObjectAndChildren(GameObject obj, Vector3 slicePosition, Vector3 sliceDirection)
    {
        // 부모 오브젝트 절단
        SliceObject(obj, slicePosition, sliceDirection);

        // 자식 오브젝트들을 재귀적으로 절단
        foreach (Transform child in obj.transform)
        {
            SliceObject(child.gameObject, slicePosition, sliceDirection);
        }
    }

    void SliceObject(GameObject obj, Vector3 slicePosition, Vector3 sliceDirection)
    {
        MeshFilter meshFilter = obj.GetComponentInChildren<MeshFilter>();

        if (meshFilter == null) return;

        SlicedHull slicedHull = obj.Slice(slicePosition, sliceDirection);

        if (slicedHull != null)
        {
            // 절단된 두 부분을 각각의 게임 오브젝트로 만듦
            GameObject lowerHull = slicedHull.CreateLowerHull(obj, obj.GetComponentInChildren<Renderer>().material);
            GameObject upperHull = slicedHull.CreateUpperHull(obj, obj.GetComponentInChildren<Renderer>().material);

            lowerHull.transform.rotation = obj.GetComponentInChildren<Transform>().transform.rotation;
            upperHull.transform.rotation = obj.GetComponentInChildren<Transform>().transform.rotation;

            lowerHull.transform.position = obj.GetComponentInChildren<Transform>().transform.position;
            upperHull.transform.position = obj.GetComponentInChildren<Transform>().transform.position;


            lowerHull.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
            upperHull.transform.localScale = new Vector3(2.5f, 2.5f, 1f);


            // 각각의 오브젝트에 물리적 요소 추가
            AddHullComponents(lowerHull,Vector3.right);
            AddHullComponents(upperHull, Vector3.left);

            // 조각 이동
            MovePieces(lowerHull, upperHull);

            // 원래 오브젝트 삭제
            Destroy(obj);
        }
    }

    void MovePieces(GameObject lowerHull, GameObject upperHull)
    {
        // 절단된 조각을 왼쪽과 오른쪽으로 이동
        Vector3 direction = (upperHull.transform.position - lowerHull.transform.position).normalized;
        Vector3 leftDirection = new Vector3(-direction.x, 0, -direction.z);  // 왼쪽 방향
        Vector3 rightDirection = direction; // 오른쪽 방향

        // Coroutine을 통해 조각 삭제
        StartCoroutine(DestroyAfterDelay(lowerHull, destroyDelay));
        StartCoroutine(DestroyAfterDelay(upperHull, destroyDelay));
    }

    IEnumerator DestroyAfterDelay(GameObject piece, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(piece);
    }

    void AddHullComponents(GameObject hull,Vector3 direction)
    {
        MeshCollider collider = hull.AddComponent<MeshCollider>();
        collider.convex = true;
        hull.AddComponent<Rigidbody>();
        Rigidbody rg = hull.GetComponent<Rigidbody>();
        rg.AddForce(direction*5f,ForceMode.VelocityChange);
    }
}
