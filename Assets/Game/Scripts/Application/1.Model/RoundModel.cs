using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundModel : Model
{
    #region 常量
    public const float ROUND_INTERVAL = 6f; //回合间隔时间
    public const float SPAWN_INTERVAL = 2.5f; //出怪间隔时间
    #endregion

    #region 事件
    #endregion

    #region 字段
    List<Round> m_Rounds = new List<Round>();//该关卡所有的出怪信息
    int m_RoundIndex = -1; //当前回合的索引
    bool m_AllRoundsComplete = false; //是否所有怪物都出来了
	Coroutine lastRoutine = null;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.M_RoundModel; }
    }

    public int RoundIndex
    {
        get { return m_RoundIndex; }
    }

    public int RoundTotal
    {
        get { return m_Rounds.Count; }
    }

    public bool AllRoundsComplete
    {
        get { return m_AllRoundsComplete; }
    }

    #endregion

	//HU
    #region 方法
    public void LoadLevel(Level level)
    {
        m_Rounds = level.Rounds;
		m_RoundIndex = -1;
		m_AllRoundsComplete = false;
    }
	//HU

    public void StartRound() //开启个携程开始跑
    {
        lastRoutine = Game.Instance.StartCoroutine(RunRound());
    }

    public void StopRound()
    {
		Game.Instance.StopCoroutine(lastRoutine);
    }

    IEnumerator RunRound()
    {
        for (int i = 0; i < m_Rounds.Count; i++)
        {
            //回合开始事件
            StartRoundArgs e = new StartRoundArgs();
            e.RoundIndex = i;
            e.RoundTotal = RoundTotal;
            SendEvent(Consts.E_StartRound, e);

            Round round = m_Rounds[i];

            for (int k = 0; k < round.Count; k++)
            {
                //出怪间隙
                yield return new WaitForSeconds(SPAWN_INTERVAL);

                //出怪事件
                SpawnEnemyArgs ee = new SpawnEnemyArgs();
                ee.EnemyType = round.Enemy;
                SendEvent(Consts.E_SpawnEnemy, ee);
            }
			//HU
			//回合数自加
			m_RoundIndex++;

			//yield return new WaitForSeconds (ROUND_INTERVAL);
            //回合间隙
			if (i < m_Rounds.Count - 1) {
				yield return new WaitForSeconds (ROUND_INTERVAL);
			}
			//HU
        }

        //出怪完成
        m_AllRoundsComplete = true;
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}
