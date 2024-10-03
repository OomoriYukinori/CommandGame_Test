using UnityEngine;

public class StatusManeger : MonoBehaviour
{
    public readonly int maxHp = 10;
    int nowHp;
    public int NowHp { get => nowHp; set => nowHp = value; }

    public readonly int maxSp = 3;
    int nowSp;
    public int NowSp { get => nowSp; set => nowSp = value; }

    public readonly int maxItem = 5;
    int nowItem;
    public int NowItem { get => nowItem; set => nowItem = value; }


    bool turn;
    // Start is called before the first frame update
    void Start()
    {
        nowHp = maxHp;
        nowSp = maxSp;
        nowItem = maxItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
