using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManeger : MonoBehaviour
{
    [SerializeField]
    Text text;
    List<string> displayText = new List<string>();
    bool displayTextBool;

    bool attack;
    bool guard;
    bool Items;


    bool skip = false;

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
    }
    public void Skill()
    {
        if (status.NowSp > 0)
        {
            guard = true;
            StartCoroutine(turn());
        }

        else displayText.Add("SPが足りない！");
    }
    public void Item()
    {
        if (status.NowItem > 0 && status.NowHp < 10)
        {
            Items = true;

            StartCoroutine(turn());
        }
        else if (status.NowHp >= 10) displayText.Add("これ以上回復できない！");
        else if (status.NowItem <= 0) displayText.Add("Itemが足りない！");
    }
    public void Escape()
    {
        displayText.Add("戦闘から逃げ出した！");
        status.NowHp = status.maxHp;
        status.NowSp = status.maxSp;
        status.NowItem = status.maxItem;
        enemyHp = enemy[0].GetComponent<StatusManeger>().maxHp;
    }

    IEnumerator turn()
    {
        displayText = new List<string>();


        if (enemyHp > 0)
        {


            if (attack)
            {
                displayText.Add("あなたの攻撃!");
                enemyHp--;
                displayText.Add("敵に1のダメージ");
            }

            if (Items && enemyHp > 0)
            {
                displayText.Add("HPが回復した!");
                status.NowItem--;
                status.NowHp += 2;
            }

            if (enemyHp > 0)
            {
                displayText.Add("敵からの攻撃!");

                if (guard)
                {
                    status.NowSp--;
                    if (UnityEngine.Random.Range(0, 100) <= 80)
                    {
                        enemyHp -= 2;
                        displayText.Add("敵に攻撃を跳ね返した! ");

                        //yield return new WaitForSeconds(1);
                        displayText.Add("敵に2のダメージ! ");
                    }
                    else
                    {
                        displayText.Add("跳ね返すのに失敗した!");
                        //yield return new WaitForSeconds(1);
                        status.NowHp -= 2;
                        displayText.Add("プレイヤーに2のダメージ!");
                    }
                    guard = false;
                }
                else
                {
                    status.NowHp -= 2;
                    displayText.Add("プレイヤーに2のダメージ!");
                }
            }



            if (enemyHp <= 0)
            {
                displayText.Add("敵は倒れた!");
            }
            else displayText.Add("敵の残りHP" + enemyHp);



            if (status.NowHp <= 0)
            {
                displayText.Add("あなたは倒れた!");
            }
            else displayText.Add("あなた" +
                            "　HP" + status.NowHp +
                            "　SP" + status.NowSp +
                            "　Item" + status.NowItem
                            );

            attack = false;
            guard = false;
            Items = false;

        }
        else displayText.Add("敵がいないようだ！");


        for (int i = 0; i < displayText.Count; i++)
        {
            Debug.Log(displayText[i]);
        }
        DisplayText();

        while (displayTextBool == true)
        {
            Debug.Log("stop2");
            yield return null;
        }

    }

    IEnumerator TextTest()
    {
        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(uI_Prefab.UI_Prefabs[2].Equals(uI_Prefab.UI_Prefabs[i]));
        }

        displayTextBool = true;
        while (displayText == null)
        {
            Debug.Log("stop1");
            yield return null;
        }

        for (int i = 0; i < displayText.Count; i++)
        {
            if (displayText[i] == null) continue;

            text.text = "";
            skip = false;

            yield return new WaitForSeconds(0.1f);
            StartCoroutine(nextTextKey());

            foreach (char c in displayText[i])
            {
                text.text += c;
                yield return new WaitForSeconds(0.1f);
                if (skip) break;
            }

            while (!skip)
            {
                yield return null;
            }
        }

        displayTextBool = false;


        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(uI_Prefab.UI_Prefabs[0].Equals(uI_Prefab.UI_Prefabs[i]));
        }
        uI_Maneger.nowPanel = 0;
    }

    IEnumerator nextTextKey()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        skip = true;
    }
    void DisplayText()
    {
        if (!displayTextBool)
        {
            StartCoroutine(TextTest());
        }
    }
}
