260915_Coroutine_Animation

## Coroutine
- 단일 쓰레드 에서 돌아가는 동기 방식
- 숙련좀 되면 UniTask 알아보기

private float _time = 2f;
    
    private void Start() => StartCoroutine(MyRoutine());

    private IEnumerator MyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);
            // 만나면        조건이 충족될때까지 대기
            Debug.Log("시간 경과");
        }
    }


    private void Start()
    {
        Debug.Log("Start 시작");
        StartCoroutine(MyRoutine());
        Debug.Log("Start 종료");
    }

    private IEnumerator MyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);
            Debug.Log("시간 경과");
        }
    }
    결과
    
Start 시작
UnityEngine.Debug:Log (object)
CoroutineTest:Start () (at Assets/Scripts/CoroutineTest.cs:11)

Start 종료
UnityEngine.Debug:Log (object)
CoroutineTest:Start () (at Assets/Scripts/CoroutineTest.cs:13)

시간 경과
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__2:MoveNext () (at Assets/Scripts/CoroutineTest.cs:21)
UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)

시간 경과
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__2:MoveNext () (at Assets/Scripts/CoroutineTest.cs:21)
UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)

시간 경과
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__2:MoveNext () (at Assets/Scripts/CoroutineTest.cs:21)
UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)

// 함수의 반환형은 'IEnumerator'
    private IEnumerator MyRoutine()
    {
        
    }


## 비동기가 아닌 이유
- 유니티 라이프사이클 내에서 돌아간다


    private void Start()
    {
        Debug.Log("Start 시작");
        
        // O 시작 : StartCoroutine();
        // X : MyRoutine();
        StartCoroutine(MyRoutine());
        // 멈출 때 : StopCoroutine();
        
        Debug.Log("Start 종료");
    }
    
    // 함수의 반환형은 'IEnumerator'
    private IEnumerator MyRoutine()
    {
        Debug.Log("Coroutine 1");
        
        // 반환할 때는 'yield return'
        // yield return 000 : 000 가 충족되는 상황까지 함수를 '일시정지'하고 대기할 것
        yield return new WaitForSeconds(1f);
        Debug.Log("Coroutine 2");
        yield return new WaitForSeconds(1f);
        Debug.Log("Coroutine 3");
        yield return new WaitForSeconds(1f);
        Debug.Log("Coroutine 4");
        yield return new WaitForSeconds(1f);
        Debug.Log("Coroutine 5");
    }

    결과

Start 시작
UnityEngine.Debug:Log (object)
CoroutineTest:Start () (at Assets/Scripts/CoroutineTest.cs:9)

Coroutine 1
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__1:MoveNext () (at Assets/Scripts/CoroutineTest.cs:22)
UnityEngine.MonoBehaviour:StartCoroutine (System.Collections.IEnumerator)
CoroutineTest:Start () (at Assets/Scripts/CoroutineTest.cs:13)

Start 종료
UnityEngine.Debug:Log (object)
CoroutineTest:Start () (at Assets/Scripts/CoroutineTest.cs:16)

Coroutine 2
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__1:MoveNext () (at Assets/Scripts/CoroutineTest.cs:27)
UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)

Coroutine 3
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__1:MoveNext () (at Assets/Scripts/CoroutineTest.cs:29)
UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)

Coroutine 4
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__1:MoveNext () (at Assets/Scripts/CoroutineTest.cs:31)
UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)

Coroutine 5
UnityEngine.Debug:Log (object)
CoroutineTest/<MyRoutine>d__1:MoveNext () (at Assets/Scripts/CoroutineTest.cs:33)
UnityEngine.SetupCoroutine:InvokeMoveNext (System.Collections.IEnumerator,intptr)

- ~ 까지 기다렸다가 실행

- // 루틴을 아예 멈출 때
- `yield break;` // 코루틴에서는 함수의 리턴과 같은 동작을 함

## 반복적으로 사용시

- yield return new WaitForSeconds(1f);
- 새로운 클래스를 생성한다

[SerializeField] private float _delay;
    private WaitForSeconds _wait;

    private void Awake()
    {
        // YieldInstruction 반복적으로 사용될거라면 캐싱해두기
        _wait = new WaitForSeconds(_delay);
    }

- // 사용
    yield return _wait;
    Debug.Log("Coroutine 2");


- yield return new WaitUntil();
- 델리게이트를 가지고 있고, 추가적인 참조타입 생성
- 괄호의 조건이 참이 될 때 까지 기다린다.
- Bool 형태의 함수를 받는 델리게이트

- yield return new WaitUntil(() => isBool);
- //                      매개변수 =>
- //                            (람다식)

yield return new WaitWhile();
- 거짓일때 까지 기다린다.

## 남용하면
- 클래스를 자주 생성하는 구조(스타트코루틴의 반환형이 클래스임)

## 반복적, 중간에 온오프가 필요할 때

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



## 익명함수
## 람다식

## Animation
- 
- 


