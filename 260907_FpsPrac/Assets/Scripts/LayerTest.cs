using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTest : MonoBehaviour
{
    public LayerMask TargetLayer;
    
    [SerializeField] private float _rayDistance;

    private void Start()
    {
        TargetLayer = TargetLayer.Everything();
    }
    
    public void Update()
    {
        // 이 오브젝트의 위치에서 정면으로 발사되는 Ray 생성.o
        // 레이케스트를 사용해서 감지된 게임 오브젝트의 이름을 출력한다.
        // 레이케스트의 거리는 인스팩터에서 조절할 수 있도록 한다.
        
        // Ray ray = new Ray(transform.position, transform.forward);
        // RaycastHit hit;
        //
        // if (Physics.Raycast(ray, out hit, _rayDistance, TargetLayer))
        // {
        //     Debug.Log(hit.collider.gameObject.name);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = (1 << other.gameObject.layer);

        // if(ContainsLayer(TargetLayer, other))
        if(TargetLayer.Contains(layer))
        {
            Debug.Log("찾음");
        }
    }

    private bool ContainsLayer(LayerMask mask, Collider layer)
    {
        return 0 != (mask.value & (1 << layer.gameObject.layer));
    }
}
