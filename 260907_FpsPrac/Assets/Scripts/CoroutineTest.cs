using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    [SerializeField] private float _delay;
    private WaitForSeconds _wait;
    private Coroutine _routine;
    private bool isBool;

    private void Awake()
    {
        // YieldInstruction 반복적으로 사용될거라면 캐싱해두기
        _wait = new WaitForSeconds(_delay);
    }
    
    private void Start()
    {
        Debug.Log("Start 시작");
        
        
        
        Debug.Log("Start 종료");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Run();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Stop();
    }

    private void Run()
    {
        if (_routine != null) return; // null이 아니면 이미 들어있고 실행중인 것이기 때문에 리턴
        
        _routine = StartCoroutine(MyRoutine());
    }

    private void Stop()
    {
        if (_routine == null) return; // null이면 실행되는 코루틴이 없기 때문에 리턴
        
        StopCoroutine(_routine); // 스탑해도 변수 내에 참조가 사라지지 않기때문에 null을 넣어준다
        _routine = null;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         isBool = !isBool;
    //     }
    // }
    
    // 함수의 반환형은 'IEnumerator'
    private IEnumerator MyRoutine()
    {
        while (true)
        {
            // 반환할 때는 'yield return'
            // yield return 000 : 000 가 충족되는 상황까지 함수를 '일시정지'하고 대기할 것
            yield return _wait;
            Debug.Log("Coroutine");
        }

        // 루틴을 아예 멈출 때
        yield break; // 코루틴에서는 함수의 리턴과 같은 동작을 함
    }
}
