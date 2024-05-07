using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using STORYGAME;


#if UNITY_EDITOR

public class GameSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameSystem gameSystem = (GameSystem)target;

        if(GUILayout.Button("Reset Story Models"))
        {
            gameSystem.ResetStoryModels();
        }
    }
}
#endif

public class GameSystem : MonoBehaviour
{

    public StoryModel[] storyModels;

    public static GameSystem instance;

    private void Awake()            //Scene�� ���� �� �� �ڵ� �����ܿ��� GameSystem�� �̱���ȭ
    {
        instance = this;
    }

    public enum GAMESTATE
    {
        STORYSHOW,
        WAITSELECT,
        STORYEND,
        BATTLEMODE,
        BATTLEDONE,
        SHOPMODE,
        ENDMODE
    }

    public Stats stats;
    public GAMESTATE currentState;

    public int currentStoryIndex = 1;

    public void ApplyChoice(StoryModel.Reslut result)       //��ư�� ���� ��� ���� �Լ�
    {
        switch (result.resultType)
        {
            case StoryModel.Reslut.ResultType.ChangeHP:
                //stats.currentHpPoint += result.value;
                ChangeState(result);
                break;

            case StoryModel.Reslut.ResultType.AddExperience:
                ChangeState(result);
                break;

            case StoryModel.Reslut.ResultType.GoToNextStory:        //���� ���丮 ����
                currentStoryIndex = result.valuel;                  //���� ���丮 �ε��� �߰�
                StoryShow(currentStoryIndex);                       //�Լ��� ���ؼ� ���丮 �ý��� �����Ͽ� ���丮 ���� ����
                break;

            case StoryModel.Reslut.ResultType.GoToRandomStory:        //���� ���丮 ����
                StoryModel temp = RandomStory();
                StoryShow(temp.storyNumber);
                break;
        }
    }

    public void StoryShow(int number)       //���丮�� �����ִ� �Լ�
    {
        StoryModel tempStoryModel = FindStoryModel(number);

        //StorySystem.Instance.currentStoryModel = tempStoryModels;
        //StorySystem.Instance.CoShowText();
    }


    public void ChangeState(StoryModel.Reslut reslut)       //��� ���� ���� ���� ���� (���� 1���� ����)
    {
        if (reslut.stats.hpPoint > 0) stats.hpPoint += reslut.stats.hpPoint;
        if (reslut.stats.spPoint > 0) stats.spPoint += reslut.stats.spPoint;
        if (reslut.stats.currentHpPoint > 0) stats.currentHpPoint += reslut.stats.currentHpPoint;
        if (reslut.stats.currentSpPoint > 0) stats.currentSpPoint += reslut.stats.currentSpPoint;
        if (reslut.stats.currentXpPoint > 0) stats.currentXpPoint += reslut.stats.currentXpPoint;
        if (reslut.stats.strength > 0) stats.strength += reslut.stats.strength;
        if (reslut.stats.dexterity > 0) stats.dexterity += reslut.stats.dexterity;
        if (reslut.stats.consitiution > 0) stats.consitiution += reslut.stats.consitiution;
        if (reslut.stats.wisdom > 0) stats.wisdom += reslut.stats.wisdom;
        if (reslut.stats.Intelligence > 0) stats.Intelligence += reslut.stats.Intelligence;
        if (reslut.stats.charisma > 0) stats.charisma += reslut.stats.charisma;
    }
    
    StoryModel RandomStory()
    {
        StoryModel tempStoryModels = null;
        List<StoryModel> storyModelList = new List<StoryModel>();

        for (int i = 0; i <storyModels.Length; i++)
        {
            if(storyModels[i].storytype == StoryModel.STORYTYPE.MAIN)
            {
                storyModelList.Add(storyModels[i]);
            }
        }

        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)];
        currentStoryIndex = tempStoryModels.storyNumber;
        Debug.Log("currentStoryIndex" + currentStoryIndex);

        return tempStoryModels;

    }
        

    StoryModel FindStoryModel (int number)          //StoryModel�� �ǵ����ִ� �Լ� ��ȣ�� ã�Ƽ� ����
    {
        StoryModel tempStoryModels = null;
                
        for(int i = 0; i< storyModels.Length; i++)      //for������ �迭 �ȿ� �ִ� ������ �� �����Ϳ���
        {                                               //storyNumber ���� ��ġ�� ��� ���Ƿ� ������ temp�� �־ 
            if (storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
            }
        }

        return tempStoryModels;                         //return ��Ų��
    }


#if UNITY_EDITOR

    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryModel>("");
    }
#endif
}
