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
        else Debug.Log("SP������Ȃ��I");
    }
    public void Item()
    {
        if (status.nowItem > 0 && status.nowHp < 10)
        {
            Items = true;

            StartCoroutine(turn());
        }
        else if(status.nowHp >= 10) Debug.Log("����ȏ�񕜂ł��Ȃ��I");
        else if(status.nowItem <= 0) Debug.Log("Item������Ȃ��I");
    }
    public void Escape()
    {
        Debug.Log("�퓬���瓦���o�����I");
        Debug.Log("���Ȃ��͉񕜂����I");
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
                Debug.Log("���Ȃ��̍U��!");
                yield return new WaitForSeconds(1);
                enemyHp--;
                Debug.Log("�G��1�̃_���[�W");
            }

            if (Items && enemyHp > 0)
            {
                Debug.Log("HP���񕜂���!");
                status.nowItem--;
                status.nowHp += 2;
            }

            yield return new WaitForSeconds(1);
            if (enemyHp > 0)
            {
                Debug.Log("�G����̍U��!");

                yield return new WaitForSeconds(1);
                if (guard)
                {
                    status.nowSp--;
                    if (UnityEngine.Random.Range(0, 100) <= 80)
                    {
                        enemyHp -= 2;
                        Debug.Log("�G�ɍU���𒵂˕Ԃ���! ");

                        yield return new WaitForSeconds(1);
                        Debug.Log("�G��2�̃_���[�W! ");
                    }
                    else
                    {
                        Debug.Log("���˕Ԃ��̂Ɏ��s����!");
                        yield return new WaitForSeconds(1);
                        status.nowHp -= 2;
                        Debug.Log("�v���C���[��2�̃_���[�W!");
                    }
                    guard = false;
                }
                else
                {
                    status.nowHp -= 2;
                    Debug.Log("�v���C���[��2�̃_���[�W!");
                }
            }

            yield return new WaitForSeconds(1);


            if (enemyHp <= 0)
            {
                Debug.Log("�G�͓|�ꂽ!");
            }
            else Debug.Log("�G�̎c��HP" + enemyHp);


            yield return new WaitForSeconds(1);

            if (status.nowHp <= 0)
            {
                Debug.Log("���Ȃ��͓|�ꂽ!");
            }
            else Debug.Log("���Ȃ�" +
                            "�@HP" + status.nowHp+
                            "�@SP" + status.nowSp+
                            "�@Item" + status.nowItem
                            
                            );

            attack = false;
            guard = false;
            Items = false;

        }
        else Debug.Log("�G�����Ȃ��悤���I");

        yield return new WaitForSeconds(1);

        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(uI_Prefab.UI_Prefabs[0].Equals(uI_Prefab.UI_Prefabs[i]));
        }
        uI_Maneger.nowPanel = 0;
    }
}
