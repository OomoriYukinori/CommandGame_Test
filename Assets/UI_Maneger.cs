using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Maneger : MonoBehaviour
{
    [SerializeField] UI_Prefab uiPrefab;
    private GameObject oldPanel;
    private int nowPanel;

    // Start is called before the first frame update
    void Start()
    {
        //uiPrefab.fightButton = gameObject;
        nowPanel = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ActivateModePanel(GameObject panelToBeActivated)
    {
        uiPrefab.UI_Prefabs[0].SetActive(panelToBeActivated.Equals(uiPrefab.UI_Prefabs[0]));
        //GameOptions_UI_Panel.SetActive(panelToBeActivated.Equals(GameOptions_UI_Panel.name));
    }
    public void ActivateBattlesPanel(GameObject panelToBeActivated)
    {
        uiPrefab.UI_Prefabs[0].SetActive(panelToBeActivated.Equals(uiPrefab.UI_Prefabs[0]));
        uiPrefab.UI_Prefabs[1].SetActive(panelToBeActivated.Equals(uiPrefab.UI_Prefabs[1]));
        //uiPrefab.resultPanel.SetActive(panelToBeActivated.Equals(uiPrefab.resultPanel));
        for (int i = 0; i < uiPrefab.UI_Num; i++)
        {
            if (panelToBeActivated.Equals(uiPrefab.UI_Prefabs[i])) nowPanel = i;
            uiPrefab.UI_Prefabs[i].SetActive(panelToBeActivated.Equals(uiPrefab.UI_Prefabs[i]));
        }
    }

    public void ReturnPanel()
    {
        for (int i = 0; i < uiPrefab.UI_Num; i++)
        {
            uiPrefab.UI_Prefabs[i].SetActive(uiPrefab.UI_Prefabs[nowPanel-1].Equals(uiPrefab.UI_Prefabs[i]));
        }
    }

}

[Serializable]
public class UI_Prefab
{
    public readonly int UI_Num = 2;

    [Header("�ȉ��R�}���h�p�̃p�l�������邱��")]
    GameObject selectPanel;

    [Header("�ȉ��X�L���p�̃p�l�������邱��")]
    GameObject skillPanel;

    /*[Header("�ȉ����ʗp�̃p�l�������邱��")]
    public GameObject resultPanel;
    [Header("�����Ɍ��ʗp�̃e�L�X�g�����邱��")]
    public TextMesh resultText;
    //*/

    public GameObject[] UI_Prefabs;

    public void SetPanel()
    {
        UI_Prefabs = new GameObject[]
        {
            selectPanel ,
            skillPanel
        };
    }
}