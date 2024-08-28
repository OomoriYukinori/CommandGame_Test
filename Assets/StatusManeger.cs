using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManeger : MonoBehaviour
{
    public readonly int maxHp = 10;
    public int nowHp;
    public readonly int maxSp = 3;
    public int nowSp;
    public readonly int maxItem = 5;
    public int nowItem;

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
