using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SubPool
{
    Transform m_parent;

    //预设
    GameObject m_prefab;

    //集合
    List<GameObject> m_objects = new List<GameObject>();

    //名字标识
    public string Name
    {
        get { return m_prefab.name; }
    }

    //构造
    public SubPool(Transform parent, GameObject prefab)
    {
        this.m_parent = parent;
        this.m_prefab = prefab;
    }

    //取对象
    public GameObject Spawn()
    {
        GameObject go = null;

        foreach (GameObject obj in m_objects)
        {
            if (!obj.activeSelf)
            {
                go = obj;
                break;
            }
        }

        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            go.transform.parent = m_parent;
            m_objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return go;
    }

    //回收对象
    public void Unspawn(GameObject go)
    {
        if(Contains(go))
        {
            go.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
			if(m_prefab.name == "Enemy0" || m_prefab.name == "Enemy1" || m_prefab.name == "Enemy2" || m_prefab.name == "Enemy3" || m_prefab.name == "Enemy7"){
              m_objects.Remove(go);
            }
        }
    }

    //回收该池子的所有对象
    public void UnspawnAll()
    {
        foreach(GameObject item in m_objects)
        {
            if (item.activeSelf)
            {
                Unspawn(item);
            }
        }
    }

    //是否包含对象
    public bool Contains(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
