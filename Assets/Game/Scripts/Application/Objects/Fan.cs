using UnityEngine;
using System.Collections;

public class Fan : Tower
{
    public int BulletCount = 6;

    public override void Shot(Enemy enemy)
    {
        base.Shot(enemy);

        for (int i = 0; i < BulletCount; i++)
        {
            //发射方向
            float radians = (Mathf.PI * 2f / BulletCount) * i;
            Vector3 dir = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);

            //产生子弹
            GameObject go = Game.Instance.ObjectPool.Spawn("FanBullet");
            FanBullet bullet = go.GetComponent<FanBullet>();
            bullet.transform.position = transform.position;//中心点
            bullet.Load(this.UseBulletID, this.Level, this.MapRect, dir);
        }
    }
}