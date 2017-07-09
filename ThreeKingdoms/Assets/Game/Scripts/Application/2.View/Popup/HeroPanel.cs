using UnityEngine;
using System.Collections;

public class HeroPanel : MonoBehaviour
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	Skill1 m_skill1;
	Skill2 m_skill2;
	GameModel gm;
	#endregion

	#region 属性
	#endregion

	#region 方法
	public void Show( Vector3 m_position)
	{
		Vector3 v = m_position;
		v.x = v.x + 1.4f;
		v.y = v.y + 0.4f;
		transform.position = v;

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
	#endregion

	#region Unity回调
	void Awake()
	{
		m_skill1 = GetComponentInChildren<Skill1>();
		m_skill2 = GetComponentInChildren<Skill2>();
	}

	void Update()
	{
		return;
	}
	#endregion

	#region 帮助方法
	#endregion
}