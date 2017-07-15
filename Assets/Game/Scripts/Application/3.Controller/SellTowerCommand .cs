using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SellTowerCommand : Controller
{
    public override void Execute(object data)
    {
        SellTowerArgs e = data as SellTowerArgs;
        Tower tower = e.tower;

        //清除Tile存储的信息
        tower.Tile.Data = null;

        //半价出售
        GameModel gm = GetModel<GameModel>();
		gm.Score += e.tower.Price / 2;

        //回收
        Game.Instance.ObjectPool.Unspawn(e.tower.gameObject);
    }
}