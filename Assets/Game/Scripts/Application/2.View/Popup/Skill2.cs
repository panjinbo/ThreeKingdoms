using UnityEngine;
using System.Collections;

public class Skill2 : MonoBehaviour
{
	SpriteRenderer m_Render;
	GameModel gm;
	void Awake()
	{
		m_Render = GetComponent<SpriteRenderer>();
		gm = MVC.GetModel<GameModel>();
	}

	void Update()
	{
		if (!gm.Dazhao) {
			string path = "Res/hero_skillsquare0";
			m_Render.sprite = Resources.LoadAll<Sprite> (path) [1];
		} 
		else {
			string path = "Res/hero_skillsquare";
			m_Render.sprite = Resources.LoadAll<Sprite> (path) [1];
		}
	}


	void OnMouseDown()
	{
		//点击攻击技能
		if (!gm.Dazhao) {
			return;
		}
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject go in objects)
		{
			Enemy enemy = go.GetComponent<Enemy>();

			string prefabName = "HeroBomb" ;
			GameObject bomb = Game.Instance.ObjectPool.Spawn(prefabName);
			bomb.transform.position = enemy.transform.position;
			enemy.Damage (5);

		}
		SendMessageUpwards("Fadazhao",  SendMessageOptions.DontRequireReceiver);

		gm.TT = Time.time;
		gm.Dazhao = false;

		return;
	}
}