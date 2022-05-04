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
    }

    private void Start()
    {
        InputManager.I.OnTouchBegan += OnStartTouch;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnStartTouch(Touch touch)
    {
        if (touch.fingerId != 0) return;

        if (InputManager.I.CastFromCamera(touch.position, out RaycastHit hit, tapMask) && !hit.transform.TryGetComponent (out IInteractable interactable))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.white, 2);
            agent.SetDestination(hit.point);
        }
    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void Interact() => anim.SetTrigger("Grab");
}
