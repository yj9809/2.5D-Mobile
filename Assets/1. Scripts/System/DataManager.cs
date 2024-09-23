using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCost
{
    public int baseSpeedUpgradeCost = 100;
    public int baseMaxObjStackCountUpgradeCost = 100;
    public int baseGoldPerBoxUpgradeCost = 100;
    public int baseSpeedUpgradeCount = 0;
    public int baseMaxObjStackCountUpgradeCount = 0;
    public int baseGoldPerBoxUpgradeCount = 0;
    public int baseEmployeeSpeedUpgradeCost = 100;
    public int baseEmployeeMaxObjStackCountUpgradeCost = 100;
    public int baseEmployeeAddCost = 100;
    public int baseEmployeeSpeedUpgradeCount = 0;
    public int baseEmployeeMaxObjStackCountUpgradeCount = 0;
    public int baseEmployeeAddCount = 0;
    public int baseUpgradeMaxCount = 5;

    // Player ������
    public float playerBaseSpeed = 5;
    public float playerBaseCartSpeed = 2.5f;
    public int playerMaxObjStackCount = 3;
    public int playerGold = 100;
    public int playerGoldPerBox = 50;
    // Employee ������
    public float employeeBaseSpeed = 3;
    public float employeeBaseCartSpeed = 1.5f;
    public int employeeBaseMaxObjStackCount = 3;

    public bool step1 = true;
    public bool step2 = false;
    public bool step3 = false;
    public bool step4 = false;
    public bool step5 = false;
    public bool step6 = false;
    public bool step7 = false;
    public bool step8 = false;

    public int guideStep = 0;
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


