using UnityEngine;
using System.Collections;

public class Drum : Tower
{

    protected override void Awake()
    {
        base.Awake();

    }
    public override void Shot(Enemy enemy)
    {
        base.Shot(enemy);
		GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemyObject in enemyObjects)
		{
			Enemy mons = enemyObject.GetComponent<Enemy>();

			//忽略已死亡的怪物
			if (mons.IsDead)
				continue;
			if (Vector3.Distance (transform.position, mons.transform.position) <= Consts.SlowGuardDistance) 
			{
				//敌人受伤
				mons.Slowdown();
				
			}
		}
  
    }
}