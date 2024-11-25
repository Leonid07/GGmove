using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public Slider loadingSlider;

    public GameObject mainMenu;
    public Animator animator;

    public Text loadingText;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }
    IEnumerator LoadSceneAsync()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            loadingSlider.value++;
            loadingText.text = $"{loadingSlider.value / loadingSlider.maxValue * 100f:0}%"; // Обновляем текстовое значение прогресса
            if (loadingSlider.value == loadingSlider.maxValue)
            {
                gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
                mainMenu.SetActive(true);
                animator.Play("AnimMainMenu");
                yield break;
            }
        }
    }
}
