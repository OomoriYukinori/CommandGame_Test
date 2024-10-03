using UnityEngine;

public class UI_Maneger : MonoBehaviour
{
    [SerializeField] UI_Prefab uiPrefab;
    public int nowPanel;

    // Start is called before the first frame update
    void Start()
    {
        uiPrefab = GetComponent<UI_Prefab>();
        //uiPrefab.fightButton = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateModePanel(GameObject panelToBeActivated)
    {
        uiPrefab.UI_Prefabs[0].SetActive(panelToBeActivated.Equals(uiPrefab.UI_Prefabs[0]));
    }
    public void ActivateBattlesPanel(GameObject panelToBeActivated)
    {
        for (int i = 0; i < uiPrefab.UI_Prefabs.Length; i++)
        {
            if (panelToBeActivated.Equals(uiPrefab.UI_Prefabs[i])) nowPanel = i;
            uiPrefab.UI_Prefabs[i].SetActive(panelToBeActivated.Equals(uiPrefab.UI_Prefabs[i]));
        }
    }

    public void ReturnPanel()
    {
        if (nowPanel > 0)
        {
            for (int i = 0; i < uiPrefab.UI_Prefabs.Length; i++)
            {
                uiPrefab.UI_Prefabs[i].SetActive(uiPrefab.UI_Prefabs[nowPanel - 1].Equals(uiPrefab.UI_Prefabs[i]));
            }
        }
    }

}

