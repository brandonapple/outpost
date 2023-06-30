using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public enum RobotType
    {
        GoldRobot,
        DiamondRobot
    }
    public RobotType thisRobotType = RobotType.GoldRobot;

    public enum RobotState {
       InBase,
       ToMine,
       ToPikeMineral,
       Mining,
       Back
    }
    public RobotState thisRobotState = RobotState.InBase;
    float timer;

    public float miningSpeed;
    public float moveSpeed;
    bool carryMineral;

    public SpriteRenderer robotSpriteRenderer;
    public Sprite toMineSprite, backBaseSprite;

    WeaponCDIcon weaponCDIcon;
    public DiamondMeteoriteMineral targetDiamondMeteoriteMineral;
    bool carryDiamondMeteoriteMineral;
    private void Awake()
    {
        robotSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        GetData();
        weaponCDIcon = Instantiate(Resources.Load<WeaponCDIcon>("Prefab/WeaponCDPercentCircleIcon"));
        weaponCDIcon.transform.parent = transform;
        weaponCDIcon.transform.localPosition = Vector3.zero;
        weaponCDIcon.SetCDCircle(0);
        weaponCDIcon.SetRadiusMultiplier(3);
    }
    private void FixedUpdate()
    {
        switch (thisRobotState)
        {
            case RobotState.InBase:

                bool charging = false;
                if (DataManager.miningIgnoreMonsterUnlocked)
                {
                    charging = true;
                }
                else 
                {
                    if(MonsterManager.instance.thisGameState == MonsterManager.GameState.MiningTime)
                    {
                        charging = true;
                    }
                }
               

                if (charging)
                {
                    timer += Time.deltaTime;
                    if (timer>.5f)
                    {
                        timer = 0;
                        robotSpriteRenderer.sprite = toMineSprite;
                        robotSpriteRenderer.enabled = true;

                        DiamondMeteoriteMineral[] diamondMeteoriteMinerals = FindObjectsOfType<DiamondMeteoriteMineral>();
                        if (diamondMeteoriteMinerals.Length>0)
                        {
                            DiamondMeteoriteMineral cloestMinetal = diamondMeteoriteMinerals[0];
                            foreach (DiamondMeteoriteMineral mineral in diamondMeteoriteMinerals)
                            {
                                if (Vector3.Distance(transform.position,mineral.transform.position) <
                                    Vector3.Distance(transform.position,cloestMinetal.transform.position))
                                {
                                    cloestMinetal = mineral;
                                }
                            }

                            thisRobotState = RobotState.ToPikeMineral;
                            targetDiamondMeteoriteMineral = cloestMinetal;
                        }
                        else
                        {
                          thisRobotState = RobotState.ToMine;
                        }


                    }
                }






                break;
            case RobotState.ToMine:
                transform.position += dirToMine * Time.deltaTime * moveSpeed * .3f;
                if (distanceToTargetMine<.5f)
                {
                    thisRobotState = RobotState.Mining;
                    transform.position = targetMine.transform.position;
                    robotSpriteRenderer.enabled = false;
                }
                break;

            

            case RobotState.Mining:
                timer += Time.deltaTime * miningSpeed * .25f;

                weaponCDIcon.SetCDCircle(timer / 1.5f);

                if (timer>1.5f)
                {
                    timer = 0;
                    thisRobotState = RobotState.Back;
                    carryMineral = true;

                    robotSpriteRenderer.enabled = true;
                    robotSpriteRenderer.sprite = backBaseSprite;
                    weaponCDIcon.SetCDCircle(0);
                }
                break;
            case RobotState.ToPikeMineral:
                Vector3 dir = (targetDiamondMeteoriteMineral.transform.position - transform.position).normalized;
                transform.position += dir * Time.deltaTime * moveSpeed * .3f;
                float distanceToTargetMinetal = Vector3.Distance(transform.position, targetDiamondMeteoriteMineral.transform.position);
                if (distanceToTargetMinetal<.2f)
                {
                    thisRobotState = RobotState.Back;
                    targetDiamondMeteoriteMineral.transform.position = transform.position + Vector3.up *.3f;
                    targetDiamondMeteoriteMineral.transform.parent = transform;
                    carryDiamondMeteoriteMineral = true;
                    robotSpriteRenderer.sprite = backBaseSprite;
                }

                break;
            case RobotState.Back:
                transform.position += dirToBase * Time.deltaTime * moveSpeed * .3f;
                if (distanceToBase<.5f)
                {
                    thisRobotState = RobotState.InBase;
                    transform.position = Base.instance.transform.position;
                    if (carryMineral)
                    {
                        carryMineral = false;
                        switch (thisRobotType)
                        {
                            case RobotType.GoldRobot:
                                Base.instance.GetGoldMineral();
                                break;
                            case RobotType.DiamondRobot:
                                Base.instance.GetDiamondMineral();
                                break;
                            default:
                                break;
                        }
                    }
                    robotSpriteRenderer.enabled = false;
                    if (targetDiamondMeteoriteMineral && carryDiamondMeteoriteMineral)
                    {
                       
                        carryDiamondMeteoriteMineral = false;
                        int diamondValue = Random.Range(5, 13);
                       
                        switch (targetDiamondMeteoriteMineral.thisMeteoriteType)
                        {
                            case DiamondMeteorite.MeteoriteMineralType.Diamond:
                                Base.instance.GetDiamondMineral(diamondValue);
                                break;
                            case DiamondMeteorite.MeteoriteMineralType.Gold:
                                Base.instance.GetGoldMineral(diamondValue);
                                break;
                            default:
                                break;
                        }

                        Destroy(targetDiamondMeteoriteMineral.gameObject);
                        targetDiamondMeteoriteMineral = null;
                    }
                }
                break;
            default:
                break;
        }
    }
    Vector3 dirToMine
    {
        get
        {
            return (targetMine.transform.position - transform.position).normalized;
        }
    }
    float distanceToTargetMine
    {
        get
        {
            return Vector3.Distance(transform.position, targetMine.transform.position);
        }
    }
    Vector3 dirToBase {
    get
        {
            return (Base.instance.transform.position - transform.position).normalized;
        }
    
    }
    float distanceToBase {
    
      get
        {
            return Vector3.Distance(transform.position, Base.instance.transform.position);
        }
    }
    public Mine targetMine
    {
        get
        {
            return thisRobotType == RobotType.GoldRobot ? Mine.goleMine : Mine.diamondMine;
        }
    }

    public void BackToBaseFast()
    {
       

        thisRobotState = RobotState.Back;
        timer = 0;
        robotSpriteRenderer.enabled = true;
        weaponCDIcon.SetCDCircle(0);
    }

    public void GetData()
    {
        miningSpeed = DataManager.instance.getCurrentValueByString("miningSpeed");
        moveSpeed = DataManager.instance.getCurrentValueByString("miningMovingSpeed");
    }

    public void SetReadyTime(float _timeValue)
    {
        timer = -_timeValue;
    }
}
