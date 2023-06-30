using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DialoguePanel : MonoBehaviour
{
    public static DialoguePanel instance;
    public GameObject assistantDialogueContainer;
    public GameObject commanderDialogueContainer;

    public enum DialogueState {empty,talking,over};
    public DialogueState thisDialogueState = DialogueState.empty;

    public dialogue[] currentDialogues;
    public int dialogueIndex;
    public string currentDialogusTheme = "null";

    private void Awake()
    {
        instance = this;
        assistantDialogueContainer.transform.localScale = Vector3.zero;
        commanderDialogueContainer.transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        switch (thisDialogueState)
        {
            case DialogueState.empty:
                break;
            case DialogueState.talking:
                if (Input.GetMouseButtonDown(0))
                {
                    if(dialogueIndex>= currentDialogues.Length)
                    {
                        thisDialogueState = DialogueState.over;
                        TalkOver();
                    }
                    else
                    {
                        DialogueContinue();
                    }
                 
                }
                break;
            case DialogueState.over:
                break;
            default:
                break;
        }

    }
    public void TalkBegin()
    {
        thisDialogueState = DialogueState.talking;
        DialogueContinue();
       
    }
    public void StartDialoguesWithString(string _string)
    {
        currentDialogusTheme = _string;
        DialogueGroupSO[] dialogusGroupSOs = Resources.LoadAll<DialogueGroupSO>("Dialogues/"+_string);
        currentDialogues = dialogusGroupSOs[Random.Range(0, dialogusGroupSOs.Length)].dialogues;
        DialogueContinue();
        thisDialogueState = DialogueState.talking;

        if (_string == "relic")
        {
            MainCanvas.instance.HideBasePanel();
            GameSpeedManager.instance.GamePause();
        }
        if (_string == "lose")
        {
            MainCanvas.instance.HideBasePanel();
        }
    }
    public void StartDialoguesWithString(string _string,float delayTime)
    {
       

        StartCoroutine(Talk());
        IEnumerator Talk()
        {
            yield return new WaitForSeconds(delayTime);
            StartDialoguesWithString(_string);
        }
    }
    void DialogueContinue()
    {
        CharacterTalk(currentDialogues[dialogueIndex].characterName,
            (OptionsManager.languageIndex == 0)?currentDialogues[dialogueIndex].contentEn : currentDialogues[dialogueIndex].content);

        //currentDialogues[dialogueIndex].content); ;;
        dialogueIndex++;


    }
    public void CharacterTalk(string _characterName,string _dialogueContent)
    {
        switch (_characterName)
        {
            case "assistant":
                ContainerShowContent(assistantDialogueContainer, _dialogueContent);
                ShowContainer(assistantDialogueContainer);
                HideContainer(commanderDialogueContainer);
                break;
            case "commander":
                ContainerShowContent(commanderDialogueContainer, _dialogueContent);
                ShowContainer(commanderDialogueContainer);
                HideContainer(assistantDialogueContainer);
                break;
            default:
                break;
        }
        AudioManager.PlayClip("dialogue");
    }
    public void TalkOver()
    {
        HideContainer(assistantDialogueContainer);
        HideContainer(commanderDialogueContainer);
        dialogueIndex = 0;

        if (currentDialogusTheme == "gameOpen")
        {
            SpaceRoot.instance.OpeningDialogueOverAndActiveStartButton();

            if (UpgradePanel.instance)
            {
                UpgradePanel.instance.ShowButton();
            }
            GameModeChoosePanel gameModeChoosePanel = Instantiate(Resources.Load<GameModeChoosePanel>("Prefab/UI/GameModeChoosePanel"));
            gameModeChoosePanel.transform.parent = transform.parent.transform;
            gameModeChoosePanel.transform.localPosition = Vector3.zero;
            gameModeChoosePanel.transform.localScale = Vector3.one;

        }
        else if (currentDialogusTheme == "relic")
        {
            if (GameManager.instance.thisGameState == GameManager.GameState.inSpace)
            {
                return;
            }
            MainCanvas.SpawnRelicBooty();
        }
        else if (currentDialogusTheme == "gameEnd")
        {
            GameEndPanel gameEndPanel = Instantiate(Resources.Load<GameEndPanel>("Prefab/UI/GameEndPanel"));
            gameEndPanel.transform.parent = transform.parent.transform;
            gameEndPanel.transform.localPosition = Vector3.zero;
        }
        else if (currentDialogusTheme == "lose")
        {
            MainCanvas.instance.ShowBasePanel();
        }
    }

    void ShowContainer(GameObject container)
    {
        container.gameObject.SetActive(true);
        container.transform.DOScale(1, .1f).SetUpdate(true);
        container.GetComponentInChildren<TextLoader>().GetText();
    }
    void HideContainer(GameObject container)
    {
        container.transform.DOScale(0, .1f).SetUpdate(true);

        StartCoroutine(DisableContainer());
        IEnumerator DisableContainer()
        {
            yield return new WaitForSeconds(.1f);
            container.gameObject.SetActive(false);
        }
    }
    void ContainerShowContent(GameObject container,string content)
    {
        Text text = container.GetComponentInChildren<Text>();
        text.text = "";
       
        text.DOText(content, 1).SetUpdate(true);
    }
}

[System.Serializable]
public struct dialogue
{
    public string characterName;
    public string content;
    public string contentEn;
}
