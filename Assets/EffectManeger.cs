using System.Collections;
using UnityEngine;

public class EffectManeger : MonoBehaviour
{
    bool attack;
    bool guard;
    bool Items;

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
        enemyHp = enemy[0].GetComponent<StatusManeger>().maxHp;

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
            StartCoroutine(turn());
        }
        else Debug.Log("SPが足りない！");
    }
    public void Item()
    {
        if (status.nowItem > 0 && status.nowHp < 10)
        {
            Items = true;

            StartCoroutine(turn());
        }
        else if(status.nowHp >= 10) Debug.Log("これ以上回復できない！");
        else if(status.nowItem <= 0) Debug.Log("Itemが足りない！");
    }
    public void Escape()
    {
        Debug.Log("戦闘から逃げ出した！");
        Debug.Log("あなたは回復した！");
        status.nowHp = status.maxHp;
        status.nowHp = status.maxSp;
        status.nowHp = status.maxItem;
        enemyHp = enemy[0].GetComponent<StatusManeger>().maxHp;
    }

    IEnumerator turn()
    {
        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(false);
        }

        if (enemyHp > 0)
        {
            if (attack)
            {
                Debug.Log("あなたの攻撃!");
                yield return new WaitForSeconds(1);
                enemyHp--;
                Debug.Log("敵に1のダメージ");
            }

            if (Items && enemyHp > 0)
            {
                Debug.Log("HPが回復した!");
                status.nowItem--;
                status.nowHp += 2;
            }

            yield return new WaitForSeconds(1);
            if (enemyHp > 0)
            {
                Debug.Log("敵からの攻撃!");

                yield return new WaitForSeconds(1);
                if (guard)
                {
                    status.nowSp--;
                    if (UnityEngine.Random.Range(0, 100) <= 80)
                    {
                        enemyHp -= 2;
                        Debug.Log("敵に攻撃を跳ね返した! ");

                        yield return new WaitForSeconds(1);
                        Debug.Log("敵に2のダメージ! ");
                    }
                    else
                    {
                        Debug.Log("跳ね返すのに失敗した!");
                        yield return new WaitForSeconds(1);
                        status.nowHp -= 2;
                        Debug.Log("プレイヤーに2のダメージ!");
                    }
                    guard = false;
                }
                else
                {
                    status.nowHp -= 2;
                    Debug.Log("プレイヤーに2のダメージ!");
                }
            }

            yield return new WaitForSeconds(1);


            if (enemyHp <= 0)
            {
                Debug.Log("敵は倒れた!");
            }
            else Debug.Log("敵の残りHP" + enemyHp);


            yield return new WaitForSeconds(1);

            if (status.nowHp <= 0)
            {
                Debug.Log("あなたは倒れた!");
            }
            else Debug.Log("あなた" +
                            "　HP" + status.nowHp+
                            "　SP" + status.nowSp+
                            "　Item" + status.nowItem
                            
                            );

            attack = false;
            guard = false;
            Items = false;

        }
        else Debug.Log("敵がいないようだ！");

        yield return new WaitForSeconds(1);

        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(uI_Prefab.UI_Prefabs[0].Equals(uI_Prefab.UI_Prefabs[i]));
        }
        uI_Maneger.nowPanel = 0;
    }
}
