using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Item viewer for the player inventory.
 */
public class ItemViewer : MonoBehaviour, IDragHandler
{
    public static ItemViewer current;
    
    public Transform spawnPosition;
    public Camera camera;
    public float minZoom, maxZoom;
    public Shader standardMat;
    
    private GameObject _spawnedObject;
    private float _currentZoom = 30f;
    private bool _isZooming;
    
    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            _isZooming = true;
            Zoom();
        }
        else
        {
            _isZooming = false;
        }
    }

    // Spawns the item, enables the camera and changes the material shader to the standard shader
    public void ShowItem(Item_SO item)
    {
        if(_spawnedObject != null) Destroy(_spawnedObject);
        
        camera.enabled = true;
        _spawnedObject = Instantiate(item.itemModel, spawnPosition);
        _spawnedObject.layer = LayerMask.NameToLayer("Inspection");
        foreach (Material material in _spawnedObject.GetComponent<MeshRenderer>().materials)
        {
            material.shader = standardMat;
        }
    }

    // Destroys the item and disables the camera
    public void HideItem()
    {
        if(_spawnedObject != null) Destroy(_spawnedObject);
        camera.enabled = false;
    }
    
    // Zoom functionality
    private void Zoom()
    {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
        float currentMagnitude = (touch0.position - touch1.position).magnitude;
        float pinchDifference = currentMagnitude - prevMagnitude;
        
        _currentZoom -= pinchDifference * 0.01f;
        camera.fieldOfView = Mathf.Clamp(_currentZoom, minZoom, maxZoom);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (_isZooming) return;
        _spawnedObject.transform.Rotate(transform.up, -Vector3.Dot(eventData.delta, camera.transform.right) *  0.5f, Space.World);
        _spawnedObject.transform.Rotate(camera.transform.right, Vector3.Dot(eventData.delta, camera.transform.up) * 0.5f, Space.World);
    }
}
