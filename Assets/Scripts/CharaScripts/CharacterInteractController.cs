using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    Movement characterController;
    Rigidbody2D Rb2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfinteraction = 1.2f;
    Character character;
    [SerializeReference] HighlightController highlightController;

    private void Awake()
    {
        characterController = GetComponent<Movement>();
        Rb2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }
    private void Update()
    {
        Check();


        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

    private void Check()
    {

        Vector2 position = Rb2d.position + characterController.lastmotionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfinteraction);



        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }

        highlightController.Hide();

    }

    private void Interact()
    {
        Vector2 position = Rb2d.position + characterController.lastmotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfinteraction);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }

    }
}
