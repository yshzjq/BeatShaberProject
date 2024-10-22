
using EzySlice;
using System.Collections;
using UnityEngine;

public class LaserCut : MonoBehaviour
{
    public LayerMask layer;
    public float moveDistance = 25f; // ������ �̵��� �Ÿ�
    public float destroyDelay = 3f; // �� �� �Ŀ� ������� ����

    Vector3 oldPos;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, 3, layer))
        {
            if (Vector3.Angle(transform.position - oldPos, hit.transform.up) > 100)
            {
                // �θ�� �ڽ� ������Ʈ ��� ����
                SliceObjectAndChildren(hit.transform.gameObject, transform.position, transform.forward);
            }
        }
        oldPos = transform.position;
    }

    void SliceObjectAndChildren(GameObject obj, Vector3 slicePosition, Vector3 sliceDirection)
    {
        // �θ� ������Ʈ ����
        SliceObject(obj, slicePosition, sliceDirection);

        // �ڽ� ������Ʈ���� ��������� ����
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
            // ���ܵ� �� �κ��� ������ ���� ������Ʈ�� ����
            GameObject lowerHull = slicedHull.CreateLowerHull(obj, obj.GetComponentInChildren<Renderer>().material);
            GameObject upperHull = slicedHull.CreateUpperHull(obj, obj.GetComponentInChildren<Renderer>().material);

            lowerHull.transform.rotation = obj.GetComponentInChildren<Transform>().transform.rotation;
            upperHull.transform.rotation = obj.GetComponentInChildren<Transform>().transform.rotation;

            lowerHull.transform.position = obj.GetComponentInChildren<Transform>().transform.position;
            upperHull.transform.position = obj.GetComponentInChildren<Transform>().transform.position;


            lowerHull.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
            upperHull.transform.localScale = new Vector3(2.5f, 2.5f, 1f);


            // ������ ������Ʈ�� ������ ��� �߰�
            AddHullComponents(lowerHull,Vector3.right);
            AddHullComponents(upperHull, Vector3.left);

            // ���� �̵�
            MovePieces(lowerHull, upperHull);

            // ���� ������Ʈ ����
            Destroy(obj);
        }
    }

    void MovePieces(GameObject lowerHull, GameObject upperHull)
    {
        // ���ܵ� ������ ���ʰ� ���������� �̵�
        Vector3 direction = (upperHull.transform.position - lowerHull.transform.position).normalized;
        Vector3 leftDirection = new Vector3(-direction.x, 0, -direction.z);  // ���� ����
        Vector3 rightDirection = direction; // ������ ����

        // Coroutine�� ���� ���� ����
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
