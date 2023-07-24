using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class inventorybutton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] Image HighlightImage;
    [SerializeField] private TextMeshProUGUI text;

    int myIndex;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        if (slot.item == null)
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = slot.item.Icon;
            icon.gameObject.SetActive(true);

            if (slot.item.stackable)
            {
                text.gameObject.SetActive(true);
                text.text = slot.count.ToString();
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
    }


    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPanel itempanel = transform.parent.GetComponent<ItemPanel>();
        itempanel.OnClick(myIndex);
    }



    public void Highlight(bool b)
    {
        HighlightImage.gameObject.SetActive(b);
    }

}
