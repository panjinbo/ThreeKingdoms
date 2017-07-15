using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Consts
{
  //目录
  public static readonly string LevelDir =  @"Res/Levels/";
  public static readonly string MapDir = @"Res/Maps/";
  public static readonly string CardDir = @"Res/Cards/";

  //存档
  public const string GameProgress = "GameProgress";
  public const float DotClosedDistance = 0.2f;
  public const float RangeClosedDistance = 0.2f;
	public const float SlowGuardDistance = 2f;

  //Model
  public const string M_GameModel = "M_GameModel";
  public const string M_RoundModel = "M_RoundModel";


  //View
  public const string V_Start = "V_Start";
  public const string V_Select = "V_Select";
  public const string V_Board = "V_Board";
  public const string V_CountDown = "V_CountDown";
  public const string V_Win = "V_Win";
  public const string V_Lost = "V_Lost";
  public const string V_Sytem = "V_Sytem";
  public const string V_Complete = "V_Complete";
   public const string V_Spanwner = "V_Spanwner";
  public const string V_TowerPopup = "V_TowerPopup";
	public const string V_NextDialog = "V_NextDialog";

  //Controller
  public const string E_StartUp = "E_StartUp";

  public const string E_EnterScene = "E_EnterScene"; //SceneArgs
  public const string E_ExitScene = "E_ExitScene";//SceneArgs

  public const string E_StartLevel = "E_StartLevel"; //StartLevelArgs
  public const string E_EndLevel = "E_EndLevel";//EndLevelArgs

  public const string E_CountDownComplete = "E_CountDownComplete";

  public const string E_StartRound = "E_StartRound";//StartRoundArgs
  public const string E_SpawnEnemy = "E_SpawnEnemy";//SpawnEnemyArgs
  public const string E_SpawnTower = "E_SpawnTower";//SpawnTowerArgs
  public const string E_UpgradeTower = "E_UpgradeTower";//UpgradeTowerArgs
  public const string E_SellTower = "E_SellTower";//SellTowerArgs

  public const string E_ShowCreate = "E_ShowCreate";//ShowCreatorArgs
  public const string E_ShowUpgrade = "E_ShowUpgrade";//ShowUpgradeArgs
	public const string E_ShowHero = "E_ShowHero";
  public const string E_HidePopup = "E_HidePopup";
	public const string E_NextDialog = "V_NextDialog";
	public const string E_Fadazhao = "E_Fadazhao";
}

public enum GameSpeed
{
    One,
    Two
}

public enum EnemyType
{
    Enemy0,
    Enemy1,
    Enemy2,
    Enemy3,
    Enemy4,
    Enemy5,
	Enemy6,
	Enemy7
}

public enum HeroType
{
	Hero0,
	Hero1,
}
