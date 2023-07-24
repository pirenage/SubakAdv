using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openChest;
    [SerializeField] bool Isopen;
    
    public override void Interact(Character character)
    {
        if(Isopen == false)
        {
            Isopen = true;
            closedChest.SetActive(false);
            openChest.SetActive(true);
            
        }
    }
}