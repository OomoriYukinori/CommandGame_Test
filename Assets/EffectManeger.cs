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

        else displayText.Add("SP������Ȃ��I");
    }
    public void Item()
    {
        if (status.NowItem > 0 && status.NowHp < 10)
        {
            Items = true;

            StartCoroutine(turn());
        }
        else if (status.NowHp >= 10) displayText.Add("����ȏ�񕜂ł��Ȃ��I");
        else if (status.NowItem <= 0) displayText.Add("Item������Ȃ��I");
    }
    public void Escape()
    {
        displayText.Add("�퓬���瓦���o�����I");
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
                displayText.Add("���Ȃ��̍U��!");
                enemyHp--;
                displayText.Add("�G��1�̃_���[�W");
            }

            if (Items && enemyHp > 0)
            {
                displayText.Add("HP���񕜂���!");
                status.NowItem--;
                status.NowHp += 2;
            }

            if (enemyHp > 0)
            {
                displayText.Add("�G����̍U��!");

                if (guard)
                {
                    status.NowSp--;
                    if (UnityEngine.Random.Range(0, 100) <= 80)
                    {
                        enemyHp -= 2;
                        displayText.Add("�G�ɍU���𒵂˕Ԃ���! ");

                        //yield return new WaitForSeconds(1);
                        displayText.Add("�G��2�̃_���[�W! ");
                    }
                    else
                    {
                        displayText.Add("���˕Ԃ��̂Ɏ��s����!");
                        //yield return new WaitForSeconds(1);
                        status.NowHp -= 2;
                        displayText.Add("�v���C���[��2�̃_���[�W!");
                    }
                    guard = false;
                }
                else
                {
                    status.NowHp -= 2;
                    displayText.Add("�v���C���[��2�̃_���[�W!");
                }
            }



            if (enemyHp <= 0)
            {
                displayText.Add("�G�͓|�ꂽ!");
            }
            else displayText.Add("�G�̎c��HP" + enemyHp);



            if (status.NowHp <= 0)
            {
                displayText.Add("���Ȃ��͓|�ꂽ!");
            }
            else displayText.Add("���Ȃ�" +
                            "�@HP" + status.NowHp +
                            "�@SP" + status.NowSp +
                            "�@Item" + status.NowItem
                            );

            attack = false;
            guard = false;
            Items = false;

        }
        else displayText.Add("�G�����Ȃ��悤���I");


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
