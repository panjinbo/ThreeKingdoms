using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level
{
    //名字
    public string Name;

    //卡片
    public string CardImage;

    //背景
    public string Background;

    //路径
    public string Road;

    //金币
    public int InitScore;

    public int RowNum;

    public int ColNum;

    //炮塔可放置的位置
    public List<Point> Holder = new List<Point>();

    //怪物行走的路径
    public List<Point> Path = new List<Point>();

    //出怪回合信息
    public List<Round> Rounds = new List<Round>();
}
