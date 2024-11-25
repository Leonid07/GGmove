using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public LevelManager levelManager;

    [Header("Button")]
    public Button buttonNext;
    public Button buttonHome;

    public Image imageLight;
    public float rotateSpeed = 100f;

    private void Start()
    {
        buttonNext.onClick.AddListener(() => GoToNext());
        buttonHome.onClick.AddListener(() => GoToHome());
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            // ¬ращение UI элемента вокруг оси Z
            imageLight.transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
            yield return null; // ќжидание до следующего кадра
        }
    }

    void GoToNext()
    {
        int index = (levelManager.index + 1);
        if (index < 4)
        {
            index++;
        }
        else
        {
            index = 4;
        }
        levelManager.StartLevel(index);
        gameObject.SetActive(false);
    }
    void GoToHome()
    {
        gameObject.SetActive(false);
        levelManager.gameObject.SetActive(false);
    }
}
