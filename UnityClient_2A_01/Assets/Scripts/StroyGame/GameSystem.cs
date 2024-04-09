using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;      //������
using System.Text;      //�ؽ�Ʈ ���
using UnityEngine.UI;
using TMPro;

namespace STORYGAME //�̸� �浹 ����
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]
    public class GameSystemEditor : Editor              //����Ƽ �����͸� ���
    {
        public override void OnInspectorGUI()       //�ν����� �Լ� ������
        {
            base.OnInspectorGUI();                  //���� �ν����͸� �����ͼ� ����

            GameSystem gameSystem = (GameSystem)target;     //���� �ý��� ��ũ��Ʈ Ÿ�� ����

            if(GUILayout.Button("Reset Story Models"))      //��ư ����
            {
                gameSystem.ResetStoryModels();
            }

            if (GUILayout.Button("Assing Text Component by Name"))      //��ư ���� (UI ������Ʈ�� �ҷ��´�)
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

        public static GameSystem instance;      //������ ��Ŭ��ȭ
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
            if (Input.GetKeyDown(KeyCode.Q)) StroyShow(1);      //QŰ�� ������ 1�� ���丮
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
                currentModels = FindStoryModel(number);             //���丮 ���� ��ȣ�� ã�Ƽ�
                delay = 0.1f;
                StartCoroutine(ShowText());                         //��ƾ ����
            }
           
        }

        StoryTableObject FindStoryModel(int number)         //���丮 �� ��ȣ�� ã�� �Լ�
        {
            StoryTableObject tempStoryModels = null;
            for(int i = 0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyNumber == number)       //���ڰ� ���� ���
                {
                    tempStoryModels = storyModels[i];           //�̸� ������ ���� ������ �ְ�
                    break;                                      //for���� ���� ���´�.
                }
            }

            return tempStoryModels;     //���丮 ���� �����ش�.
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
            storyModels = Resources.LoadAll<StoryTableObject>("");  //Resources ���� �Ʒ� ��� StoryModel �ҷ�����
        }
#endif

    }
}
