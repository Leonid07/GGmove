using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform startParent;
    private Vector3 startPosition; // Здесь сохраняется начальная позиция объекта
    private CanvasGroup canvasGroup;

    [Header("Slot")]
    public bool CorrectSlot = false;

    [Header("анимация тряски")]
    public Coroutine shakeCoroutine;
    public float shakeIntensity = 0.1f;
    public float shakeFrequency = 0.05f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startParent = transform.parent;
        startPosition = transform.position; // Сохраняем начальную позицию при начале перетаскивания

        // Блокируем raycast'ы, чтобы объект не взаимодействовал с интерфейсом
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
        if (Controller.Instance.isVibration == true && CorrectSlot == true)
        {
            Vibration.Vibrate();
        }
        if (CorrectSlot == false && CorrectSlot == true)
        {
            shakeCoroutine = StartCoroutine(ShakeObject());
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; // Установите z-координату на значение, соответствующее вашей сцене
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Разблокируем raycast'ы, чтобы объект снова мог взаимодействовать с интерфейсом
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        // Останавливаем тряску при завершении перетаскивания
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.position = startPosition; // Сбрасываем позицию в исходную
        }

        // Если объект не был сброшен в новый слот, возвращаем его на исходную позицию
        if (transform.parent == startParent)
        {
            transform.position = startPosition; // Вот где используется startPosition
        }
    }
    private IEnumerator ShakeObject()
    {
        while (true)
        {
            Vector3 originalPosition = transform.position;
            float shakeOffsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float shakeOffsetY = Random.Range(-shakeIntensity, shakeIntensity);
            transform.position = new Vector3(originalPosition.x + shakeOffsetX, originalPosition.y + shakeOffsetY, originalPosition.z);
            yield return new WaitForSeconds(shakeFrequency);
        }
    }
    // Метод для обновления startPosition, вызывается из InventorySlot.cs
    public Vector3 UpdateStartPosition()
    {
        return startPosition;
    }
}