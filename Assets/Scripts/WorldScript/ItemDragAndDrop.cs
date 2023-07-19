using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDrop : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;
    [SerializeField] GameObject ItemIcons;
    RectTransform iconTransform;
    Image itemIconImage;

    private void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = ItemIcons.GetComponent<RectTransform>();
        itemIconImage = ItemIcons.GetComponent<Image>();
    }



    private void Update()
    {
        if (ItemIcons.activeInHierarchy == true)
        {
            iconTransform.position = Input.mousePosition;
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {

                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;

                    ItemSpawnManager.instance.SpawnItem(
                         worldPosition,
                         itemSlot.item,
                         itemSlot.count
                         );

                    itemSlot.Clear();
                    ItemIcons.SetActive(false);

                }

            }
        }
    }
    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            Item item = itemSlot.item;
            int count = itemSlot.count;

            itemSlot.Copy(this.itemSlot);
            this.itemSlot.Set(item, count);
        }
        UpdateIcon();
    }


    private void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            ItemIcons.SetActive(false);
        }
        else
        {
            ItemIcons.SetActive(true);
            itemIconImage.sprite = itemSlot.item.Icon;
        }
    }
}
