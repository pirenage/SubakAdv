using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 5;
    int selectedTool;

    public Action<int> onChange;

    public Item GetItem
    {
        get
        {
            return GameManager.instance.inventoryContainer.slots[selectedTool].item;
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            selectedTool += 1;
            selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
        }
        else
        {
            selectedTool -= 1;
            selectedTool = (selectedTool <= 0 ? toolbarSize - 1 : selectedTool);
        }
        onChange?.Invoke(selectedTool);
    }
}
