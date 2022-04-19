using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        InputManager.I.OnTouchBegan += OnStartTouch;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnStartTouch(Touch touch)
    {
        if (touch.fingerId != 0) return;

        if (InputManager.I.CastFromCamera(touch.position, out RaycastHit hit))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.white, 2);
            agent.SetDestination(hit.point);
        }
    }
}
