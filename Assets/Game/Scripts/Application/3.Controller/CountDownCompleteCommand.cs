using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CountDownCompleteCommand : Controller
{
    public override void Execute(object data)
    {
        //开始出怪
        RoundModel rModel = GetModel<RoundModel>();
        GameModel gModel = GetModel<GameModel> ();
        gModel.IsPlaying = true;
        rModel.StartRound();
    }
}
