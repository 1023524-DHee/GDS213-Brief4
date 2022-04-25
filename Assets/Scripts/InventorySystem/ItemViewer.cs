using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemViewer : MonoBehaviour, IDragHandler
{
    public static ItemViewer current;
    
    public Transform spawnPosition;
    public Camera camera;
    public float minZoom, maxZoom;
    
    private GameObject _spawnedObject;
    private float _pinchDifference;
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
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;
            _pinchDifference = currentMagnitude - prevMagnitude;
            
            Zoom();
        }
        else
        {
            _isZooming = false;
        }
    }

    public void ShowItem(Item_SO item)
    {
        HideItem();
        _spawnedObject = Instantiate(item.itemModel, spawnPosition);
        camera.enabled = true;
    }

    public void HideItem()
    {
        if(_spawnedObject != null) Destroy(_spawnedObject);
        camera.enabled = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (_isZooming) return;
        _spawnedObject.transform.Rotate(transform.up, -Vector3.Dot(eventData.delta, camera.transform.right) *  0.5f, Space.World);
        _spawnedObject.transform.Rotate(camera.transform.right, Vector3.Dot(eventData.delta, camera.transform.up) * 0.5f, Space.World);
    }

    private void Zoom()
    {
        _currentZoom -= _pinchDifference * 0.01f;
        camera.fieldOfView = Mathf.Clamp(_currentZoom, minZoom, maxZoom);
    }
}
