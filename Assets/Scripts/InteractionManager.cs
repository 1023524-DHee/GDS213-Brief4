using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float interactDistance;
    public LayerMask interactableMask;

    private void Start()
    {
        InputManager.I.OnTouchBegan += OnStartTouch;
    }

    private void OnStartTouch(Touch touch)
    {
        // Only respond to the first concurrent touch
        if (touch.fingerId != 0) return;

        // If a raycast from the player's touch position hits something in the interactable layer 
        if (InputManager.I.CastFromCamera(touch.position, out RaycastHit hit, interactableMask))
        {
            // If the player is within interactDistance and hit object is interactable
            if (F.FastDistance(PlayerMovement.Player.transform.position, hit.transform.position) < interactDistance * interactDistance &&
                hit.transform.TryGetComponent(out IInteractable interactable))
            {
                // Interact with object and trigger player's interact animation
                // It is recommended that this direct call of the player's Interact function is updated
                // Possibly replaced by an event that the player can subscribe to
                interactable.Interact();
                PlayerMovement.Player.Interact();
            }
        }
    }
}
