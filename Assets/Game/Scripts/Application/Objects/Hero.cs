using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//HU 全部

class Hero : Role
{
	#region 常量
	#endregion

	#region 事件
	#endregion

	#region 字段
	public HeroType HeroType = HeroType.Hero0;//萝卜类型
	Animator m_Animator;
	#endregion

	#region 属性
	#endregion

	#region 方法
	//点击冷冻技能
	public void FreezeAll()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject go in objects)
		{
			Enemy enemy = go.GetComponent<Enemy>();
			//被冷冻的效果
			enemy.Freeze();
		}

	}



	//接近萝卜攻击
	public void Attack()
	{
		m_Animator.SetBool ("attack", true);
	}

	public void UnAttack()
	{
		m_Animator.SetBool ("attack", false);
	}

	public override void Damage (int hit)
	{
		if (IsDead)
			return;
		
		base.Damage (hit);

		m_Animator.SetTrigger ("IsDamage");

		Debug.Log ("Damage");
	}

	protected override void Die (Role role)
	{
		base.Die (role);

		m_Animator.SetBool ("IsDead", true);

		Debug.Log ("Die");
	}

	public override void OnSpawn()
	{
		//初始化
		base.OnSpawn();

		HeroInfo info = Game.Instance.StaticData.GetHeroInfo ((int)HeroType);
		m_Animator = GetComponent<Animator> ();
		m_Animator.Play ("Idle");
		MaxHp = info.Hp;
		Hp = info.Hp;
	}

	public override void OnUnspawn()
	{
		//还原
		base.OnUnspawn(); 
		m_Animator.SetBool ("IsDead", false);
		m_Animator.ResetTrigger ("IsDamage");

	}
	#endregion

	#region Unity回调
	#endregion

	#region 帮助方法
	#endregion
}

