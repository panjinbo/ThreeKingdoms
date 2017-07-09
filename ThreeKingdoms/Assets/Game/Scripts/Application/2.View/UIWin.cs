using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIWin : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Text txtCurrent;
    public Text txtTotal;
    public Button btnRestart;
    public Button btnContinue;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Win; }
    }
    #endregion

    #region 方法
	//HU
    public void Show()
    {
        this.gameObject.SetActive(true);

		RoundModel rm = GetModel<RoundModel> ();
		UpdateRoundInfo (rm.RoundIndex + 1, rm.RoundTotal);
    }
	//HU

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

	//HU
    void UpdateRoundInfo(int currentRound, int totalRound)
    {
        txtCurrent.text = currentRound.ToString("D2");
        txtTotal.text = totalRound.ToString();
    }
	//HU
    #endregion

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
    public void OnContinueClick()
    {
		GameModel gm = GetModel<GameModel> ();
		if (gm.PlayLevelID >= gm.LevelCount - 1)
		{
			//游戏通关
			Game.Instance.LoadScene(4);
		}
		else
		{
			//开始下一关
			SendEvent (Consts.E_StartLevel, new StartLevelArgs (){ LevelIndex = gm.PlayLevelID + 1 });
		}
    }
	//HU
    #endregion

    #region 帮助方法
    #endregion
}
