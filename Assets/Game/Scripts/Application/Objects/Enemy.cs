using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class Enemy : Role
{
    #region 常量
    public const float CLOSED_DISTANCE = 0.1f;
	float slowdownrate = 0.6f;
    #endregion

    #region 事件
    public event Action<Enemy> Reached;
	public event Action<Enemy> NearReached;
    #endregion

    #region 字段
	//CC
	public GameObject hpbar;
	public GameObject fill;
	//CC

    public EnemyType EnemyType = EnemyType.Enemy0;//怪物类型
    float m_MoveSpeed;//移动速度（米）
    Vector3[] m_Path = null; //路径拐点，在哪里拐
    int m_PointIndex = -1; //下一拐点索引
    bool m_IsReached = false;//是否到达终点
	Animator m_Animator;
	bool attack = false;
    public int Bonus;
	float T = 0;
	bool slowdown = false;
	bool freeze = false;
	GameObject slow;
	GameObject ice;
	float Freezetime = 0;
	float speed = 0;
	float preScale = 1;
    #endregion

    #region 属性
    public float MoveSpeed
    {
        get { return m_MoveSpeed; }
        set { m_MoveSpeed = value; }
    }
    #endregion

    #region 方法

	//被大boss freeze

	public void Freeze(){
		this.freeze = true;
		Freezetime = Time.time;
		this.speed = this.MoveSpeed;
		this.MoveSpeed = 0;
		string prefabName = "HeroFreeze" ;
		ice = Game.Instance.ObjectPool.Spawn(prefabName);
		ice.transform.position = transform.position;
		StartCoroutine (FreezeForSec (6.0f));
	}

	IEnumerator FreezeForSec(float second) {
		GetComponent<Animator> ().speed = 0;
		yield return new WaitForSeconds (second);
		GetComponent<Animator> ().speed = 1.0f;
	}



	//Damage特效
	public override void Damage (int hit)
	{
		if (IsDead)
			return;

		base.Damage (hit);

		//CC

		if (Hp == 0) {
			fill.transform.localScale = new Vector3 (0f, 1.1f, 1.0f);
			return;
		}

		float scaleX = (float)Hp / MaxHp;
		Debug.Log (scaleX);

		Vector3 scale = new Vector3 (scaleX, 1.1f, 1.0f);
		fill.transform.localScale = scale;
		Vector2 diff = new Vector2 ((scaleX - preScale) * 42.0f * 10 / 750, 0);
		fill.transform.Translate(diff,Space.Self);
		preScale = scaleX;
		//CC

	}

	//放慢速度
	public void Slowdown()
	{
		if (this.slowdown||this.freeze)
			return;
		//slowdown特效 add by pan
		slow = Game.Instance.ObjectPool.Spawn ("Slow");
		Vector3 pos1 = transform.position;
		pos1.y = pos1.y + 1.0f;
		slow.transform.position = pos1;
		this.MoveSpeed = this.MoveSpeed * this.slowdownrate;
		T = Time.time;
		this.slowdown = true;

	}

	//告诉走的路径
    public void Load(Vector3[] path)
    {
        m_Path = path;
        MoveNext();
    }

	//是否还有下一个拐点
    bool HasNext()
    {
        return (m_PointIndex + 1) < (m_Path.Length - 1);
    }

    void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    void MoveNext()
    {
        if (!HasNext())
            return;

        if (m_PointIndex == -1)
        {
            //起点位置
            m_PointIndex = 0;
            MoveTo(m_Path[m_PointIndex]);
        }
        else
        {
            //下一点的位置
            m_PointIndex++;
        }
    }
    #endregion

    #region Unity回调
    void Update()
    {
		//unfreeze
		if (this.freeze == true && Time.time - Freezetime >= 6.0f)  {
			this.freeze = false;
			this.MoveSpeed = this.speed;
			Game.Instance.ObjectPool.Unspawn(ice);

		}
		//放慢速度
		if (this.slow == true && this.freeze == false) {
			Vector3 pos1 = transform.position;
			pos1.y = pos1.y + 1.0f;
			slow.transform.position = pos1;

		}

		//恢复速度
		if (slowdown && (Time.time - T) >= 3.0f) {
			Game.Instance.ObjectPool.Unspawn(slow);
			this.MoveSpeed = this.MoveSpeed / this.slowdownrate;
			slowdown = false;
			//Game.Instance.ObjectPool.Unspawn(slow);
		}

        //到达了终点
        if (m_IsReached)
            return;

        //当前位置
        Vector3 pos = transform.position;
        //目标位置
        Vector3 dest = m_Path[m_PointIndex + 1];
        //计算距离
        float dis = Vector3.Distance(pos, dest);
		//短距离攻击
		if (!HasNext() && dis < 20 * CLOSED_DISTANCE && !attack) {
			m_Animator.SetBool ("attackup",true);
			attack = true;
			if (NearReached != null)
				NearReached (this);

		}
		//消失
        if (dis < CLOSED_DISTANCE)
        {
            //到达拐点
            MoveTo(dest);

            if (HasNext())
                MoveNext();
            else
            {
                //到达终点
				m_Animator.SetBool("attackup",false);
				attack = false;
                m_IsReached = true;

                //触发到达终点事件 攻击
                if (Reached != null)
                    Reached(this);
            }
        }
        else
        {
			//移动的单位方向
			Vector3 direction = (dest - pos).normalized;
			if (direction == Vector3.right) {
				m_Animator.SetBool ("right", true);
				m_Animator.SetBool ("up", false);
				m_Animator.SetBool ("down", false);
				m_Animator.SetBool ("left", false);
			} else if (direction == Vector3.up) {
				m_Animator.SetBool ("right", false);
				m_Animator.SetBool ("up", true);
				m_Animator.SetBool ("down", false);
				m_Animator.SetBool ("left", false);
			} else if (direction == Vector3.down) {
				m_Animator.SetBool ("right", false);
				m_Animator.SetBool ("up", false);
				m_Animator.SetBool ("down", true);
				m_Animator.SetBool ("left", false);
			} else if (direction == Vector3.left) {
				m_Animator.SetBool ("right", false);
				m_Animator.SetBool ("up", false);
				m_Animator.SetBool ("down", false);
				m_Animator.SetBool ("left", true);
			}
            //进行帧移动(米/帧 =  米/秒  * Time.deltaTime)
            transform.Translate(direction * m_MoveSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region 事件回调
    public override void OnSpawn()
    {
        base.OnSpawn();

        EnemyInfo info = Game.Instance.StaticData.GetEnemyInfo((int)EnemyType);
        this.MaxHp = info.Hp;
        this.Hp = info.Hp;
        this.MoveSpeed = info.MoveSpeed;
        this.Bonus = info.Bonus;
		    m_Animator = GetComponent<Animator> ();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
		if (freeze) {
			Game.Instance.ObjectPool.Unspawn (ice);
		}
		if (slowdown) {
			Game.Instance.ObjectPool.Unspawn(slow);

		}
		//HU
		this.m_Path = null;
		this.m_PointIndex = -1;
		this.m_IsReached = false;
		this.m_MoveSpeed = 0;
		this.Reached = null;
		//HU
    }
    #endregion

    #region 帮助方法
    #endregion
}
