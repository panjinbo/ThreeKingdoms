using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILost : View
{

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Text txtCurrent;
    public Text txtTotal;
    public Button btnRestart;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Lost; }
    }
    #endregion

	//HU
    #region 方法
    public void Show()
    {
        this.gameObject.SetActive(true);

		RoundModel rm = GetModel<RoundModel> ();
		UpdateRoundInfo (rm.RoundIndex + 1, rm.RoundTotal);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    void UpdateRoundInfo(int currentRound, int totalRound)
    {
        txtCurrent.text = currentRound.ToString("D2");
        txtTotal.text = totalRound.ToString();
    }
    #endregion
	//HU

    #region Unity回调
    void Awake()
    {
        UpdateRoundInfo(0, 0);
    }
    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {

    }

	//HU
    public void OnRestartClick()
    {
		GameModel gm = GetModel<GameModel> ();
		SendEvent (Consts.E_StartLevel, new StartLevelArgs (){ LevelIndex = gm.PlayLevelID });
    }
	//HU
    #endregion

    #region 帮助方法
    #endregion
}