using UnityEngine;
using System.Collections;

public interface IReusable
{
    //当取出时调用
    void OnSpawn();

    //当回收时调用
    void OnUnspawn();
}