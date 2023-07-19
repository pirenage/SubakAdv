using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsCharacterController : MonoBehaviour
{
    Movement character;
    Rigidbody2D Rb2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfinteraction = 1.2f;
    [SerializeField] MarkerManager markerManager;
    // [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] float maxDistance = 1.5f;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<Movement>();
        Rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        selectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    private void selectTile()
    {
        // selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
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

    private void UseTool()
    {
        Vector2 position = Rb2d.position + character.lastmotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfinteraction);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
