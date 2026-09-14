using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDrawer : MonoBehaviour
{
    private const string TAG_TARGET = "Target";
    
    [SerializeField] private float _range = 2f;

    private void Update()
    {
        DrawProbeRay();
        ProbeForward();
    }

    private void DrawProbeRay()
    {
        Debug.DrawRay(transform.position, transform.forward * _range, Color.green);
    }

    private void ProbeForward()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _range))
        {
            ReportHit(hit);
        }
    }

    private void ReportHit(RaycastHit hit)
    {
        if (hit.collider.CompareTag(TAG_TARGET))
        {
            Debug.Log($"RayDrawer: 대상 {hit.collider.name}와의 거리는 {hit.distance}");
        }
    }
}
