using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float curTime;
    public float coolTime = 2f;
    public int enemyCnt = 0;
    public int enemyMaxCnt = 0;
    public GameMgr gameMgr;
    public bool isRunning = false;

    void Start()
    {
        gameMgr = GameObject.Find("GameMgr").GetComponent<GameMgr>();
        isRunning = true;
    }

    void Update()
    {
        if (enemyCnt > enemyMaxCnt)
            isRunning = false;

        if (isRunning)
        {
            curTime += Time.deltaTime;
            if(curTime > coolTime) // 1. 만약 쿨타임이 찼다면
            {
                curTime = 0; // 2. curTime = 0으로 초기화
                GameObject enemy = Instantiate(enemyPrefab); // 3. ememyPrefab생성
                enemy.transform.position = transform.position; // 4. enemy위치 이동
                enemy.name = "Enemy_" + enemyCnt; // 5. enemy 이름변경
                enemy.GetComponent<EnemyController>().enemyHp = gameMgr.curEnemyHp;
                enemy.GetComponent<EnemyController>().moveSpeed = gameMgr.curEnemySpeed;
                enemyMaxCnt = gameMgr.stageEnemyCnt;
                enemyCnt++; // 6. enemyCnt 추가
            }
        }
    }

    public void InitEnemyMaker()
    {
        enemyCnt = 0;
        isRunning = true;
        gameMgr.curLV++;
        gameMgr.StageLvUp();
    }
}
