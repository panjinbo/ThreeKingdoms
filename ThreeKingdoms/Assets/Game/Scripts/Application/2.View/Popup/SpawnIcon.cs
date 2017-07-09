using UnityEngine;
using System.Collections;

public class SpawnIcon : MonoBehaviour
{
    SpriteRenderer m_Render;
    TowerInfo m_Info;
    Vector3 m_CreatePosition;
    bool m_Enough = false;
    GameModel gm;
    void Awake()
    {
        m_Render = GetComponent<SpriteRenderer>();
    }

	public void reset(int i)
	{
		Vector3 pos = transform.position;
		switch (i) {
		case 0:
			pos.x = -8;
                pos.y = -4;
			break;
		case 1:
			pos.x = -6.5f;
                pos.y = -4;
			break;
		case 2:
			pos.x = -5;
                pos.y = -4;
			break;
		default:
			break;

		}
		transform.position = pos;


	}


	public void Load(GameModel gm, TowerInfo info, Vector3 createPostion, bool upSide,bool LeftEdge,bool RightEdge)
    {
        m_Info = info;

        m_CreatePosition = createPostion;
        this.gm = gm;
        //是否足够
		    m_Enough = gm.Score >= info.BasePrice;

        //图标
        string path = "Res/Roles/" + (m_Enough ? info.NormalIcon : info.DisabledIcon);
        m_Render.sprite = Resources.Load<Sprite>(path);

    }

    void OnMouseDown()
    {
        if (!m_Enough)
          return;
        gm.Score-=m_Info.BasePrice;
        SpawnTowerArgs e = new SpawnTowerArgs()
        {
            Position = m_CreatePosition,
            TowerID = m_Info.ID
        };
        SendMessageUpwards("SpawnTower", e, SendMessageOptions.DontRequireReceiver);
    }
}
