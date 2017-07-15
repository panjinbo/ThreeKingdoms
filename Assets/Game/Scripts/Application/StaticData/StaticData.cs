using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticData : Singleton<StaticData>
{
	Dictionary<int, HeroInfo> m_Heros = new Dictionary<int, HeroInfo>();
	Dictionary<int, EnemyInfo> m_enemys = new Dictionary<int, EnemyInfo>();
	Dictionary<int, TowerInfo> m_Towers = new Dictionary<int, TowerInfo>();
	Dictionary<int, BulletInfo> m_Bullets = new Dictionary<int, BulletInfo>();

    protected override void Awake()
    {
        base.Awake();

		InitHeros();
        InitEnemys();
        InitTowers();
        InitBullets();
	}

	void InitHeros()
	{
		m_Heros.Add (0, new HeroInfo() {ID = 1, Hp = 5 });
		m_Heros.Add (1, new HeroInfo() {ID = 2, Hp = 5 });
	}

    void InitEnemys()
    {
		//每种怪物的信息 级别越小 血量越低 跑得越快
		/*m_enemys.Add(0, new EnemyInfo() { ID = 0, Hp = 1, MoveSpeed = 1.5f });
        m_enemys.Add(1, new EnemyInfo() { ID = 1, Hp = 2, MoveSpeed = 1f });
        m_enemys.Add(2, new EnemyInfo() { ID = 2, Hp = 5, MoveSpeed = 1f });
        m_enemys.Add(3, new EnemyInfo() { ID = 3, Hp = 10, MoveSpeed = 1f });
        m_enemys.Add(4, new EnemyInfo() { ID = 4, Hp = 10, MoveSpeed = 1f });
        m_enemys.Add(5, new EnemyInfo() { ID = 5, Hp = 100, MoveSpeed = 0.5f });
		m_enemys.Add(6, new EnemyInfo() { ID = 6, Hp = 10, MoveSpeed = 1.5f });
		m_enemys.Add(7, new EnemyInfo() { ID = 7, Hp = 10, MoveSpeed = 1.5f });*/


		m_enemys.Add(0, new EnemyInfo() { ID = 0, Hp = 10, MoveSpeed = 1f , Bonus =1});
		m_enemys.Add(1, new EnemyInfo() { ID = 1, Hp = 25, MoveSpeed = 1f , Bonus =1});
		m_enemys.Add(2, new EnemyInfo() { ID = 2, Hp = 28, MoveSpeed = 1f , Bonus = 2});
		m_enemys.Add(3, new EnemyInfo() { ID = 3, Hp = 33, MoveSpeed = 1f , Bonus =3});
		m_enemys.Add(4, new EnemyInfo() { ID = 4, Hp = 5, MoveSpeed = 1f , Bonus =1});
		m_enemys.Add(5, new EnemyInfo() { ID = 5, Hp = 5, MoveSpeed = 1f, Bonus =1});
		m_enemys.Add(6, new EnemyInfo() { ID = 6, Hp = 5, MoveSpeed = 1f , Bonus =1});
		m_enemys.Add(7, new EnemyInfo() { ID = 7, Hp = 100, MoveSpeed = 1f , Bonus =10});
	}


   void InitTowers()
    {
		m_Towers.Add (0, new TowerInfo () {
			ID = 0,
			PrefabName = "nuche",
			NormalIcon = "button/nuche_button",
			DisabledIcon = "button/nuche_button_blk",
			DisabledUpdate= "button/update_1_blk",
			NormalUpdate="button/update_1",
			MaxLevel = 3,
			BasePrice = 3,
			ShotRate = 0.2f,
			GuardRange = 2f,
			UseBulletID = 0
		});
		m_Towers.Add (1, new TowerInfo () {
			ID = 1,
			PrefabName = "drum",
			NormalIcon = "button/drum_button",
			DisabledIcon = "button/drum_button_blk",
			DisabledUpdate = "button/update_1_blk",
			NormalUpdate = "button/update_1",
            MaxLevel = 3,
			BasePrice = 2,
			ShotRate = 2f,
			GuardRange = 2f,
			UseBulletID = 1
		});
		m_Towers.Add (2, new TowerInfo () {
			ID = 2,
			PrefabName = "paotai",
			NormalIcon = "button/paotai_button",
			DisabledIcon = "button/paotai_button_blk",
			DisabledUpdate = "button/update_2_blk",
			NormalUpdate = "button/update_2",
            MaxLevel = 3,
			BasePrice = 3,
			ShotRate = 1f,
			GuardRange = 3f,
			UseBulletID = 1
		});
        m_Towers.Add(3, new TowerInfo()
        {
            ID = 3,
            PrefabName = "nucheup",
			NormalIcon = "",
			DisabledIcon = "",
			DisabledUpdate = "Tower/update1",
			NormalUpdate = "Tower/update2",
            MaxLevel = 3,
            BasePrice = 2,
            ShotRate = 0.4f,
            GuardRange = 2f,
            UseBulletID = 1
        });
        m_Towers.Add(4, new TowerInfo()
        {
            ID = 4,
            PrefabName = "drumup",
				NormalIcon = "",
				DisabledIcon = "2",
				DisabledUpdate = "Tower/update1",
				NormalUpdate = "Tower/update2",
            MaxLevel = 3,
            BasePrice = 2,
            ShotRate = 3f,
            GuardRange = 2f,
            UseBulletID = 1
        });
        m_Towers.Add(5, new TowerInfo()
        {
            ID = 5,
            PrefabName = "paotaiup",
				NormalIcon = "",
				DisabledIcon = "",
				DisabledUpdate = "Tower/update1",
				NormalUpdate = "Tower/update2",
            MaxLevel = 3,
            BasePrice = 2,
            ShotRate = 2f,
            GuardRange = 3f,
            UseBulletID = 1
        });
    }

	void InitBullets()
	{
		m_Bullets.Add(0, new BulletInfo() { ID = 0, PrefabName = "BallBullet", BaseSpeed = 1f, BaseAttack = 1 });
		m_Bullets.Add(1, new BulletInfo() { ID = 1, PrefabName = "PaotaiBullet", BaseSpeed = 2f, BaseAttack = 1 });
	}


	//取萝卜信息
	public HeroInfo GetHeroInfo(int HeroType)
	{
		return m_Heros[HeroType];
	}

	//取怪物信息
	public EnemyInfo GetEnemyInfo(int enemyType)
	{
		return m_enemys[enemyType];
	}
	 public TowerInfo GetTowerInfo(int towerID)
    {
        return m_Towers[towerID];
    }
	public BulletInfo GetBulletInfo(int bulletID)
	{
		return m_Bullets[bulletID];
	}

}
