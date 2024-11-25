using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image Slot;
    public Image imageHundle;
    public LevelManager levelManager;
    private Vector3 startPosition; // ���� ������ ��� ���������� ������� ������� �������

    [Header("Particle System")]
    public ParticleSystem particleConfety;

    public float activeTime = 2.0f;
    public float inactiveTime = 2.0f;
    public Coroutine toggleCoroutine;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DraggableItem draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
            Image draggedItemImage = eventData.pointerDrag.GetComponent<Image>();

            // ��������� ������� ������� ������� �� ���������

            // �������� ������� �������� � ���������������� ���������
            Sprite draggedSprite = draggedItemImage.sprite;
            Sprite currentSprite = Slot.sprite;

            // ������ ������� �������
            Slot.sprite = draggedSprite;
            draggedItemImage.sprite = currentSprite;

            // ��������� startPosition � DraggableItem
            draggedItem.transform.position = draggedItem.UpdateStartPosition();

            // ��������� ��������� CanvasGroup, ����� ������ ����� ��� ���� ����
            CanvasGroup canvasGroup = eventData.pointerDrag.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
            }

            // ��������� �����
            levelManager.CheckSlot();
        }
    }
    public IEnumerator ToggleActiveInactive()
    {
            imageHundle.gameObject.SetActive(true);
            yield return new WaitForSeconds(activeTime);
            imageHundle.gameObject.SetActive(false);
    }
}