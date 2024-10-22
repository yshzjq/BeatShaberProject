using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RedNodeSpawner : MonoBehaviour
{
    // 빨강 파랑 노드들 생성기
    public BlueNodeSpawner nodeSpawner;

    // 스크립트에 정보(위치, 방향, 시간)를 입력
    public List<string> nodeInfos;

    // 빨강이나 파랑중 생성할 노드 프리팹
    public GameObject notePrefab;
    public GameObject noteCenterPrefab;

    // 0,0 좌표 부터 2,4 좌표 까지 생성될 위치들 총 15개의 좌표
    public Transform[] pos;

    // 다른 빨강이나 파란 노드 생성기 준비 완료
    public bool isReady = false;

    // 정보(위치 방향 시간)들
    private int[] posType;
    private int[] directionTypeNum;
    private float[] spawnTime;

    // 실제 정보(위치 방향) 값을 Dict 로 할당
    private Dictionary<int, Vector3> direction;
    

    // 현재 시간
    float currentTime = 0;
    // 생성될 노드 순서
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
            // 노드 정보를 추출
            string[] informations = nodeInfo.Split(',');

            // 노드 정보를 할당
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
                    // 가운데 노드
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
