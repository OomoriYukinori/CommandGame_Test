using UnityEngine;

public class UI_Prefab : MonoBehaviour
{

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