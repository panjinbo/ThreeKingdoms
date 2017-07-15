using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class UIDialog1 : View{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public Image background;
	public Image liubei;
	public Image caocao;
	public Image zhaoyun;
	public Image zhangfei;
	public Text textleft;
	public Text textright;
	int current_stage;
	public event EventHandler<EventArgs> OnClick;
	#endregion

	#region 属性
	public override string Name
	{
		get { return Consts.V_NextDialog; }
	}


	#endregion

	#region 方法
	#endregion

	#region Unity回调
	void Awake()
	{
		liubei.gameObject.SetActive (true);
		zhaoyun.gameObject.SetActive(true);
		caocao.gameObject.SetActive (false);
		zhangfei.gameObject.SetActive (false);
		textleft.text = "Yun, thanks for saving my son's life";
		current_stage = 0;
		OnClick += Map_OnTileClick;
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			EventArgs e = new EventArgs ();
			if (OnClick != null)
				OnClick (this, e);
		}
	}

	#endregion

	#region 事件回调


	public override void RegisterEvents()
	{
		AttentionEvents.Add(Consts.E_NextDialog);
	}

	public override void HandleEvent(string eventName, object data)
	{
		switch (eventName)
		{
			
		}
	}
	void Map_OnTileClick(object sender, EventArgs e)
	{
		current_stage += 1;
		if (current_stage == 1) {
			textleft.gameObject.SetActive (false);
			textright.gameObject.SetActive (true);
			textright.text = "It's my honor to serve you and your son, Prince Liu.";
		} else if (current_stage == 2) {
			textright.text = "We should leave now, Chancelor Cao is coming.";
		} else if (current_stage == 3) {
			textright.gameObject.SetActive (false);
			textleft.gameObject.SetActive (true);
			textleft.text = "Yes, indeed.";
		} else if (current_stage == 4) {
			liubei.gameObject.SetActive (false);
			zhaoyun.gameObject.SetActive (false);
			zhangfei.gameObject.SetActive (true);
			textleft.text = "Let me cover you, brother";
		} else if (current_stage == 5) {
			textleft.gameObject.SetActive (false);
			caocao.gameObject.SetActive (true);
			textright.gameObject.SetActive (true);
			textright.text = "LMAO, today is a good day to die, Sir Liu";
		} else {
			StartLevelArgs ee = new StartLevelArgs()
			{
				LevelIndex = 0
			};
			SendEvent(Consts.E_StartLevel, ee);
		}
	}		
	#endregion

	#region 帮助方法
	#endregion
}
