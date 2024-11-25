using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public MainMenu mainMenu;

    [SerializeField] Level_1_manager[] maneger;
    public InventorySlot[] slot;

    [Header("Параметры для таймера")]
    public float startTime = 300f; // Время отсчета в секундах
    private float currentTime;
    public Text timerText;
    private bool isPaused = false;
    private Coroutine timerCoroutine; // Переменная для хранения ссылки на корутину

    [Header("Panel Win")]
    public GameObject panelWin;

    [Header("Окно проигрыша")]
    public GameObject panelLose;

    [Header("ADS panel")]
    public GameObject panelADS;

    [Header("Кнопки")]
    public Button buttonBack;
    public Button buttonRestart;
    public Button buttonClue;// подсказка

    public int index = 0;

    public List<Sprite> tableSlot;

    public void Start()
    {
        buttonBack.onClick.AddListener(() => BackMenu());
        buttonRestart.onClick.AddListener(() => StartLevel(index));
        buttonClue.onClick.AddListener(() => ActioveADSPanel());
    }

    public void StartTimer()
    {
        currentTime = startTime;
        UpdateTimerText();
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }
    #region методы для таймера
    IEnumerator TimerCoroutine()
    {
        while (currentTime > 0)
        {
            if (!isPaused)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerText();
            }
            yield return null;
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            UpdateTimerText();
            Debug.Log("Time's up!");
            panelLose.SetActive(true);
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }
    #endregion
    public void BackMenu()
    {
        gameObject.SetActive(false);
    }

    public void StartLevel(int indexLevel)
    {
        index = indexLevel;
        tableSlot.Clear();

        for (int i = 0; i < maneger[indexLevel].tileMap.Length; i++)
        {
            slot[i].Slot.sprite = maneger[indexLevel].tileMap[i];
            tableSlot.Add(slot[i].Slot.sprite);
        }
        for (int i = 0; i < slot.Length; i++)
        {
                slot[i].Slot.GetComponent<DraggableItem>().enabled = true;
                slot[i].Slot.GetComponent<DraggableItem>().CorrectSlot = false;
        }
        RandomizeSlots(indexLevel);
        StartTimer();
    }

    private void RandomizeSlots(int indexLevel)
    {
        for (int i = slot.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            Sprite temp = slot[i].Slot.sprite;
            slot[i].Slot.sprite = slot[randomIndex].Slot.sprite;
            slot[randomIndex].Slot.sprite = temp;
        }
    }

    public void CheckSlot()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].Slot.sprite.name == maneger[index].tileMap[i].name)
            {
                if (slot[i].Slot.GetComponent<DraggableItem>().CorrectSlot == false)
                {
                    slot[i].particleConfety.Play();
                }
                slot[i].Slot.GetComponent<DraggableItem>().CorrectSlot = true;
                slot[i].Slot.GetComponent<DraggableItem>().enabled = false;
            }
        }
        if (check() == true)
        {
            panelWin.SetActive(true);
            mainMenu.CheckLevel();
            Debug.Log("ЗАРАБОТАЛО"); // когда картинка полностью собрана
        }
    }

    public bool check()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].Slot.sprite.name == maneger[index].tileMap[i].name)
            {

            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void SearchSlot()
    {
        StartCoroutine(slot[SearchIndex()].ToggleActiveInactive());
        StartCoroutine(slot[CorrentSearchSlot()].ToggleActiveInactive());
    }

    public int SearchIndex()
    {
        for (int i = 0; i < tableSlot.Count; i++)
        {
            for (int j =0; j < tableSlot.Count; j++)
            {
                if (slot[i].Slot.GetComponent<DraggableItem>().CorrectSlot == false && tableSlot[i].name == slot[j].Slot.sprite.name)
                {
                    return j;
                }
            }
        }
        return 0;
    }
    public int CorrentSearchSlot()
    {
        for (int i = 0; i < tableSlot.Count; i++)
        {
            if (slot[i].Slot.GetComponent<DraggableItem>().CorrectSlot == false)
            {
                return i;
            }
        }
        return 0;
    }

    void ActioveADSPanel()
    {
        panelADS.SetActive(true);
        PauseTimer();
    }

    [Serializable]
    public struct Level_1_manager
    {
        public Sprite[] tileMap;
    }
}