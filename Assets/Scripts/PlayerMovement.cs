using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Player;

    public LayerMask tapMask;

    private NavMeshAgent agent;
    private Animator anim;

    private void Awake()
    {
        Player = this;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        InputManager.I.OnTouchBegan += OnStartTouch;
    }

    private void OnStartTouch(Touch touch)
    {
        // Only respond to the first concurrent touch
        if (touch.fingerId != 0) return;

        // If a raycast from the player's touch position hits something in the tapMask layer 
        // That is not an interactable object, set the agent's destination to the hit point
        if (InputManager.I.CastFromCamera(touch.position, out RaycastHit hit, tapMask) && !hit.transform.TryGetComponent (out IInteractable interactable))
            agent.SetDestination(hit.point);
    }

    private void Update()
    {
        // Update animator to move at the same speed as the navmesh agent
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void Interact() => anim.SetTrigger("Grab");
}
