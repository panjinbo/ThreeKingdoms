using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBoard : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public Text txtScore;
    public Image imgRoundInfo;
    public Text txtCurrent;
    public Text txtTotal;
    public Image imgPauseInfo;
    public Button btnSpeed1;
    public Button btnSpeed2;
    public Button btnResume;
    public Button btnPause;
    public Button btnSystem;
    GameModel gModel;
    bool m_IsPlaying = false;
    GameSpeed m_Speed = GameSpeed.One;
    int m_Score = 0;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Board; }
    }

    void Update(){
      this.Score = gModel.Score;
    }
    public int Score
    {
        get { return m_Score; }
        set
        {
            m_Score = value;
            txtScore.text = value.ToString();
        }
    }

    public bool IsPlaying
    {
        get { return m_IsPlaying; }
        set
        {
            m_IsPlaying = value;

            //imgRoundInfo.gameObject.SetActive(value);
            imgPauseInfo.gameObject.SetActive(!value);
        }
    }

    public GameSpeed Speed
    {
        get { return m_Speed; }
        set
        {
            m_Speed = value;

            btnSpeed1.gameObject.SetActive(m_Speed == GameSpeed.One);
            btnSpeed2.gameObject.SetActive(m_Speed == GameSpeed.Two);
        }
    }
    #endregion

    #region 方法
    public void UpdateRoundInfo(int currentRound, int totalRound)
    {
        txtCurrent.text = currentRound.ToString("D2");//始终保留2位整数
        txtTotal.text = totalRound.ToString();
    }
    #endregion

    #region Unity回调
    void Awake()
    {
        this.Score = 0;
        this.IsPlaying = true;
        this.Speed = GameSpeed.One;
        gModel = GetModel<GameModel>();
    }
    #endregion

    #region 事件回调
    public void OnSpeed1Click()
    {
		if (Speed == GameSpeed.One) {
			Speed = GameSpeed.Two;
			Time.timeScale = Time.timeScale * 2;
			btnSpeed1.gameObject.SetActive (Speed != GameSpeed.Two);
			btnSpeed2.gameObject.SetActive (Speed == GameSpeed.Two);
		}

    }

    public void OnSpeed2Click()
    {
		if (Speed == GameSpeed.Two) {
			Speed = GameSpeed.One;
			Time.timeScale = Time.timeScale / 2;
			btnSpeed1.gameObject.SetActive (Speed == GameSpeed.One);
			btnSpeed2.gameObject.SetActive (Speed != GameSpeed.One);
		}
    }

    public void OnPauseClick()
    {
        IsPlaying = false;
		Time.timeScale = 0;
		btnResume.gameObject.SetActive (!IsPlaying);
		btnPause.gameObject.SetActive (IsPlaying);
    }

    public void OnResumeClick()
    {
        IsPlaying = true;
		Time.timeScale = 1;
		btnPause.gameObject.SetActive (IsPlaying);
		btnResume.gameObject.SetActive (!IsPlaying);
    }

    public void OnSystemClick()
    {

    }

    public override void RegisterEvents()
    {
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {

        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
