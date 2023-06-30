using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpaceRoot : MonoBehaviour
{
    public static SpaceRoot instance;
    SpriteRenderer[] sprites;

    public ShipGoButton shipGoButton;
   

    public bool startWithAnimation;
    private void Awake()
    {
        instance = this;
        sprites = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        if (!HistoryManager.openingAnimationShowed)
        {
            HistoryManager.openingAnimationShowed = true;
            HistoryManager.instance.SaveData();
            startWithAnimation = true;
        }
        else
        {
            startWithAnimation = false;
        }

        if (startWithAnimation)
        {
            StartGameOpenAnimation();
        }
        else
        {
            OpeningDialogueOverAndActiveStartButton();
        }
    }
    public void StartGameOpenAnimation()
    {
        StarsRoot.instance.StartGameOpenAnimation();
        startWithAnimation = false;
        shipGoButton.gameObject.SetActive(false);


        if (UpgradePanel.instance)
        {
            UpgradePanel.instance.HideUpgradeButton();
        }
        MainCanvas.instance.HideBasePanel();

        if (GamePlaythroughManager.instance.playThroughTime==0)
        {
            OptionPanelLanguage optionPanelLanguage = Instantiate(Resources.Load<OptionPanelLanguage>("Prefab/UI/OptionPanelLanguage")
                ,MainCanvas.instance.transform);
           // optionPanelLanguage.transform.parent = MainCanvas.instance.transform;
            optionPanelLanguage.transform.localScale = Vector3.one;
        }
      
    }
    public void Show()
    {
        foreach (SpriteRenderer spriteRenderer in sprites)
        {
            spriteRenderer.gameObject.SetActive(true);
        }
        ChildShip.instance.EnableCollider();
    }
    public void Hide()
    {
        foreach (SpriteRenderer spriteRenderer in sprites)
        {
            spriteRenderer.gameObject.SetActive(false);
        }

        Base.instance.SwitchOn();

        ChildShip.instance.DisableCollider();
    }

    [ContextMenu("ship back")]
    public void ShipBack()
    {
        GameManager.instance.ShipBack();
    }
    public void ShipReady()
    {
        shipGoButton.Show();
    }



    public void ShipLunch()
    {
        Base.instance.SwitchOff();
        Show();
        ChildShip.instance.ShipLunch();
        GameManager.instance.ShipLunch();
    }

    public void SkipLandingAnimation()
    {
        MainCam.instance.SkipToPlantPos();
        MainCam.instance.SkipLandingAnimationEndFellowChildShip();
        MainCam.instance.TurnToBattleFieldViewFast();

        ChildShip.instance.LandingCompleted();
        ChildShip.instance.SkipLandingAnimation();
    }

    public void SkipBackAnimation()
    {
        GameManager.instance.ShipLunch();
        ChildShip.instance.SkipBackAnimation();
        MainCam.instance.SkipToMotherShipPos();
        ShipBack();
    }

    public void OpeningDialogueOverAndActiveStartButton()
    {
        shipGoButton.gameObject.SetActive(true);
        if (!MainCanvas.instance) return;
        MainCanvas.instance.ShowCoinManager();
    }

    public void ChildShipClick()
    {
        if (ChildShip.instance.thisShipState == ChildShip.ShipState.toPlant)
        {
            SkipLandingAnimation();
        }
        else if (ChildShip.instance.thisShipState == ChildShip.ShipState.backToMotherShip)
        {
            SkipBackAnimation();

            DialoguePanel.instance.StartDialoguesWithString("safeBack", 1);
        }
    }

    public void MotherShipSetSail()
    {
        StarsRoot.instance.MotherShipSetSail();
        shipGoButton.gameObject.SetActive(false);
       
    }


    public void DestroyGameObject()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.DOFade(0, 1);
        }
        Destroy(gameObject, 1);
    }
}
