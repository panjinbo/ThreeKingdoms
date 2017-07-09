using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EndLevelCommand : Controller
{
    public override void Execute(object data)
    {
		//HU
		EndLevelArgs e = data as EndLevelArgs;

		//保存游戏状态
		GameModel gm = GetModel<GameModel>();
		gm.StopLevel (e.IsWin);
		RoundModel rModel = GetModel<RoundModel>();
		rModel.StopRound();
		//弹出UI
		if (e.IsWin) {
			GetView<UIWin> ().Show ();
		} else {
			GetView<UILost> ().Show ();
		}
		//HU
    }
}