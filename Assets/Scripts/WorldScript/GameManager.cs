using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDrop dragAndDropController;
    public DayTimeController dayTimeController;
    public DialogueSystem dialogueSystem;

    void Awake()
    {
        instance = this;
    }
}
