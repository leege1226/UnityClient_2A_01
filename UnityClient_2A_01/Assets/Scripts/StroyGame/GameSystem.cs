using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;      //에디터
using System.Text;      //텍스트 사용
using UnityEngine.UI;
using TMPro;

namespace STORYGAME //이름 충돌 방지
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]
    public class GameSystemEditor : Editor              //유니티 에디터를 상속
    {
        public override void OnInspectorGUI()       //인스펙터 함수 재정의
        {
            base.OnInspectorGUI();                  //기존 인스펙터를 가져와서 실행

            GameSystem gameSystem = (GameSystem)target;     //게임 시스템 스크립트 타겟 설정

            if(GUILayout.Button("Reset Story Models"))      //버튼 생성
            {
                gameSystem.ResetStoryModels();
            }

            if (GUILayout.Button("Assing Text Component by Name"))      //버튼 생성 (UI 컴포넌트를 불러온다)
            {
                GameObject textObject = GameObject.Find("StoryTextUI");
                if (textObject != null)
                {
                    Text textComponent = textObject.GetComponent<Text>();
                    if (textComponent != null)
                    {
                        gameSystem.textComponect = textComponent;
                        Debug.Log("Text Componet assigned Successfully");
                    }
                }
            }

        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {

        public static GameSystem instance;      //간단한 싱클톤화
        public Text textComponect = null;

        public float delay = 0.1f;
        public string fullText;
        public string currentText = "";

        public enum GAMESTATE
        {
            STORYSHOW,
            WAITSELECT,
            STROYEND,
            ENDMODE
        }

        public GAMESTATE currentState;
        public StoryTableObject[] storyModels;
        public StoryTableObject currentModels;
        public int currentStoryIndex;
        public bool showStroy = false;
        

        private void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            StartCoroutine(ShowText());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) StroyShow(1);      //Q키를 누르면 1번 스토리
            if (Input.GetKeyDown(KeyCode.W)) StroyShow(2);
            if (Input.GetKeyDown(KeyCode.E)) StroyShow(3);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                delay = 0.0f;
                
            }
        }

        public void StroyShow(int number)
        {
            if(!showStroy)
            {
                currentModels = FindStoryModel(number);             //스토리 모델을 번호로 찾아서
                delay = 0.1f;
                StartCoroutine(ShowText());                         //루틴 실행
            }
           
        }

        StoryTableObject FindStoryModel(int number)         //스토리 모델 번호로 찾는 함수
        {
            StoryTableObject tempStoryModels = null;
            for(int i = 0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyNumber == number)       //숫자가 같은 경우
                {
                    tempStoryModels = storyModels[i];           //미리 선언해 놓은 변수에 넣고
                    break;                                      //for문을 빠져 나온다.
                }
            }

            return tempStoryModels;     //스토리 모델을 돌려준다.
        }

        IEnumerator ShowText()
        {
            showStroy = true;
            for(int i =0; i <= currentModels.storyText.Length; i++)
            {
                currentText = currentModels.storyText.Substring(0, i);
                textComponect.text = currentText;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(delay);
            showStroy = false;
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]

        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryTableObject>("");  //Resources 폴더 아래 모든 StoryModel 불러오기
        }
#endif

    }
}
