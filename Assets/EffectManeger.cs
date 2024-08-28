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
        else Debug.Log("SP������Ȃ��I");
    }
    public void Item()
    {
        if (status.nowItem > 0)
        {
            Items = true;
        }
        else Debug.Log("Item������Ȃ��I");
    }
    public void Escape()
    {
        Debug.Log("�Ƃŋx��ő̗͂��S�񕜂����I");
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
            Debug.Log("���Ȃ��̍U��!");
            enemyHp--;
            Debug.Log("�G��1�̃_���[�W");
        }

        if (Items && enemyHp > 0)
        {
            Debug.Log("HP���񕜂���!");
            playerHp += 2;
        }

        if (enemyHp > 0)
        {
            Debug.Log("�G����̍U��!");
            if (guard)
            {
                if (UnityEngine.Random.Range(0, 100) <= 80)
                {
                    enemyHp--;
                    Debug.Log("�G�ɍU���𒵂˕Ԃ���! ");
                    Debug.Log("�G��1�̃_���[�W! ");
                }
                else
                {
                    Debug.Log("���˕Ԃ��̂Ɏ��s����!");
                    playerHp--;
                    Debug.Log("�v���C���[��1�̃_���[�W!");
                }
                guard = false;
            }
            else
            {
                playerHp--;
                Debug.Log("�v���C���[��1�̃_���[�W!");
            }
        }

        if (playerHp <= 0)
        {
            Debug.Log("���Ȃ��͓|�ꂽ!");
        }
        if (enemyHp <= 0)
        {
            Debug.Log("�G�͓|�ꂽ!");
        }



        yield return new WaitForSeconds(3);

        for (int i = 0; i < uI_Prefab.UI_Prefabs.Length; i++)
        {
            uI_Prefab.UI_Prefabs[i].SetActive(uI_Prefab.UI_Prefabs[0].Equals(uI_Prefab.UI_Prefabs[i]));
        }
        uI_Maneger.nowPanel = 0;
    }
}
