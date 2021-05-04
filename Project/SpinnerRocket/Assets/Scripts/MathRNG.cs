using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MathRNG
{
    #region Variables
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public int Seed { get; set; }
    private int Next { get; set; }
    #endregion

    #region Constructors
    public MathRNG(decimal MinValue, decimal MaxValue, int Seed = 1)
    {
        this.MinValue = MinValue;
        this.MaxValue = MaxValue;
        this.Seed = Seed;
        Next = 1;
    }
    public MathRNG(int Seed = 1)
    {
        this.Seed = Seed;
    }
    #endregion

    #region NextValues
    public decimal NextValue()
    {
        return NextValue(MinValue, MaxValue);
    }
    public decimal NextValue(decimal MinValue, decimal MaxValue)
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
    #endregion

    #region Test Area
    public static void Testing()
    {
        int ciclos = 100;
        int SumatoriaProm = 0;
        int Maximo = 0;
        int Minimo = 0;
        for (int m = 0; m < ciclos; m++)
        {
            MathRNG obj = new MathRNG(1, 100, UnityEngine.Random.Range(1, 999999999));
            var lst = new List<String>();
            for (int l = 0; l < 300; l++)
            {
                lst.Add(((Int32)obj.NextValue()).ToString());
            }
            var lstgr = lst.Distinct();
            SumatoriaProm += lstgr.Count();
            Maximo = lstgr.Count() > Maximo ? lstgr.Count() : Maximo;
            Minimo = lstgr.Count() < Minimo || Minimo == 0 ? lstgr.Count() : Minimo;
            //Debug.Log("Seed " + obj.Seed + ": " + lstgr.Count());
            //Debug.Log(String.Join(", ", lst));
            //Debug.Log(String.Join(", ", lstgr));
        }
        SumatoriaProm = SumatoriaProm / ciclos;
        Debug.Log("Maximo: " + Maximo);
        Debug.Log("Promedio: " + SumatoriaProm);
        Debug.Log("Minimo: " + Minimo);
    }
    #endregion
}