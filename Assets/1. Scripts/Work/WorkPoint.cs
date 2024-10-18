using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum WorkPointType
{
    Ingredient,
    ConveyorBelt_ingredient,
    BoxPackaging_churu,
    ChuruStorage,
    BoxStorage,
    Truck,
    Packaging,
    Office,
    Store
}
public class WorkPoint : MonoBehaviour
{
    public bool _Scripts = true;
    [HideIfGroup("_Scripts"), SerializeField] private IngredientMaker _ingredientMaker;
    [HideIfGroup("_Scripts"), SerializeField] private ConveyorBelt _conveyorBelt;
    [HideIfGroup("_Scripts"), SerializeField] private BoxStorage _boxStorage;
    [HideIfGroup("_Scripts"), SerializeField] private BoxPackaging _boxPackaging;
    [HideIfGroup("_Scripts"), SerializeField] private Truck truck;

    [Title("WorkPointType")]
    [EnumToggleButtons, SerializeField] private WorkPointType wpType;

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            switch (wpType)
            {
                case WorkPointType.Store:
                    AudioManager.Instance.PlayEffect(EffectType.Store);
                    break;
            }
        }
    }

        private void OnTriggerStay(Collider other)
    {
        Player p = other.GetComponent<Player>();
        Employee e = other.GetComponent<Employee>();
        if (p != null || e != null)
        {
            switch(wpType)
            {
                case WorkPointType.Ingredient:
                    if (p != null) p.TakeObject(_ingredientMaker);
                    if (e != null) e.TakeObject(_ingredientMaker);
                    break;
                case WorkPointType.ConveyorBelt_ingredient :
                    if (p != null) p.GiveObject(_conveyorBelt);
                    if (e != null) e.GiveObject(_conveyorBelt);
                    break;
                case WorkPointType.ChuruStorage:
                    if(p != null) p.GiveObject(_boxStorage, true);
                    if(e != null) e.GiveObject(_boxStorage, true);
                    break;
                case WorkPointType.BoxStorage:
                    if(p != null) p.GiveObject(_boxStorage, false);
                    break;
                case WorkPointType.BoxPackaging_churu:
                    if (p != null) p.GiveObject(_boxPackaging);
                    if (e != null) e.GiveObject(_boxPackaging);
                    break;
                case WorkPointType.Truck:
                    if(p != null) p.GiveObject(truck);
                    break;
                case WorkPointType.Packaging:
                    if(p != null && p.ChuruStack.Count <= 0 && p.BoxStack.Count <= 0 && p.IngredientStack.Count <= 0)
                        _boxPackaging.Packaging(p, e);
                    if (e != null && e.ChuruStack.Count <= 0 && e.BoxStack.Count <= 0 && e.IngredientStack.Count <= 0)
                        _boxPackaging.Packaging(p, e);
                    break;
                case WorkPointType.Office:
                    if(p != null) UIManager.Instance.ShowUpgradeUI();
                    break;
                case WorkPointType.Store:
                    if(p != null) UIManager.Instance.ShowStoreUI();
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            UIManager.Instance.CloseUpgradeUI();
            UIManager.Instance.CloseStoreUI();
            p.StopBoxPackagingAnimationPlayer();
            switch (wpType)
            {
                case WorkPointType.Store:
                    AudioManager.Instance.PlayEffect(EffectType.Store);
                    break;
            }
        }
        
    }
}
