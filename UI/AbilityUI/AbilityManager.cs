using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    public AbilityContainer currentAbilityContainer;

    public enum AbilityState { idle,choosen}
    public AbilityState thisAbilityState = AbilityState.idle;


    public AbilityRangeIndicator abilityRangeIndicator;

    public LayerMask groundLayer;
    bool stateChanging;

    public bool hideAbilities;
    public List<AbilityContainer> abilityContainerList;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        abilityContainerList = new List<AbilityContainer>();
    }
    public void ChooseAbility(AbilityContainer abilityContainer)
    {
        switch (thisAbilityState)
        {
            case AbilityState.idle:

                currentAbilityContainer = abilityContainer;
                abilityRangeIndicator = Instantiate(Resources.Load<AbilityRangeIndicator>("Abilities/abilityRangeIndicator"));
                thisAbilityState = AbilityState.choosen;
                stateChanging = true;
                abilityRangeIndicator.abilityRadius = abilityContainer.abilityRadius;

                Invoke(nameof(StateChangeOver), .1f);

                break;
            case AbilityState.choosen:
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        


        switch (thisAbilityState)
        {
            case AbilityState.idle:
                break;
            case AbilityState.choosen:

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast( ray,out hit, 1000,groundLayer);
                abilityRangeIndicator.transform.position = hit.point;


                if (stateChanging)
                {
                    return;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    thisAbilityState = AbilityState.idle;
                    Destroy(abilityRangeIndicator.gameObject);
                    AbilityInvoke(hit.point);
                }

                break;
            default:
                break;
        }
    }

    void StateChangeOver()
    {
        stateChanging = false;
    }

    void AbilityInvoke(Vector3 pos)
    {
        currentAbilityContainer.AbilityInvoke();
        switch (currentAbilityContainer.abilityName)
        {
            case "hailStone":
                HailStoneBullet hailStoneBullet =
                    Instantiate(Resources.Load<HailStoneBullet>("Abilities/hailStoneBullet"));
                hailStoneBullet.transform.position = pos;
                break;
            case "lightningChain":
                AbilityLightningChainBullet lightningChainBullet = 
                    Instantiate(Resources.Load<AbilityLightningChainBullet>("Abilities/abilityLightningChainBullet"));
                lightningChainBullet.transform.position = pos;

                break;
            case "stormHammer":
                StormHammerBullet stormHammer =
                    Instantiate(Resources.Load<StormHammerBullet>("Abilities/stormHammerBullet"));
                stormHammer.hammerCenterPos = pos;
                // stormHammer.transform.position = Base.instance.transform.position;
                stormHammer.transform.position = pos;

                break;
            case "godPoke":
                AbilityGodPoke godPoke =
                    Instantiate(Resources.Load<AbilityGodPoke>("Abilities/abilityGodPoke"));
                godPoke.transform.position = pos;
                break;
            case "dollTarget":

                DollTarget dollTarget =
                    Instantiate(Resources.Load<DollTarget>("Abilities/dollTarget"));
                dollTarget.transform.position = pos;
                break;
            case "meteorite":
                MeteoriteBullet meteorite =
                    Instantiate(Resources.Load<MeteoriteBullet>("Abilities/meteoriteBullet"));
                meteorite.transform.position = pos;
                break;
            case "timeFreezon":
                TimeFreezonBullet timeFreezonBullet =
                    Instantiate(Resources.Load<TimeFreezonBullet>("Abilities/timeFreezonBullet"));
                break;
            default:
                break;
        }
    }

    public void AddAbility(string abilityName)
    {
        AbilityContainer abilityContainer = Instantiate(Resources.Load<AbilityContainer>("Prefab/UI/abilityContainer_prefab"));
        abilityContainer.transform.parent = transform;

        abilityContainerList.Add(abilityContainer);

        AbilityDataSO[] dataSOs = Resources.LoadAll<AbilityDataSO>("AbilityData");
        AbilityDataSO targetAbilityDataSO = null;
        foreach (AbilityDataSO dataSO in dataSOs)
        {
            if (dataSO.abilityName == abilityName)
            {
                targetAbilityDataSO = dataSO;
            }
        }

        abilityContainer.abilityName = targetAbilityDataSO.abilityName;
        abilityContainer.abilityIcon = targetAbilityDataSO.abilityIcon;
        abilityContainer.abilityRadius = targetAbilityDataSO.radius;

        abilityContainer.ShowData();
    }


    public void HidePanel()
    {
        transform.localScale = Vector3.zero;
    }
    public void ShowPanel()
    {
        transform.localScale = Vector3.one;
        foreach (AbilityContainer ability in abilityContainerList)
        {
            ability.transform.localScale = Vector3.one;
        }
    }


    public void EmptyAllAbilities()
    {
        foreach (AbilityContainer container in abilityContainerList)
        {
            if (container.gameObject)
            {
                Destroy(container.gameObject);
            }
        }

        abilityContainerList = new List<AbilityContainer>();
    }
}
