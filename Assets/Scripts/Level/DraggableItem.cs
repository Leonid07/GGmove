using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform startParent;
    private Vector3 startPosition; // ����� ����������� ��������� ������� �������
    private CanvasGroup canvasGroup;

    [Header("Slot")]
    public bool CorrectSlot = false;

    [Header("�������� ������")]
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
        startPosition = transform.position; // ��������� ��������� ������� ��� ������ ��������������

        // ��������� raycast'�, ����� ������ �� ���������������� � �����������
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
        screenPoint.z = 10.0f; // ���������� z-���������� �� ��������, ��������������� ����� �����
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ������������ raycast'�, ����� ������ ����� ��� ����������������� � �����������
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        // ������������� ������ ��� ���������� ��������������
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.position = startPosition; // ���������� ������� � ��������
        }

        // ���� ������ �� ��� ������� � ����� ����, ���������� ��� �� �������� �������
        if (transform.parent == startParent)
        {
            transform.position = startPosition; // ��� ��� ������������ startPosition
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
    // ����� ��� ���������� startPosition, ���������� �� InventorySlot.cs
    public Vector3 UpdateStartPosition()
    {
        return startPosition;
    }
}