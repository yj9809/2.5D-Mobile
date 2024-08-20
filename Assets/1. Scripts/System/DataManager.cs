using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCost
{
    public int baseSpeedUpgradeCost = 10;
    public int baseCartSpeedUpgradeCost = 10;
    public int baseMaxObjStackCountUpgradeCost = 10;
}

public class DataManager : Singleton<DataManager>
{
    public BaseCost baseCost = new BaseCost();
}
