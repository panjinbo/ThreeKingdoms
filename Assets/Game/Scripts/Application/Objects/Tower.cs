using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Tower : ReusbleObject
{
	public string [] ntrigger= {
		"n1",
		"n2",
		"n3",
		"n4",
		"n5",
		"n6",
		"n7",
		"n8",
		"n9",
		"n10",
		"n11",
		"n12",
		"n13",
		"n14",
		"n15",
		"n16",
		"n17",
		"n18",
		"n19",
		"n20",
		"n21",
		"n22",
		"n23",
		"n24",
		"n25",
		"n26",
		"n27",
		"n28",
		"n29",
		"n30",
		"n31",
		"n32",
		"n33",
		"n34",
		"n35",
		"n36",
		"center"
	};
	public string[] ptrigger = {
		"p1",
		"p2",
		"p3",
		"p4",
		"p5",
		"p6",
		"p7",
		"p8",
		"p9",
		"p10",
		"p11",
		"p12",
		"p13",
		"p14",
		"p15",
		"p16",
		"p17",
		"p18",
		"p19",
		"p20",
		"p21",
		"p22",
		"p23",
		"p24",
		"p25",
		"p26",
		"p27",
		"p28",
		"p29",
		"p30",
		"p31",
		"p32",
		"p33",
		"p34",
		"p35",
		"p36",
		"center"
	};
	public int preindex=-1;
    public int ID { get; private set; }
    public int Level
    {
        get { return m_Level; }
        set
        {
            m_Level = Mathf.Clamp(value, 0, MaxLevel);
            transform.localScale = Vector3.one * (1 + m_Level * 0.25f);
        }
    }
    public int MaxLevel { get; private set; }
    public bool IsTopLevel { get { return Level >= MaxLevel; } }
    public float ShotRate { get; private set; }
    public float GuardRange { get; private set; }
    public int BasePrice { get; private set; }
    public int UseBulletID { get; private set; }
    public int Price { get { return BasePrice * Level; } }
    public Tile Tile { get; private set; }
    public Rect MapRect { get; private set; }
	public int name{ get; private set;}

	Enemy m_Target;
    Animator m_Animator;
    int m_Level;
    float m_LastShotTime = 0;

    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();


    }

    void Update()
    {
        if (m_Target == null)
        {
			//To Find All enemey.
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in objects)
            {
				Enemy enemy = go.GetComponent<Enemy>();

				if (!enemy.IsDead && Vector3.Distance(transform.position, enemy.transform.position) <= GuardRange)
                {
					m_Target = enemy;
					//for(int i=0;i<12;i++)
					//	m_Animator.SetBool (ttrigger[i], false);
					//m_Animator.SetBool ("t0", true);

                    break;
                }
            }
        }
        else
        {
            if (m_Target.IsDead || Vector3.Distance(transform.position, m_Target.transform.position) > GuardRange)
            {
                m_Target = null;

                LookAt(null);
                return;
            }

            //朝向目标
			if (name!=1)
               LookAt(m_Target);

            //发射判断
            if (Time.time >= m_LastShotTime + 1f / ShotRate)
            {
                //创建子弹
                Shot(m_Target);

                //记录发射时间
                m_LastShotTime = Time.time;
            }
        }
    }

    public void Load(int towerID, Tile tile,Rect mapRect)
    {
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
        MaxLevel = info.MaxLevel;
        ShotRate = info.ShotRate;
        BasePrice = info.BasePrice;
        GuardRange = info.GuardRange;
        UseBulletID = info.UseBulletID;
        Level = 1;
		name = info.ID;
        Tile = tile;
        MapRect = mapRect;
    }

	public virtual void Shot(Enemy enemy)
    {
        m_Animator.SetTrigger("IsAttack");
    }

	void LookAt(Enemy target)
    {
        if (target == null)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            transform.eulerAngles = eulerAngles;
			m_Animator.ResetTrigger ("IsAttack");
			for (int i = 0; i < 36; i++) {
				m_Animator.SetBool (ntrigger [i], false);
				m_Animator.SetBool (ptrigger [i], false);
			}
			//m_Animator.Play ("center0");
        }
        else
        {
			Vector3 vec = (transform.position-target.Position).normalized;
            float angle = Mathf.Atan2(vec.y, vec.x);
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = angle * Mathf.Rad2Deg + 90;
			angle =(angle * 57.32f+450)%360;
			//  transform.eulerAngles = eulerAngles;
			if (preindex==-1) {
				int index = (int)angle;
				index = index / 10;
				m_Animator.SetBool ("center", false);
				m_Animator.SetBool (ntrigger[index], true);
				preindex = index;



			} else {

				int index = ((int)angle) / 10;

				if (index > preindex) {
					m_Animator.SetBool (ntrigger [(index + 1)], false);
					m_Animator.SetBool (ptrigger [(index - 1)], false);
					if (index - preindex <= 18) {
						for (int i = preindex; i < index; i++) 
							//m_Animator.SetBool (ttrigger [i], false);
							m_Animator.SetBool (ntrigger [i + 1], true);
						
					}
					else
						{
						int len = preindex + 36 - index;
						for (int t = 1; t <= len; t++) 
						{
							int temp = preindex - t;
							if (temp <= 0)
								temp = temp + 36;
							m_Animator.SetBool (ptrigger [temp], true);
						}
						}
				   }

				else if(index<preindex)
				{
					m_Animator.SetBool (ntrigger [(index+1)], false);
					m_Animator.SetBool (ptrigger [((index+35)%36)], false);
					if (preindex - index <= 18) {
						for (int i = preindex; i >index; i--) 
							//m_Animator.SetBool (ttrigger [i], false);
							m_Animator.SetBool (ptrigger [i-1], true);

					}
					else
					{
						int len = index + 36 - preindex;
						for (int t = 1; t <= len; t++) 
						{
							int temp = preindex+t;
							temp = temp % 36;
							m_Animator.SetBool (ntrigger [temp], true);
						}
					}
				}
				preindex = index;


			}
        }
    }

    public override void OnSpawn()
    {
        
    }

    public override void OnUnspawn()
    {
		if (name != 1) {
			for (int i = 0; i < 36; i++) {
				m_Animator.SetBool (ntrigger [i], false);
				m_Animator.SetBool (ptrigger [i], false);
			}
			m_Animator.SetBool ("center", true);
			m_Animator.Play ("center0");
		} else 
		{
			m_Animator.ResetTrigger ("IsAttack");
			m_Animator.Play ("center0");
		}
        m_Target = null;
        m_LastShotTime = 0;

        Tile = null;

        Level = 0;
        MaxLevel = 0;
        ShotRate = 0;
        BasePrice = 0;
    }
}