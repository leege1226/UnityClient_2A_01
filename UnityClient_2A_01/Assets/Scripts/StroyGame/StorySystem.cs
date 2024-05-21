using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       //UI를 컨트롤 할 것이라 추가
using System;               //String 관련 함수 사용하기 위해 추가

public class StorySystem : MonoBehaviour
{
    public static StorySystem instance;     //간단한 싱글톤 화
    public StoryModel currentStoryModel;    //지금 스토리 모델 참조

    public enum TEXTSYSTEM
    {
        NONE,
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                //각 글자가 나타나는데 걸리는 시간
    public string fullText;                  //전체 표시할 텍스트
    private string currentText = "";        //현재까지 표시된 텍스트
    public Text textComponent;              //text 컴포넌트 UI
    public Text storyIndex;                 //스토리 번호 표시할 UI
    public Image imageComponent;            //이미지 UI

    public Button[] buttonWay = new Button[3];  //선택지 버튼 추가
    public Text[] buttonWayText = new Text[3];  //선택지 버튼 Text

    public TEXTSYSTEM currentTextShow = TEXTSYSTEM.NONE;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttonWay.Length; i++)          //버튼 숫자에 따른 함수
        {
            int wayIndex = i;
            buttonWay[i].onClick.AddListener(() => OnWayClick(wayIndex));

        }
        CoShowText();

    }

    public void StoryModelinit()
    {
        fullText = currentStoryModel.storyText;
        storyIndex.text = currentStoryModel.storyNumber.ToString();

        for(int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWayText[i].text = currentStoryModel.options[i].buttonText;    //버튼 이름 설정
        }
    }

    public void OnWayClick(int index)
    {

        if (currentTextShow == TEXTSYSTEM.DOING)
            return;

        bool CheckEventTypeNone = false;    //기본적으로 none 일때는 무조건 성공이라 판단. 실패 시 다시 불리는 것을 피하기 위해 bool 선언
        StoryModel playStoryModel = currentStoryModel;

        if (playStoryModel.options[index].eventCheak.eventType == StoryModel.EventCheak.EventType.NONE) //버튼에서 체크할 이벤트 타입이 없으면 성공으로 간주
        {
            for(int i = 0; i < playStoryModel.options[index].eventCheak.sucessRasult.Length; i++)   //성공 시 결과 이벤트 설정한 것들을 동작하게 한다
            {
                GameSystem.instance.ApplyChoice(currentStoryModel.options[index].eventCheak.sucessRasult[i]);
                CheckEventTypeNone = true;
            }
        }
    }

    public void CoShowText()        //전체적인 스토리 모델 호출
    {
        StoryModelinit();
        ResetShow();
        StartCoroutine(ShowText());
    }

    public void ResetShow()  //스토리 다 보여주고 초기화
    {
        textComponent.text = "";

        for(int i = 0; i < buttonWay.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(false);
        }
    }


    IEnumerator ShowText()                              //코루틴 함수 사용
    {

        currentTextShow = TEXTSYSTEM.DOING;

        if(currentStoryModel.MainImage != null)
        {
            //Texture2D를 Sprite 변환

            Rect rect = new Rect(0, 0, currentStoryModel.MainImage.width, currentStoryModel.MainImage.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(currentStoryModel.MainImage, rect, pivot);

            imageComponent.sprite = sprite;
        }

        else
        {
            Debug.Log("텍스쳐 로딩이 되지 않습니다. : " + currentStoryModel.MainImage.name);
        }

        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);     //SubString 문자열 자르는 함수
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);     //delay 초만큼 포문을 지연 시킨다.
        }

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delay);

        currentTextShow = TEXTSYSTEM.NONE;
    }
    
}
