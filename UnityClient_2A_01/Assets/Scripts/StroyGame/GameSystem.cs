using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;      //에디터
using System.Text;      //텍스트 사용

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
        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {

        public static GameSystem instance;      //간단한 싱클톤화

        private void Awake()
        {
            instance = this;
        }

        public StoryTableObject[] storyModels;

#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]

        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryTableObject>("");  //Resources 폴더 아래 모든 StoryModel 불러오기
        }
#endif

    }
}
