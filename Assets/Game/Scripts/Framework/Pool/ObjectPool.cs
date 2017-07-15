using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public string ResourceDir = "";

    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    //取对象
    public GameObject Spawn(string name)
    {
        if (!m_pools.ContainsKey(name))
            RegisterNew(name);
        SubPool pool = m_pools[name];
        return pool.Spawn();
    }

    //回收对象
    public void Unspawn(GameObject go)
    {
        SubPool pool = null;

        foreach (SubPool p in m_pools.Values)
        {
            if (p.Contains(go))
            {
                pool = p;
                break;
            }
        }

        pool.Unspawn(go);
    }

    //回收所有对象
    public void UnspawnAll()
    {
        foreach (SubPool p in m_pools.Values)
            p.UnspawnAll();
    }

    //创建新子池子
    void RegisterNew(string name)
    {
        //预设路径
        string path = "";
        if (string.IsNullOrEmpty(ResourceDir.Trim()))
            path = name;
        else
            path = ResourceDir + "/" + name;

        //加载预设
        GameObject prefab = Resources.Load<GameObject>(path);

        //创建子对象池
        SubPool pool = new SubPool(transform, prefab);
        m_pools.Add(pool.Name, pool);
    }
}