using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool triggerOnce = false;

    public UnityEvent<GameObject> onTriggered;
    public TriggerType triggerType { get; private set; }
    public LayerMask triggerMask;

    public UnityEvent<GameObject> onEnter;
    public UnityEvent<GameObject> onExit;


    public enum TriggerType
    {
        Enter,
        Exit,
        Both
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((triggerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            if (triggerType == TriggerType.Enter || triggerType == TriggerType.Both)
            {
                onTriggered.Invoke(other.gameObject);
            }

            onEnter.Invoke(other.gameObject);

            if (triggerOnce) gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((triggerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            if (triggerType == TriggerType.Exit || triggerType == TriggerType.Both)
            {
                onTriggered.Invoke(other.gameObject);
            }

            onExit.Invoke(other.gameObject);

            if (triggerOnce) gameObject.SetActive(false);
        }
    }
}
