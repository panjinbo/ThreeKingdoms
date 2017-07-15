using UnityEngine;
using System.Collections;

public class Paotai : Tower
{
	Transform m_ShotPoint;
	protected override void Awake()
	{
		base.Awake();
		m_ShotPoint = transform.Find("ShotPoint");
	}
    public override void Shot(Enemy enemy)
    {
		base.Shot(enemy);

		GameObject go = Game.Instance.ObjectPool.Spawn("PaotaiBullet");
		PaotaiBullet bullet = go.GetComponent<PaotaiBullet>();
		bullet.transform.position = m_ShotPoint.position;
		bullet.Load(this.UseBulletID, this.Level, this.MapRect, enemy);
    }
}