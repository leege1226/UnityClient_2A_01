using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace STORYGAME
{
    [CreateAssetMenu(fileName = "NewStroy", menuName = "ScriptableObjects/StoryTableObject")]

    public class StoryTableObject : ScriptableObject
    {
        public int storyNumber;     //스토리 번호
        public Enums.StoryType storyType;     //스토리 타입 설정
        public bool stroyDone;

        [TextArea(10, 10)]      //텍스트 영역 표시
        public string storyText;        //메인 스토리
        public List<Option> options = new List<Option>();

        [System.Serializable]
        public class Option
        {
            public string optionText;
            public string buttonText;
            public EventCheek eventCheek;
        }

        [System.Serializable]
        public class EventCheek
        {
            public int checkValue;
            public Enums.EvenType eventType;
            public List<Reslut> successResult = new List<Reslut>();
            public List<Reslut> failResult = new List<Reslut>();
        }

        [System.Serializable]
        public class Reslut
        {
            public Enums.ResultType resultType;
            public int value;
            public Stats stats;
        }
    }

}
