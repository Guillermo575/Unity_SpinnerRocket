using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MathRNG
{
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public int Seed { get; set; }
    private int Next { get; set; }
    public MathRNG(decimal MinValue, decimal MaxValue, int Seed = 1)
    {
        this.MinValue = MinValue;
        this.MaxValue = MaxValue;
        this.Seed = Seed;
        Next = 1;
    }
    public decimal NextValue()
    {
        decimal SeedModify = Seed;
        SeedModify = Next % 3 == 0 ? (decimal)(Seed * 0.0001) : SeedModify;
        SeedModify = Next % 3 == 1 ? (decimal)(Seed * 0.0000001) : SeedModify;
        SeedModify = Next % 3 == 2 ? (decimal)(Seed * 0.000000001) : SeedModify;
        decimal Catalist = Next % 2 == 0 ? (decimal)Mathf.Sin((float)(Next + SeedModify)) : (decimal)Mathf.Sin((float)(Next + SeedModify));
        decimal initialValue = (decimal)((Catalist + 1) / 2);
        decimal PseudoRandom = (initialValue * (MaxValue - MinValue)) + MinValue;
        Next++;
        return PseudoRandom;
    }
    public static void Testing()
    {
        MathRNG obj = new MathRNG(1, 100, 456785763);
        var lst = new List<String>();
        for (int l = 0; l < 100; l++)
        {
            lst.Add(((Int32)obj.NextValue()).ToString());
        }
        var lstgr = lst.Distinct();
        Debug.Log(lstgr.Count());
        //Debug.Log(String.Join(", ", lst));
        //Debug.Log(String.Join(", ", lstgr));
    }
}