using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExConstructor : MonoBehaviour      //생성자 예제
{

    private int value;  //사용할 변수 설정

    //생성자
    public ExConstructor(int _value)
    {
        value = _value;     //생성자를 호출 할 때 받은 변수를 메인 변수에 입력
        Debug.Log("객체가 생성 되었습니다. 초기값 : " + value);  //객체가 생성되었을 때 디버그 로그로 받은 값 출력
    }

    
    void Start()
    {
        //생성자 호출
        ExConstructor ex = new ExConstructor(10);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))     //스페이스 키를 눌렀을 때 이 객체 파괴
        {
            Destroy(this.gameObject);
        }
    }

    //Unity에서는 명시적으로 소멸자를 호출 하는 것이 아니라 OnDestory()메서드를 활용하여 객체 파괴 될 때 필요한 작업 수행
    void OnDestroy()
    {
        Debug.Log("객체가 파괴되었습니다.");
    }

}
