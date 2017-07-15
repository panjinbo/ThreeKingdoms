using UnityEngine;
using System.Collections;

public class Spawner : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    Map m_Map = null;
	Hero m_Hero = null;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_Spanwner; }
    }
    #endregion

    #region 方法



	//HU
	public void SpawnHero(int HeroType,Vector3 position)
	{
		string prefabName = "Hero" + HeroType;
		GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);
		m_Hero = go.GetComponent<Hero> ();
		m_Hero.Dead += hero_Dead;
		m_Hero.transform.position = position;
	}
	//HU

    public void SpawnEnemy(int EnemyType)
    {
        //创建怪物
        string prefabName = "Enemy" + EnemyType;
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.Reached += enemy_Reached;
		enemy.NearReached += enemy_NearReached;
        enemy.HpChanged += enemy_HpChanged;
        enemy.Dead += enemy_Dead;
        enemy.Load(m_Map.Path);

		//CC
		enemy.hpbar = go.transform.GetChild (0).gameObject;
		enemy.fill = enemy.hpbar.transform.GetChild (0).gameObject;
		//CC
    }

	//HU
	void hero_Dead(Role hero)
	{
		//回收hero
		Game.Instance.ObjectPool.Unspawn(hero.gameObject);

		//游戏胜利
		GameModel gm = GetModel<GameModel> ();
		GameObject[] enemys = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject go in enemys){
			Game.Instance.ObjectPool.Unspawn (go);
		}
		SendEvent (Consts.E_EndLevel, new EndLevelArgs () {LevelID = gm.PlayLevelID, IsWin = false });
	}


	//HU

    void enemy_HpChanged(int arg1, int arg2)
    {

    }

	//HU
	void enemy_Dead(Role enemy)
  {
		//回收自己
		Enemy m = (Enemy)enemy;
		int Bonus = m.Bonus;

		Vector3 ps = enemy.transform.position;
		Game.Instance.ObjectPool.Unspawn (enemy.gameObject);

//		string prefabName = "Money" ;
//		GameObject money = Game.Instance.ObjectPool.Spawn(prefabName);
//		money.transform.position = ps;

		//游戏失败判断
		GameModel gm = GetModel<GameModel> ();
		gm.Score += Bonus;
		RoundModel rm = GetModel<RoundModel> ();
		GameObject[] enemys = GameObject.FindGameObjectsWithTag ("Enemy");

		if (!m_Hero.IsDead	&& enemys.Length <= 0 && rm.AllRoundsComplete) //萝卜没死，场景里已没有怪物，所有怪物已出完
		{

			SendEvent (Consts.E_EndLevel, new EndLevelArgs () {LevelID = gm.PlayLevelID, IsWin = true });
		}
  }

  //HU
  void SpawnTower(Vector3 position, int towerID)
  {
    //找到Tile
    Tile tile = m_Map.GetTile(position);

    //创建Tower
    TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
    GameObject go = Game.Instance.ObjectPool.Spawn(info.PrefabName);
    Tower tower = go.GetComponent<Tower>();
    tower.transform.position = position;
    tower.Load(towerID, tile, m_Map.MapRect);

    //设置Tile数据
    tile.Data = tower;
  }
  void map_OnTileClick(object sender, TileClickEventArgs e)
  {
    GameModel gm = GetModel<GameModel>();

    //游戏还未开始，那么不操作菜单
    if (!gm.IsPlaying)
      return;

    //如果有菜单显示，那么隐藏菜单
    if (TowerPopup.Instance.IsPopShow)
    {
      SendEvent(Consts.E_HidePopup);
      return;
    }
		//点英雄
		if (e.Tile.isHero>0) {
			if (e.Tile.isHero == 2) {
				e.Tile.Y = e.Tile.Y - 1;
			}
			ShowCreateArgs arg = new ShowCreateArgs () {

				Position = m_Map.GetPosition (e.Tile),
				UpSide = false,
				LeftEdge = false,
				RightEdge = true
			};
			if (e.Tile.isHero == 2) {
				e.Tile.Y = e.Tile.Y +1;
			}
			SendEvent (Consts.E_ShowHero,arg);
			return;
		}
    //非放塔格子，不操作菜单
    if(!e.Tile.CanHold)
    {
      SendEvent(Consts.E_HidePopup);
      return;
    }

    if (e.Tile.Data == null)
    {
      ShowCreateArgs arg = new ShowCreateArgs()
      {
        Position = m_Map.GetPosition(e.Tile),
        UpSide = e.Tile.Y < Map.RowCount / 2,
		LeftEdge= (e.Tile.X==0),
		RightEdge= (e.Tile.X==11)
      };
      SendEvent(Consts.E_ShowCreate, arg);
    }
    else
    {
      ShowUpgradeArgs arg = new ShowUpgradeArgs()
      {
        Tower = e.Tile.Data as Tower
      };
      SendEvent(Consts.E_ShowUpgrade, arg);
    }
  }
    void enemy_Reached(Enemy enemy)
    {


		//
		m_Hero.Damage(1);
		m_Hero.UnAttack ();

		//怪物死亡
		enemy.Hp = 0;

    }

	void enemy_NearReached(Enemy enemy)
	{
		m_Hero.Attack ();
	}
	//HU
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene);
        AttentionEvents.Add(Consts.E_SpawnEnemy);
        AttentionEvents.Add(Consts.E_SpawnTower);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e0 = data as SceneArgs;
                if (e0.SceneIndex == 3)
                {
					          //获取数据
          					GameModel gModel = GetModel<GameModel>();

          					//加载地图
          					m_Map = GetComponent<Map>();
          				  m_Map.LoadLevel(gModel.PlayLevel);
                   		 m_Map.OnTileClick += map_OnTileClick;
          					//HU
          					//加载萝卜
          					Vector3[] path = m_Map.Path;
          					Vector3 pos = path [path.Length - 2];
							int Herotype = int.Parse (gModel.PlayLevel.Name);
          					SpawnHero (Herotype,pos);
				pos.y += 0.5f;
				gModel.des = pos;
          					//HU
                }
                break;
            case Consts.E_SpawnEnemy:
                SpawnEnemyArgs e1 = data as SpawnEnemyArgs;
                SpawnEnemy(e1.EnemyType);
                break;
            case Consts.E_SpawnTower:
          			SpawnTowerArgs e2 = data as SpawnTowerArgs;
          			SpawnTower(e2.Position, e2.TowerID);
          			break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
