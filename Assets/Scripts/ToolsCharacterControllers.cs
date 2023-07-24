using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterControllers : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D Rb2d;
    ToolBarController toolBarController;
    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfinteraction = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] ToolAction onTilePickUp;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController2D>();
        Rb2d = GetComponent<Rigidbody2D>();
        toolBarController = GetComponent<ToolBarController>();
    }
    private void Update()
    {
        selectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();
        }
    }

    private void selectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }

    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = Rb2d.position + character.lastmotionVector * offsetDistance;
        Item item = toolBarController.GetItem;
        if (item == null) { return false; }
        if (item.onAction == null) { return false; }

        animator.SetTrigger("act");
        bool complete = item.onAction.OnApply(position);
        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
            }

        }
        return complete;
    }

    private void UseToolGrid()
    {
        if (selectable == true)
        {
            Item item = toolBarController.GetItem;
            if (item = null)
            {
                PickUptile();
                return;
            }
            if (item.onTilemapAction == null) { return; }
            animator.SetTrigger("act");

            bool complete = item.onTilemapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController, item);
            if (complete == true)
            {
                if (item.onItemUsed != null)
                {
                    item.onItemUsed.OnItemUsed(item, GameManager.instance.inventoryContainer);
                }
            }

        }
    }

    private void PickUptile()
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }
}
