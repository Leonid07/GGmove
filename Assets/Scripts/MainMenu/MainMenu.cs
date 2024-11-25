using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button buttonSimpleLevel;
    public Button buttonMediumLevel;
    public Button buttonHardLevel;
    public Button buttonSettings;
    public Button buttonBackToMenuFromSettings;

    [Header("Level Manager")]
    public GameObject panelSimpleLevel;
    public GameObject panelMediumLevel;
    public GameObject panelHardLevel;
    public GameObject panelSettings;

    [Header("Level scripts")]
    public LevelManager level_1;
    public LevelManager level_2;
    public LevelManager level_3;

    public int indexLevel_1 = 0;
    public int indexLevel_2 = 0;
    public int indexLevel_3 = 0;

    [Header("Параметры для анимации Settings")]
    public CanvasGroup canvasGroupPanelSettings;
    public float duration = 1.0f;

    [Header("кнопки переключения уровней Simple")]
    public Button buttonLeftSimple;
    public Button buttonRightSimple;
    public TMP_Text textLevel_1;
    [Header("кнопки переключения уровней Medium")]
    public Button buttonLeftMedium;
    public Button buttonRightMedium;
    public TMP_Text textLevel_2;
    [Header("кнопки переключения уровней Hard")]
    public Button buttonLeftHard;
    public Button buttonRightHard;
    public TMP_Text textLevel_3;

    [Header("Переключатели разблокированных уровней")]
    public bool unlockLevel_1 = false;
    public bool unlockLevel_2 = false;
    public bool unlockLevel_3 = false;
    public bool unlockLevel_4 = false;
    public bool unlockLevel_5 = false;

    public LevelPanel levelPanel_1;
    public LevelPanel levelPanel_2;
    public LevelPanel levelPanel_3;
    public LevelPanel levelPanel_4;
    public LevelPanel levelPanel_5;

    private void Start()
    {
        canvasGroupPanelSettings = panelSettings.GetComponent<CanvasGroup>();

        buttonSettings.onClick.AddListener(StartCourutineFadeInSettings);
        buttonBackToMenuFromSettings.onClick.AddListener(StartCourutineFadeOutSettings);

    }

    public void StartCourutineFadeOutSettings()
    {
        StartCoroutine(FadeOut());
    }
    public void StartCourutineFadeInSettings()
    {
        panelSettings.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        float startAlpha = canvasGroupPanelSettings.alpha;
        float endAlpha = 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            canvasGroupPanelSettings.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroupPanelSettings.alpha = endAlpha;
        panelSettings.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        float startAlpha = canvasGroupPanelSettings.alpha;
        float endAlpha = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            canvasGroupPanelSettings.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroupPanelSettings.alpha = endAlpha;
    }

    public void CheckLevel()
    {
        if (unlockLevel_2 == true)
        {
            levelPanel_2.isLoad = 1;
            levelPanel_2.IsLock();
            Data.InstanceData.SavePanel();
            unlockLevel_2 = false;
        }
        if (unlockLevel_3 == true)
        {
            levelPanel_3.isLoad = 1;
            levelPanel_3.IsLock();
            Data.InstanceData.SavePanel();
            unlockLevel_3 = false;
        }
        if (unlockLevel_4 == true)
        {
            levelPanel_4.isLoad = 1;
            levelPanel_4.IsLock();
            Data.InstanceData.SavePanel();
            unlockLevel_4 = false;
        }
        if (unlockLevel_5 == true)
        {
            levelPanel_5.isLoad = 1;
            levelPanel_5.IsLock();
            Data.InstanceData.SavePanel();
            unlockLevel_5 = false;
        }
    }

    public void UnlockLevel_2()
    {
        unlockLevel_2 = true;
    }
    public void UnlockLevel_3()
    {
        unlockLevel_3 = true;
    }
    public void UnlockLevel_4()
    {
        unlockLevel_4 = true;
    }
    public void UnlockLevel_5()
    {
        unlockLevel_5 = true;
    }

    #region методы для кнопок уровней
    public void LoadLevelSimple_1_1()
    {
        panelSimpleLevel.SetActive(true);
        level_1.StartLevel(0);
    }
    public void LoadLevelSimple_1_2()
    {
        panelSimpleLevel.SetActive(true);
        level_1.StartLevel(1);
    }
    public void LoadLevelSimple_1_3()
    {
        panelSimpleLevel.SetActive(true);
        level_1.StartLevel(2);
    }
    public void LoadLevelSimple_1_4()
    {
        panelSimpleLevel.SetActive(true);
        level_1.StartLevel(3);
    }
    public void LoadLevelSimple_1_5()
    {
        panelSimpleLevel.SetActive(true);
        level_1.StartLevel(4);
    }
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////  средний уровнь сложности
    /// </summary>
    public void LoadLevelMedium_2_1()
    {
        panelMediumLevel.SetActive(true);
        level_2.StartLevel(0);
    }
    public void LoadLevelMedium_2_2()
    {
        panelMediumLevel.SetActive(true);
        level_2.StartLevel(1);
    }
    public void LoadLevelMedium_2_3()
    {
        panelMediumLevel.SetActive(true);
        level_2.StartLevel(2);
    }
    public void LoadLevelMedium_2_4()
    {
        panelMediumLevel.SetActive(true);
        level_2.StartLevel(3);
    }
    public void LoadLevelMedium_2_5()
    {
        panelMediumLevel.SetActive(true);
        level_2.StartLevel(4);
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////   высокий уровень сложности
    public void LoadLevelHard_3_1()
    {
        panelHardLevel.SetActive(true);
        level_3.StartLevel(0);
    }
    public void LoadLevelHard_3_2()
    {
        panelHardLevel.SetActive(true);
        level_3.StartLevel(1);
    }
    public void LoadLevelHard_3_3()
    {
        panelHardLevel.SetActive(true);
        level_3.StartLevel(2);
    }
    public void LoadLevelHard_3_4()
    {
        panelHardLevel.SetActive(true);
        level_3.StartLevel(3);
    }
    public void LoadLevelHard_3_5()
    {
        panelHardLevel.SetActive(true);
        level_3.StartLevel(4);
    }
    #endregion
}
