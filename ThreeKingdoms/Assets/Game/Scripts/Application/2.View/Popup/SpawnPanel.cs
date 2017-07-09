using UnityEngine;
using System.Collections;

public class SpawnPanel : MonoBehaviour
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    SpawnIcon[] m_Icons;
    #endregion

    #region 属性
    #endregion

    #region 方法
	public void Show(GameModel gm, Vector3 createPosition, bool upSide,bool LeftEdge,bool RightEdge)
    {
        transform.position = createPosition;
        for (int i = 0; i < m_Icons.Length; i++)
        {
            TowerInfo info = Game.Instance.StaticData.GetTowerInfo(i);
			m_Icons [i].reset (i);

			m_Icons[i].Load(gm, info, createPosition, upSide,LeftEdge,RightEdge);
        }
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Unity回调
    void Awake()
    {
        m_Icons = GetComponentsInChildren<SpawnIcon>();
    }
    #endregion

    #region 帮助方法
    #endregion
}
