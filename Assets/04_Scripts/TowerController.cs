using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int attackPower;
    public float attackCurTime;
    public float attackSpeed;
    public GameObject targetEnemy;
    public GameObject bulletPrefab;
    public GameObject muzzleEffect;
    public Animator anim;

    public enum TOWERSTATE
    {
        IDLE = 0,
        ATTACK,
        UPGRADING,
        NONE
    }
    public TOWERSTATE towerState;
    public EnemyDetecting enemyDetecting;

    void Start()
    {
        towerState = TOWERSTATE.IDLE;
        enemyDetecting = GetComponentInChildren<EnemyDetecting>();
    }

    void Update()
    {
        switch (towerState)
        {
            case TOWERSTATE.IDLE:
                if(enemyDetecting.enemies.Count > 0 && targetEnemy == null)
                {
                    targetEnemy = enemyDetecting.enemies[0];
                    towerState = TOWERSTATE.ATTACK;
                }
                break;
            case TOWERSTATE.ATTACK:
                if(targetEnemy != null)
                {
                    transform.LookAt(targetEnemy.transform);
                    Vector3 dir = transform.localRotation.eulerAngles;
                    dir.x = 0;
                    transform.localRotation = Quaternion.Euler(dir);
                    attackCurTime += Time.deltaTime;
                    if(attackCurTime > attackSpeed)
                    {
                        Debug.Log("공격");
                        attackCurTime = 0; // 0으로 초기화
                        GameObject bullet = Instantiate(bulletPrefab);
                        bullet.transform.position = transform.position;
                        bullet.GetComponent<BulletController>().target = targetEnemy;
                        bullet.GetComponent<BulletController>().bulletDamage = attackPower;
                    }
                }
                else
                {
                    attackCurTime = 0;
                    towerState = TOWERSTATE.IDLE;
                }
                break;
            case TOWERSTATE.UPGRADING:
                //Todo
                //1. 업그레이드중에 애니메이션 재생
                //2. 업그레이드중에 다른 행동 못하게 하기
                break;
            case TOWERSTATE.NONE:
                break;
            default:
                break;
        }

    }
}
