using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerProbe : MonoBehaviour
{
    [SerializeField] private float _range = 10f;
    [SerializeField] private LayerMask _targetMask;

    private void Update()
    {
        DrawProbeRay();
        ProbeForward();
    }

    private void DrawProbeRay()
    {
        Debug.DrawRay(transform.position, transform.forward * _range, Color.red);
    }

    private void ProbeForward()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _range, _targetMask))
        {
            Debug.Log($"LayerProbe: {hit.collider.name}을 맞혔습니다.");
            Debug.Log($"LayerProbe: 마스크 값은 {_targetMask.value}입니다.");
        }
    }
}
