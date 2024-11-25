using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelADS : MonoBehaviour
{
    public Button buttonWatch;
    public Button buttonNo;

    public LevelManager levelManager;

    private void Start()
    {
        buttonWatch.onClick.AddListener(WatchADS);
        buttonNo.onClick.AddListener(ClosePanelADS);
    }

    public void WatchADS()
    {
        Controller.Instance.adBonus.ShowAd(levelManager);
    }
    public void ClosePanelADS()
    {
        levelManager.ResumeTimer();
        gameObject.SetActive(false);
    }
}
