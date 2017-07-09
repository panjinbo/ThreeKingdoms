using UnityEngine;
using System.Collections;

public class Skill1 : MonoBehaviour
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
			m_Render.sprite = Resources.LoadAll<Sprite> (path) [0];
		} 
		else {
			string path = "Res/hero_skillsquare";
			m_Render.sprite = Resources.LoadAll<Sprite> (path) [0];
		}
			
	}

	void OnMouseDown()
	{
		if (!gm.Dazhao) {
			return;
		}
		//点击冷冻技能

		GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject go in objects) {
			Enemy enemy = go.GetComponent<Enemy> ();
			//被冷冻的效果
			enemy.Freeze ();
		}
		SendMessageUpwards("Fadazhao",  SendMessageOptions.DontRequireReceiver);

		gm.TT = Time.time;
		gm.Dazhao = false;

		return;
	}
}