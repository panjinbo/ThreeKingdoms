using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
	[HideInInspector]
	public Map Map = null;

	//关卡列表
	List<FileInfo> m_files = new List<FileInfo>();

	//当前编辑的关卡索引号
	int m_selectIndex = -1;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if(Application.isPlaying)
		{
			//关联的Mono脚本组件
			Map = target as Map;

			EditorGUILayout.BeginHorizontal();
			int currentIndex = EditorGUILayout.Popup(m_selectIndex, GetNames(m_files));
			if (currentIndex != m_selectIndex)
			{
				m_selectIndex = currentIndex;

				//加载关卡
				LoadLevel();
			}
			if (GUILayout.Button("读取列表"))
			{
				//读取关卡列表
				LoadLevelFiles();
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("清除塔点"))
			{
				Map.ClearHolder();
			}
			if (GUILayout.Button("清除路径"))
			{
				Map.ClearRoad();
			}
			EditorGUILayout.EndHorizontal();

			if (GUILayout.Button("保存数据"))
			{
				//保存关卡
				SaveLevel();
			}
		}

		if (GUI.changed)
			EditorUtility.SetDirty(target);
	}

	void LoadLevelFiles()
	{
		//清除状态
		Clear();
		m_selectIndex = 0;
		//加载列表

		//默认加载第一个关卡
			m_selectIndex = 0;
			LoadLevel();
	}

	void LoadLevel()
	{

		Level level = new Level();
		Tools.FillLevel(m_selectIndex, ref level);

		Map.LoadLevel(level);
	}

	void SaveLevel()
	{
		//获取当前加载的关卡
		Level level = Map.Level;

		//临时索引点
		List<Point> list = null;

		//收集放塔点
		list = new List<Point>();
		for (int i = 0; i < Map.Grid.Count; i++)
		{
			Tile t = Map.Grid[i];
			if (t.CanHold)
			{
				Point p = new Point(t.X, t.Y);
				list.Add(p);
			}
		}
		level.Holder = list;

		//收集寻路点
		list = new List<Point>();
		for (int i = 0; i < Map.Road.Count; i++)
		{
			Tile t = Map.Road[i];
			Point p = new Point(t.X, t.Y);
			list.Add(p);
		}
		level.Path = list;

		//路径
		string fileName = m_files[m_selectIndex].FullName;

		//保存关卡
		Tools.SaveLevel(fileName, level);

		//弹框提示
		EditorUtility.DisplayDialog("保存关卡数据", "保存成功", "确定");
	}

	void Clear()
	{
		m_files.Clear();
		m_selectIndex = -1;
	}


	string[] GetNames(List<FileInfo> files)
	{
		List<string> names = new List<string>();
		foreach (FileInfo file in files)
		{
			names.Add(file.Name);
		}
		return names.ToArray();
	}
}
