using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCost
{
    public int baseSpeedUpgradeCost = 10;
    public int baseCartSpeedUpgradeCost = 10;
    public int baseMaxObjStackCountUpgradeCost = 10;
    public bool step1 = true;
    public bool step2 = false;
    public bool step3 = false;
    public bool step4 = false;
    public bool step5 = false;
    public bool step6 = false;
    public bool step7 = false;
    public bool step8 = false;
}

public class DataManager : Singleton<DataManager>
{
    public BaseCost baseCost = new BaseCost();

    public void StepOnOff(int num)
    {
        switch (num)
        {
            case 1:
                baseCost.step1 = true;
                break;
            case 2:
                baseCost.step2 = true;
                break;
            case 3:
                baseCost.step3 = true;
                break;
            case 4:
                baseCost.step4 = true;
                break;
            case 5:
                baseCost.step5 = true;
                break;
            case 6:
                baseCost.step6 = true;
                break;
            case 7:
                baseCost.step7 = true;
                break;
            case 8:
                baseCost.step8 = true;
                break;
        }
    }
}


