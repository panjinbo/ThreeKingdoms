using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class UpgradeTowerCommand : Controller
{
    public override void Execute(object data)
    {
        UpgradeTowerArgs e = data as UpgradeTowerArgs;
        Tower tower = e.tower;
        tower.Tile.Data = null;
        Game.Instance.ObjectPool.Unspawn(e.tower.gameObject);
        //tower.Level++;
    }
}