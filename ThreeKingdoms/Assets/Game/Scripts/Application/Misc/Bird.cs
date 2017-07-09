using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
    public float Time = 1; //一次循环所需时间
    public float OffsetY = 2; //Y方向浮动偏移

	void Start () 
    {
        /*iTween.MoveBy(this.gameObject, iTween.Hash(
            "y", OffsetY,
            "easeType", iTween.EaseType.easeInOutSine,
            "loopType", iTween.LoopType.pingPong,
            "time", Time)); */
	}
}
