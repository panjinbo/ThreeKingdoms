using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIComplete : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Button BtnSelect;
    public Button BtnClear;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Complete; }
    }
    #endregion

    #region 方法
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void RegisterEvents()
    {
        
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    public void OnSelectClick()
    {
        //回到开始界面
        Game.Instance.LoadScene(4);
    }

    public void OnClearClick()
    {
		//HU
		GameModel gm = GetModel<GameModel>();
		gm.ClearProgress ();
		//HU
    }
    #endregion

    #region 帮助方法
    #endregion
}
