using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undifined,
    Tree,
    Ore
}

[CreateAssetMenu(menuName = "Data/Tool Action/Gather Resource Node")]
public class GatherResource : ToolAction
{
    [SerializeField] float sizeOfinteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodesType;
    public override bool OnApply(Vector2 worldPoint)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfinteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanBeHit(canHitNodesType) == true)
                {
                    hit.Hit();
                    return true;
                }
            }
        }
        return false;
    }
}
