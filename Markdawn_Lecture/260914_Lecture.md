260914_Delegate/Event_UnityAction/UnityEvent

## Event와 Delegate
- 플레이어에 Event를 부과하고 UI의 갱신기능을 걸어놓고 Event가 발화할때 걸려있는 UI 일제히 실행


- Delegate : 함수를 담는 타입 (기능을 담아둘수 있는 키워드)

- Event : Delegate를 실행

public delegate void ScoreChangedHandler(int score);
- 함수선언과 유사하지만 델리게이트가 붙으면 ScoreChangedHandler를 타입으로 사용가능

private ScoreChangedHandler _scoreChanged;
- 반환형이 없고 int형을 1개 받는 함수를 받을 수 있다. 

private void Start()  
    {  
        _scoreChanged = PrintScore;  // 함수를 담아줄 수 있다. (함수를 가르키는 참조타입)
        _scoreChanged.Invoke(10);  // 
    }

    private void PrintScore(int score)  
    {  
        Debug.Log($"DelegateSample: score is {score}");  
    }

## 델리게이트 체인
- 담고 빼는 기능

// 담기  
_scoreChanged += PrintScore;  
_scoreChanged += SaveScore;

// 떼기  
_scoreChanged -= SaveScore;

##

- Action 반환값 없는 함수 담을 때
- Func 반환값 있는 함수 담을 때

mvc, mvp 패턴
stringBuilder (문자열 굉장히 많이필요할 때)