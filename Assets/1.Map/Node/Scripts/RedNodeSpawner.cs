using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedNodeSpawner : MonoBehaviour
{
    // ���� �Ķ� ���� ������
    public BlueNodeSpawner nodeSpawner;

    // ��ũ��Ʈ�� ����(��ġ, ����, �ð�)�� �Է�
    public List<string> nodeInfos;

    // �����̳� �Ķ��� ������ ��� ������
    public GameObject notePrefab;
    public GameObject noteCenterPrefab;

    // 0,0 ��ǥ ���� 2,4 ��ǥ ���� ������ ��ġ�� �� 15���� ��ǥ
    public Transform[] pos;

    // �ٸ� �����̳� �Ķ� ��� ������ �غ� �Ϸ�
    public bool isReady = false;

    // ����(��ġ ���� �ð�)��
    private int[] posType;
    private int[] directionTypeNum;
    private float[] spawnTime;

    // ���� ����(��ġ ����) ���� Dict �� �Ҵ�
    private Dictionary<int, Vector3> direction;
    

    // ���� �ð�
    float currentTime = 0;
    // ������ ��� ����
    int nodeIdx = 0;

    public float startDelayTIme = 1f;

    private void Awake()
    {
        posType = new int[100];
        directionTypeNum = new int[100];
        spawnTime = new float[100];

        direction = new Dictionary<int, Vector3>();

        direction.Add(0, new Vector3(0f, -1f, 0f));
        direction.Add(1, new Vector3(-1f, -1f, -1f));
        direction.Add(2, new Vector3(-1f, 0f, 0f));
        direction.Add(3, new Vector3(-1f, 1f, 1f));
        direction.Add(4, new Vector3(0f, 1f, 0f));
        direction.Add(5, new Vector3(1f, 1f, 1f));
        direction.Add(6, new Vector3(1f, 0f, 0f));
        direction.Add(7, new Vector3(1f, -1f, -1f));

        int idx = 0;

        foreach (string nodeInfo in nodeInfos)
        {
            // ��� ������ ����
            string[] informations = nodeInfo.Split(',');

            // ��� ������ �Ҵ�
            posType[idx] = int.Parse(informations[0]);
            directionTypeNum[idx] = int.Parse(informations[1]);
            spawnTime[idx] = float.Parse(informations[2]);

            idx++;
        }

        isReady = true;

        StartCoroutine("redNodeMusicSpawn");
    }

    IEnumerator redNodeMusicSpawn()
    {
        

        while (true)
        {
            yield return null;
            if (nodeSpawner.isReady == true) break;
        }

        
        yield return new WaitForSeconds(startDelayTIme);
        GameManager.instance.StartCoroutine_StartMusic();


        while (true)
        {

            yield return null;

            currentTime += Time.deltaTime;

            if (spawnTime[nodeIdx] <= currentTime)
            {
                if (directionTypeNum[nodeIdx] == 8)
                {
                    // ��� ���
                    GameObject node = Instantiate(noteCenterPrefab, pos[posType[nodeIdx]].position, Quaternion.identity);
                }
                else
                {
                    GameObject node = Instantiate(notePrefab, pos[posType[nodeIdx]].position, Quaternion.identity);
                    node.GetComponentInChildren<RotateToPlayer>().dir = direction[directionTypeNum[nodeIdx]];
                }

                nodeIdx++;
            }

            if (nodeIdx >= nodeInfos.Count) break;
        }


    }
}
