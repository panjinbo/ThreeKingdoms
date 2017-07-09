using UnityEngine;
using System.Collections;

public class UIStart : View
{
    public override string Name
    {
        get { return Consts.V_Start; }
    }

    public void GotoSelect()
    {
        Game.Instance.LoadScene(2);
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }
}