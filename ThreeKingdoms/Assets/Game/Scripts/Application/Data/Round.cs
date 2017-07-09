using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Round
{
    public int Enemy; //怪物类型ID
    public int Count;   //怪物数量

    public Round(int enemy, int count)
    {
        this.Enemy = enemy;
        this.Count = count;
    }
}