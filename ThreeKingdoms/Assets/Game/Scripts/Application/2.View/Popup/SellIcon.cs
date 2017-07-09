using UnityEngine;
using System.Collections;

public class SellIcon : MonoBehaviour
{
    Tower m_Tower;

    public void Load(Tower tower)
    {
        m_Tower = tower;
    }

    void OnMouseDown()
    {
        SellTowerArgs e = new SellTowerArgs()
        {
            tower = m_Tower
        };
        SendMessageUpwards("SellTower", e, SendMessageOptions.DontRequireReceiver);
    }
}