260909_Lecture

LayerMask : int 형 32바이트 만큼

ProjectSetting -> Physics -> Layer Collision Matrix

Debug.Log(LayerMask.NameToLayer("Enemy"));  // 결과 : 8     // 몇번째 칸
Debug.Log(LayerMask.GetMask("Enemy"));      // 결과 : 256   // 해당 칸 수치

or : Layer 더해줄 때
and : 내가 찾는 Layer 인지 확인할 때

메서드체이닝 // ㄷㄷ

[RequireComponent(typeof(Rigidbody))] // 리지드바디 강제 추가
// using 과 클래스 사이에 작성