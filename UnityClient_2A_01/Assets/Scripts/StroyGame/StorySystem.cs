using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       //UI�� ��Ʈ�� �� ���̶� �߰�
using System;               //String ���� �Լ� ����ϱ� ���� �߰�

public class StorySystem : MonoBehaviour
{
    public static StorySystem instance;     //������ �̱��� ȭ
    public StoryModel currentStoryModel;    //���� ���丮 �� ����

    public enum TEXTSYSTEM
    {
        NONE,
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                //�� ���ڰ� ��Ÿ���µ� �ɸ��� �ð�
    public string fullText;                  //��ü ǥ���� �ؽ�Ʈ
    private string currentText = "";        //������� ǥ�õ� �ؽ�Ʈ
    public Text textComponent;              //text ������Ʈ UI
    public Text storyIndex;                 //���丮 ��ȣ ǥ���� UI
    public Image imageComponent;            //�̹��� UI

    public Button[] buttonWay = new Button[3];  //������ ��ư �߰�
    public Text[] buttonWayText = new Text[3];  //������ ��ư Text

    public TEXTSYSTEM currentTextShow = TEXTSYSTEM.NONE;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttonWay.Length; i++)          //��ư ���ڿ� ���� �Լ�
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
            buttonWayText[i].text = currentStoryModel.options[i].buttonText;    //��ư �̸� ����
        }
    }

    public void OnWayClick(int index)
    {

        if (currentTextShow == TEXTSYSTEM.DOING)
            return;

        bool CheckEventTypeNone = false;    //�⺻������ none �϶��� ������ �����̶� �Ǵ�. ���� �� �ٽ� �Ҹ��� ���� ���ϱ� ���� bool ����
        StoryModel playStoryModel = currentStoryModel;

        if (playStoryModel.options[index].eventCheak.eventType == StoryModel.EventCheak.EventType.NONE) //��ư���� üũ�� �̺�Ʈ Ÿ���� ������ �������� ����
        {
            for(int i = 0; i < playStoryModel.options[index].eventCheak.sucessRasult.Length; i++)   //���� �� ��� �̺�Ʈ ������ �͵��� �����ϰ� �Ѵ�
            {
                GameSystem.instance.ApplyChoice(currentStoryModel.options[index].eventCheak.sucessRasult[i]);
                CheckEventTypeNone = true;
            }
        }
    }

    public void CoShowText()        //��ü���� ���丮 �� ȣ��
    {
        StoryModelinit();
        ResetShow();
        StartCoroutine(ShowText());
    }

    public void ResetShow()  //���丮 �� �����ְ� �ʱ�ȭ
    {
        textComponent.text = "";

        for(int i = 0; i < buttonWay.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(false);
        }
    }


    IEnumerator ShowText()                              //�ڷ�ƾ �Լ� ���
    {

        currentTextShow = TEXTSYSTEM.DOING;

        if(currentStoryModel.MainImage != null)
        {
            //Texture2D�� Sprite ��ȯ

            Rect rect = new Rect(0, 0, currentStoryModel.MainImage.width, currentStoryModel.MainImage.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(currentStoryModel.MainImage, rect, pivot);

            imageComponent.sprite = sprite;
        }

        else
        {
            Debug.Log("�ؽ��� �ε��� ���� �ʽ��ϴ�. : " + currentStoryModel.MainImage.name);
        }

        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);     //SubString ���ڿ� �ڸ��� �Լ�
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);     //delay �ʸ�ŭ ������ ���� ��Ų��.
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
