using UnityEngine;
using System.Collections;

public abstract class Bullet : ReusbleObject, IReusable
{
    //类型
    public int ID { get; private set; }
    //等级
    public int Level { get; set; }
    //基本速度
    public float BaseSpeed { get; private set; }
    //基本攻击力
    public int BaseAttack { get; private set; }

    //移动速度
    public float Speed { get { return BaseSpeed * Level; } }
    //攻击力
    public int Attack { get { return BaseAttack * Level; } }

    //地图范围
    public Rect MapRect { get; private set; }

    //延迟回收时间(秒)
    public float DelayToDestory = 0.2f;

    //是否爆炸
    protected bool m_IsExploded = false;

    //动画组件
    Animator m_Animator;


    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }

    public void Load(int bulletID, int level, Rect mapRect)
    {
        MapRect = mapRect;

        this.ID = bulletID;
        this.Level = level;

        BulletInfo info = Game.Instance.StaticData.GetBulletInfo(bulletID);
        this.BaseSpeed = info.BaseSpeed;
        this.BaseAttack = info.BaseAttack;
    }


	public void Explode()
	{

		//标记已爆炸
		m_IsExploded = true;  

		//暴击特效 add by pan

		GameObject go = Game.Instance.ObjectPool.Spawn ("Baoji");
		Vector3 pos = transform.position;
		Debug.Log (pos);
		pos.y = pos.y + 0.8f;
		pos.x = pos.x + 0.08f;
		Debug.Log (pos);
		go.transform.position = pos;

		//播放爆炸动画
		m_Animator.SetTrigger("IsExplode");

		//延迟回收
		StartCoroutine("DestoryCoroutine");
	}


    IEnumerator DestoryCoroutine()
    {
        //延迟
        yield return new WaitForSeconds(DelayToDestory);

        //回收
        Game.Instance.ObjectPool.Unspawn(this.gameObject);
    }

    public override void OnSpawn()
    {

    }

    public override void OnUnspawn()
    {
        m_IsExploded = false;
        m_Animator.Play("Play");
        m_Animator.ResetTrigger("IsExplode");
    }
}
