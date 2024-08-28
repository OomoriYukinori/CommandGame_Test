using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManeger : MonoBehaviour
{
    bool attack;
    bool guard;
    bool Items;

    int playerHp;
    int enemyHp;

    StatusManeger status;

    UI_Maneger uI_Maneger;
    UI_Prefab uI_Prefab;

    GameObject[] enemy;
    // Start is called before the first frame update
    void Start()
    {
        uI_Maneger = GetComponent<UI_Maneger>();
        uI_Prefab = GetComponent<UI_Prefab>();

        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        enemyHp = enemy[0].GetComponent<StatusManeger>().nowHp;

        status = this.GetComponent<StatusManeger>();

        Escape();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public void Attack()
    {
        attack = true;
        StartCoroutine(turn());
    }public void Skill()
    {
        if (status.nowSp > 0)
        {
            guard = true;
        }
        else Debug.Log("SPが足りない！");
    }
    public void Item()
    {
        if (status.nowItem > 0)
        {
            Items = true;
        }
        else Debug.Log("Itemが足りない！");
    }
    public void Escape()
    {
        Debug.Log("家で休んで体力が全回復した！");
        playerHp = status.maxHp;
    }

    IEnumerator turn()
    {
        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(false);
        }
        if (attack)
        {
            Debug.Log("あなたの攻撃!");
            enemyHp--;
            Debug.Log("敵に1のダメージ");
        }

        if (Items && enemyHp > 0)
        {
            Debug.Log("HPが回復した!");
            playerHp += 2;
        }

        if (enemyHp > 0)
        {
            Debug.Log("敵からの攻撃!");
            if (guard)
            {
                if (UnityEngine.Random.Range(0, 100) <= 80)
                {
                    enemyHp--;
                    Debug.Log("敵に攻撃を跳ね返した! ");
                    Debug.Log("敵に1のダメージ! ");
                }
                else
                {
                    Debug.Log("跳ね返すのに失敗した!");
                    playerHp--;
                    Debug.Log("プレイヤーに1のダメージ!");
                }
                guard = false;
            }
            else
            {
                playerHp--;
                Debug.Log("プレイヤーに1のダメージ!");
            }
        }

        if (playerHp <= 0)
        {
            Debug.Log("あなたは倒れた!");
        }
        if (enemyHp <= 0)
        {
            Debug.Log("敵は倒れた!");
        }



        yield return new WaitForSeconds(3);

        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(uI_Prefab.UI_Prefabs[0].Equals(uI_Prefab.UI_Prefabs[i]));
        }
        uI_Maneger.nowPanel = 0;
    }
}
