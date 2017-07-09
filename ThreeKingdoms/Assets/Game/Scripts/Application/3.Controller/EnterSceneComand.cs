using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnterSceneComand : Controller
{
    public override void Execute(object data)
    {
        SceneArgs e = data as SceneArgs;

        //注册视图（View）
        switch (e.SceneIndex)
        {
            case 0: //Init

                break;
            case 1://Start
                RegisterView(GameObject.Find("UIStart").GetComponent<UIStart>());
                break;
            case 2://Select
                RegisterView(GameObject.Find("Canvas").GetComponent<UIDialog1>());
                break;
            case 3://Level
                RegisterView(GameObject.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Map").GetComponent<Spawner>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UICountDown").GetComponent<UICountDown>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIWin").GetComponent<UIWin>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UILost").GetComponent<UILost>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UISystem").GetComponent<UISystem>());
				        RegisterView(GameObject.Find("TowerPopup").GetComponent<TowerPopup>());
                break;
            case 4://Complete
                RegisterView(GameObject.Find("UIComplete").GetComponent<UIComplete>());
                break;
            default:
                break;
        }
    }
}
