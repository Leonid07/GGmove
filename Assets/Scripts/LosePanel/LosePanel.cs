using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    public LevelManager levelManager;

    [Header("Buttons")]
    public Button buttonOnceAgain;
    public Button buttonHome;

    private void Start()
    {
        buttonOnceAgain.onClick.AddListener(OnceAgain);
        buttonHome.onClick.AddListener(GoToHome);
    }

    void OnceAgain()
    {
        gameObject.SetActive(false);
        levelManager.StartLevel(levelManager.index);
    }
    void GoToHome()
    {
        gameObject.SetActive(false);
        levelManager.gameObject.SetActive(false);
    }
}
