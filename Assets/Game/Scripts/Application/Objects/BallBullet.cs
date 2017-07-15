using UnityEngine;
using System.Collections;

public class BallBullet : Bullet
{
	//旋转速度（度/秒）
	public float RotateSpeed = 180f;

	public Vector2 Direction { get; private set; }

	public void Load(int bulletID, int level, Rect mapRect, Vector3 direction)
	{
		Load(bulletID, level, mapRect);
		Direction = direction;
	}

    protected override void Update()
    {
		//已爆炸跳过
		if (m_IsExploded)
			return;

		//移动
		transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

		//旋转
//		transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime, Space.World);

		//检测（存活/死亡）
		GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemyObject in enemyObjects)
		{
			Enemy enemy = enemyObject.GetComponent<Enemy>();

			//忽略已死亡的怪物
			if (enemy.IsDead)
				continue;

			if (Vector3.Distance(transform.position, enemy.transform.position) <= Consts.RangeClosedDistance)
			{
				//敌人受伤
				enemy.Damage(this.Attack);

				//爆炸
				Explode();

				//退出(重点)
				break;
			}
		}

		//边间检测
		if (!m_IsExploded && !MapRect.Contains (transform.position))
			return;
			//Explode();
    }
    
}