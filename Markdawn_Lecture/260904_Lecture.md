260904_Lecture

벨로시티 = 단위벡터 * 속도

레이캐스트
콜라이더 여부 확인

레이 광선어디로 쏠거다

레이케스트힛 구조체

인터페이스는 태그 대신 사용할수 있다.
태그비교
if (오브젝트.transform.CompareTag("태그 명")) // 참 거짓 반환

인터페이스를 태그처럼 사용
if (오브젝트.transform.GetComponent<인터페이스명>());

인터페이스를 태그처럼 사용 및 들어있는지 없는지
if (오브젝트.transform.GetComponent<인터페이스명>()) == null // 인터페이스가 없어서 비어있다.
if (오브젝트.transform.GetComponent<인터페이스명>()) != null // 인터페이스가 있어서 들어갔다.

Physics.SphereCast() // 범위 Ray 비슷
