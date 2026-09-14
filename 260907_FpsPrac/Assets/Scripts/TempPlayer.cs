using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TempPlayer : MonoBehaviour
{
    public UnityEvent TempEvent;
    
    // IntChange : 반환형이 없고, int 매개변수를 1개 받는
    //             함수를 담아둘 수 있는 타입니다.
    public delegate void IntChange(int value);

    private int _health;

    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            // OnHealthChange(Health); // 위험 ( 널 체크 안됨 )
            OnHealthChange?.Invoke(_health); // 조금더 안전
        }
    }

    public event Action<int> OnHealthChange;

    public ObservableProperty<float> Exp = new(0);
    //public Action<int, float, string> a;
    
    // public event IntChange OnHealthChange; // 앞에 On 붙여주는 것이 관습처럼 쓰임
    // event 키워드는 외부에서 실행하는 상황을 방지
    // 필요시 키워드 제거

    private void OnEnable()
    {
        
    }

    private void TryLoadData(Action s, Action f)
    {
        // 로드
        // if (성공시)
        // {
        //     s.Invoke(); // 역으로 호출하는 것을 콜백 이라고 함
        // }
        // else
        // {
        //     f.Invoke();
        // }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) TakeDamage(5);
        if(Input.GetKeyDown(KeyCode.Alpha2)) Heal(10);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Exp.Value += 20.5f;
        if (Input.GetKeyDown(KeyCode.Alpha4)) TempEvent?.Invoke();
    }

    // private void OnDestroy() => Exp.RemoveAllListeners();
    
    private void Init()
    {
        Exp = new ObservableProperty<float>(0);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("데미지 받음");
        Health -= damage;
    }

    public void Heal(int heal)
    {
        Debug.Log("회복했다");
        Health += heal;
    }
}
