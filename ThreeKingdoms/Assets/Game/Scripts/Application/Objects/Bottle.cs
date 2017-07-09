using UnityEngine;
using System.Collections;

public class Bottle : Tower
{
    Transform m_ShotPoint;
	public int BulletCount = 6;

    protected override void Awake()
    {
        base.Awake();
        m_ShotPoint = transform.Find("ShotPoint");
    }

	public override void Shot(Enemy enemy)
	{
		base.Shot(enemy);

		for (int i = 0; i < BulletCount; i++)
		{
			//发射方向
			float radians = (Mathf.PI * 2f / BulletCount) * i;
			Vector3 dir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);

			//产生子弹
			GameObject go = Game.Instance.ObjectPool.Spawn("BallBullet");
			BallBullet bullet = go.GetComponent<BallBullet>();
			bullet.transform.position = m_ShotPoint.position;
			bullet.Load(this.UseBulletID, this.Level, this.MapRect, dir);
		}
	}
}