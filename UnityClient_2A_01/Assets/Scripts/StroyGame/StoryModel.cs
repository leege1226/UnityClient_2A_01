using STORYGAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryTableModel")]
public class StoryModel : ScriptableObject
{
    public int storyNumber;             //���丮 ��ȣ
    public Texture2D MainImage;         //���丮 ������ �̹��� �ؽ���

    public enum STORYTYPE               //���丮 Ÿ�� ����
    {
        MAIN,
        SUB,
        SERIAL
    }
    public STORYTYPE storytype;         //���丮 Ÿ�� ����
    public bool storyDone;              //���丮 ���� ����

    [TextArea(10, 10)]                 //�ؽ�Ʈ ���� ǥ��
    public string storyText;           //���� ���丮

    public Option[] options;           //������ �迭

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText;
        public EventCheak eventCheak;
    }

    [System.Serializable]

    public class EventCheak
    {
        public int cheakvalue;

        public enum EventType : int
        {
            NONE,
            GoToBattle,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA
        }

        public EventType eventType;

        public Reslut[] sucessRasult;       //�������� ���� ���� ��� �迭
        public Reslut[] failResult;         //�������� ���� ���� ��� �迭
    }

    [System.Serializable]
    public class Reslut
    {
        public enum ResultType : int
        {
            ChangeHP,
            ChangeSP,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoToRandomStory,
            GotoEnding
        }

        public ResultType resultType;   //����� Ÿ��
        public int valuel;              //��ȭ ��ġ �Է�
        public Stats stats;             //�ش� ���� ��ȭ ��ġ
    }
}
