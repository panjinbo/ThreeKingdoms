using UnityEngine;
using System.Collections;

public class UpgradeIcon : MonoBehaviour
{
    SpriteRenderer m_Render;
    Tower m_Tower;
	GameModel gm;

    void Awake()
    {
        m_Render = GetComponent<SpriteRenderer>();
    }

    public void Load(GameModel gm, Tower tower)
    {
        m_Tower = tower;
		this.gm = gm;
        
        //图标
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(tower.ID);
		string path = "Res/Roles/" + ((tower.name>=3||gm.Score<m_Tower.BasePrice) ? info.DisabledUpdate : info.NormalUpdate);
        m_Render.sprite = Resources.Load<Sprite>(path);
    }

    void OnMouseDown()
    {
		if (m_Tower.name>=3 || gm.Score<m_Tower.BasePrice)
            return;
		gm.Score -= m_Tower.BasePrice;
        UpgradeTowerArgs e = new UpgradeTowerArgs()
        {
            tower = m_Tower
        };
        SendMessageUpwards("UpgradeTower", e, SendMessageOptions.DontRequireReceiver);
        SpawnTowerArgs e2 = new SpawnTowerArgs()
        {
            Position = m_Tower.transform.position,
			TowerID = m_Tower.name + 3
        };
        SendMessageUpwards("SpawnTower", e2, SendMessageOptions.DontRequireReceiver);
    }
}