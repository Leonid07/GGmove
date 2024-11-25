using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipePanel : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    public int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    float dragThreshould;

    [Header("Кнопки переключения")]
    public GameObject buttonLeft;
    public GameObject buttonRight;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            CheckButton();
            targetPos += pageStep;
            StartCoroutine(MovePage());
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            CheckButton();
            targetPos -= pageStep; 
            StartCoroutine(MovePage()); 
        }
    }

    public void CheckButton()
    {
        // Отключаем левую кнопку, если текущая страница 1
        if (currentPage == 1)
        {
            buttonLeft.SetActive(false);
        }
        else
        {
            buttonLeft.SetActive(true);
        }

        // Отключаем правую кнопку, если текущая страница 5
        if (currentPage == 5)
        {
            buttonRight.SetActive(false);
        }
        else
        {
            buttonRight.SetActive(true);
        }
    }

    IEnumerator MovePage()
    {
        Vector3 startPos = levelPagesRect.localPosition; 
        float elapsedTime = 0f;

        while (elapsedTime < tweenTime)
        {
            levelPagesRect.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / tweenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        levelPagesRect.localPosition = targetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            StartCoroutine(MovePage());
        }
    }
}