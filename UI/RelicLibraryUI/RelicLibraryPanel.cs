using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicLibraryPanel : MonoBehaviour
{
    public static RelicLibraryPanel instance;

    public RelicLibrarySingleContainer containerPrefab;
    public List<RelicLibrarySingleContainer> containerList;
    public Sprite lockSprite;
    public Color lockContainerBGColor;
    public Color unlockContainerBGColor;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ShowRelicSOs();
    }
    public void ShowRelicSOs()
    {

        #region clear container list
        foreach (RelicLibrarySingleContainer container in containerList)
        {
            if (container.gameObject)
            {
                Destroy(container.gameObject);
            }
        }
        containerList = new List<RelicLibrarySingleContainer>();
        #endregion 

        #region spawn relic containers
        RelicDataSO[] relicDataSOs = Resources.LoadAll<RelicDataSO>("RelicSOs");
        containerPrefab.gameObject.SetActive(true);
        foreach (RelicDataSO relicDataSO in relicDataSOs)
        {
            RelicLibrarySingleContainer container = Instantiate(containerPrefab);
            container.transform.parent = containerPrefab.transform.parent;
            containerList.Add(container);
            container.currentRelicDataSO = relicDataSO;
            container.ShowRelicData();  
        }
        containerPrefab.gameObject.SetActive(false);
        #endregion
    }
}
