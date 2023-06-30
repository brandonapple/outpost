using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChildShip : MonoBehaviour
{
    public static ChildShip instance;
   public enum ShipState
    {
        withMotherShip,
        toPlant,
        landing,
        landed,
        backToMotherShip,
    }
    public ShipState thisShipState = ShipState.withMotherShip;

    public Vector3 shipDir = new Vector3(1, 0, 0);
    public float speed;

    public GameObject childShipHangingPos;
    public GameObject motherShipLibrary;

    public GameObject engineFireEffect;
    public GameObject[] slowDownFireEffects;

    public AudioSource shipAudio;

    private void Awake()
    {
        instance = this;

        shipAudio = GetComponentInChildren<AudioSource>();
        shipAudio.Stop();
        shipAudio.volume = 0;
    }
    private void Start()
    {
        GetRotation();
    }

    public void ShipGO()
    {
        GetRotation();
        thisShipState = ShipState.toPlant;
        MainCam.instance.BeginFellowChildShip();
        SwitchOnFireEffect();
        TipPanel.instance.ShowSkipAnimationTip();

        shipAudio.Play();
    }


    void GetRotation()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, shipDir);
    }

    private void FixedUpdate()
    {
        switch (thisShipState)
        {
            case ShipState.withMotherShip:
                break;
            case ShipState.toPlant:
                shipDir = Vector3.Lerp(shipDir, (Base.instance.transform.position - transform.position).normalized, .01f);
                GetRotation();
                transform.position += shipDir * Time.deltaTime * speed;
                
                if (Vector3.Distance(transform.position,Base.instance.transform.position)<5)
                {
                    thisShipState = ShipState.landing;
                    if (speed>0)
                    {
                        speed -= Time.deltaTime * 1.5f;
                    }
                    SwitchOffFireEffect();
                    SwitchOnSlowDownFireEffect();
                    MainCam.instance.EndFellowShipInPlantAndMoveToTargetPos();

                    shipAudio.Stop();
                    AudioManager.PlayClip("childShipSlowDown");
                }
                else
                {
                    if (speed < 3.5f)
                    {
                        speed += Time.deltaTime * .5f;
                    }
                }
                shipAudio.volume = speed / 3.5f;

                break;


            case ShipState.landing:
                shipDir = Vector3.Lerp(shipDir, Vector3.up, .01f);
                GetRotation();
                transform.position += (Base.instance.transform.position - transform.position).normalized * 1.5f * Time.deltaTime;
                if (Vector3.Distance(transform.position,Base.instance.transform.position)<.1f)
                {
                    thisShipState = ShipState.landed;
                    SwitchOffSlowDownFireEffect();
                    Invoke(nameof(LandingCompleted), 1);
                    EffectManager.instance.SpawnEffect("shipLandedEffect", transform.position, Quaternion.identity);

                    MainCam.instance.ShowBattleField();
                }
               

                break;
            case ShipState.landed:
                transform.position = Base.instance.transform.position;
                break;
            case ShipState.backToMotherShip:

               

                if (Vector3.Distance(childShipHangingPos.transform.position,transform.position)>5f)
                {
                    if (speed < 3.5f)
                    {
                        speed += Time.deltaTime * .5f;
                    }
                }
                else
                {
                    if (speed > 0)
                    {
                        speed -= Time.deltaTime;
                    }
                    shipDir = Vector3.Lerp(shipDir, Vector3.right, .01f);
                    GetRotation();
                    SwitchOffFireEffect();
                    SwitchOnSlowDownFireEffect();
                }

                shipAudio.volume = speed / 3.5f;

                transform.position += Time.deltaTime * speed * (childShipHangingPos.transform.position - transform.position).normalized;
                if (Vector3.Distance(childShipHangingPos.transform.position,transform.position)<.2f)
                {
                    shipAudio.volume = 0;
                    shipAudio.Stop();

                    thisShipState = ShipState.withMotherShip;
                    transform.position = childShipHangingPos.transform.position;
                    shipDir = Vector3.right;
                    GetRotation();
                    SwitchOffSlowDownFireEffect();

                    SpaceRoot.instance.ShipBack();

                    DialoguePanel.instance.StartDialoguesWithString("safeBack");
                }

                break;
            default:
                break;
        }
    }


    public void LandingCompleted()
    {
        SpaceRoot.instance.Hide();
        RelicResourcesManager.instance.CheckAndInitRelicDataSOs();
        RelicManager.instance.CheckIfSpawnRelic();
    }
    public void ShipLunch()
    {
        thisShipState = ShipState.backToMotherShip;
        speed = 0;

        MainCam.instance.BeginFellowChildShip();

        SwitchOnFireEffect();
        EffectManager.instance.SpawnEffect("shipLandedEffect", transform.position, Quaternion.identity);
        shipAudio.volume = 0;
        shipAudio.Play();

    }

    public void MoveDiamondToMotherShip(int value)
    {
        DiamondInSpace diamondInSpace = Instantiate(Resources.Load<DiamondInSpace>("Prefab/diamondInSpace"));
        diamondInSpace.transform.position = transform.position;
        diamondInSpace.motherShipLibrary = motherShipLibrary;
        switch (value)
        {
            case 1:
                break;
            case 10:
                diamondInSpace.transform.localScale =Vector3.one* 2;
                break;
            case 100:
                diamondInSpace.transform.localScale = Vector3.one * 3;
                break;
            case 1000:
                diamondInSpace.transform.localScale = Vector3.one * 4;
                break;
        }
    }
    public void SkipLandingAnimation()
    {
        thisShipState = ShipState.landed;
        transform.position = Base.instance.transform.position;
        shipDir = Vector3.up;
        GetRotation();
        
        SwitchOffFireEffect();
        SwitchOffSlowDownFireEffect();
        shipAudio.volume = 0;
        shipAudio.Play();
    }

    public void SkipBackAnimation()
    {
        thisShipState = ShipState.withMotherShip;
        transform.position = childShipHangingPos.transform.position;
        shipDir = Vector3.right;
        GetRotation();
       
       
        SwitchOffFireEffect();
        SwitchOffSlowDownFireEffect();
        shipAudio.volume = 0;
        shipAudio.Play();

        // DialoguePanel.instance.SafeBackDialogue();
        AchievementManager.instance.ReachAchievement(SteamManager.AchievementType.safeReturn);
    }


    public void SwitchOnFireEffect()
    {
        engineFireEffect.gameObject.SetActive(true);
    }
    public void SwitchOffFireEffect()
    {
        engineFireEffect.gameObject.SetActive(false);
    }



    public void SwitchOnSlowDownFireEffect()
    {
        foreach (GameObject slowDownEffect in slowDownFireEffects)
        {
            slowDownEffect.gameObject.SetActive(true);
        }
    }
    public void SwitchOffSlowDownFireEffect()
    {
        foreach (GameObject slowDownEffect in slowDownFireEffects)
        {
            slowDownEffect.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (SettleMentPanel.instance.showing)
        {
            return;
        }


        SpaceRoot.instance.ChildShipClick();
    }
    public void DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    public void EnableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
