using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{
    public float OffsetX = 1000; //X方向的偏移量
    public float Duration = 1f;//周期时间

    void Start()
    {
        /*iTween.MoveBy(gameObject, iTween.Hash(
            "x", OffsetX,
            "easeType", iTween.EaseType.linear,
            "loopType", iTween.LoopType.loop,
            "time", Duration)); */
    }
}