using UnityEngine;
using System.Collections;

public class PaotaiBullet : Bullet
{
	//目标
	public Enemy Target { get; private set; }

	//移动方向
	public Vector3 Direction { get; private set; }

	public void Load(int bulletID, int level, Rect mapRect, Enemy enemy)
	{
		Load(bulletID, level, mapRect);

		Target = enemy;

		//计算方向
		Direction = (Target.transform.position - transform.position).normalized;
	}

	protected override void Update()
	{
		//已爆炸无需跟踪
		if (m_IsExploded)
			return;

		//目标检测
		if (Target!= null)
		{
			if (!Target.IsDead)
			{
				//计算方向
				Direction = (Target.transform.position - transform.position).normalized;
			}

			//角度
			LookAt();

			//移动
			transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

			//打中目标
			if (Vector3.Distance(transform.position, Target.transform.position) <= Consts.DotClosedDistance)
			{
				//敌人受伤
				Target.Damage(this.Attack);

				//爆炸
				Explode();
			}
		}
		else
		{
			//移动
			transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

			//边界检测
			if (!m_IsExploded && !MapRect.Contains(transform.position))
				Explode();
		}
	}

	void LookAt()
	{
		float angle = Mathf.Atan2(Direction.y, Direction.x);
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.z = angle * Mathf.Rad2Deg - 90;
		transform.eulerAngles = eulerAngles;
	}
}