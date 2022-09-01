using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Transform position;

    public void Teleport(GameObject gameObject)
    {
        // We need to do this so the navmeshagent doesn't chuck a fit
        gameObject.SetActive(false);

        // Stop the navmesh from wanting to return
        gameObject.GetComponent<NavMeshAgent>().destination = position.position;
        
        // Move player and camera
        Vector3 difference = position.position - gameObject.transform.position;
        gameObject.transform.position = position.position;
        Camera.main.transform.position += difference;

        // Reenable player
        gameObject.SetActive(true);
    }
}
