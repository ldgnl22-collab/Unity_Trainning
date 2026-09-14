using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickPicker : MonoBehaviour
{
    [SerializeField] private Transform _marker;
    
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ReadClick();
    }

    private void ReadClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PickAtMouse();
        }
    }

    private void PickAtMouse()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.yellow, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"ClickPicker: {hit.collider.name}을 골랐습니다.");
            _marker.position = hit.point;
            // _marker.position = hit.transform.position;
        }
    }
}
