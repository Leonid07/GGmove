using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public LevelPanel[] levelPanel;

    public static Data InstanceData { get; private set; }

    private void Awake()
    {
        if (InstanceData != null && InstanceData != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceData = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        LoadPanel();
    }

    public void SavePanel()
    {
        for (int i = 0; i < levelPanel.Length; i++)
        {
            PlayerPrefs.SetInt(levelPanel[i].idLevel, levelPanel[i].isLoad);
        }
        PlayerPrefs.Save();
    }
    public void LoadPanel()
    {
        for (int i =0; i < levelPanel.Length; i++)
        {
            if (PlayerPrefs.HasKey(levelPanel[i].idLevel))
            {
                if (levelPanel[i].imageBlock == null)
                {
                    continue;
                }
                levelPanel[i].isLoad = PlayerPrefs.GetInt(levelPanel[i].idLevel);
                levelPanel[i].IsLock();
            }
        }
    }
}
