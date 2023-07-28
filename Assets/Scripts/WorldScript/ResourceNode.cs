using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;
    [SerializeField] Item item;
    [SerializeField] int itemCountInOneDrop = 1;
    [SerializeField] ResourceNodeType nodeType;
    [SerializeField] AudioClip choppingSound;

    public override void Hit()
    {
        AudioManager.instance.Play(choppingSound);
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 pos = transform.position;
            pos.x += spread * UnityEngine.Random.value - spread / 2;
            pos.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(pos, item, itemCountInOneDrop);
        }
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
