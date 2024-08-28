using UnityEngine;

public class UI_Prefab : MonoBehaviour
{

    [Header("以下コマンド用のパネルを入れること")]
    GameObject selectPanel;

    [Header("以下スキル用のパネルを入れること")]
    GameObject skillPanel;

    /*[Header("以下結果用のパネルを入れること")]
    public GameObject resultPanel;
    [Header("ここに結果用のテキストを入れること")]
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