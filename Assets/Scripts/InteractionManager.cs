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
        if (touch.fingerId != 0) return;

        if (InputManager.I.CastFromCamera(touch.position, out RaycastHit hit, interactableMask))
        {
            if (F.FastDistance(PlayerMovement.Player.transform.position, hit.transform.position) < interactDistance * interactDistance &&
                hit.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                PlayerMovement.Player.Interact();
            }
        }
    }
}
