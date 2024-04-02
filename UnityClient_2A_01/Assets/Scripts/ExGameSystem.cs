using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int index;      //맨 앞 소문자
    private string name;
    private ItemType type;
    private Sprite image;
    //멤버들이 private라 접근 불가

    public int Index        //프로퍼터 설정 (같은 이름 맨 앞 대문자)
    {
        get { return index; }
        set { index = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public ItemType Type        //프로퍼티 설정 클래스도 가능
    {
        get { return type; }
        set { type = value; }
    }

    public Sprite Image         //프로퍼티 설정 유니티 클래스도 가능
    {
        get { return Image; }
        set { Image = value; }
    }

    public Item(int index, string name, ItemType type)  //아이템을 생성 하는 생성자 함수
    {
        this.index = index;     //각 변수들을 함수로 받아서 생성함
        this.name = name;
        this.type = type;
    }

}

public enum ItemType    //아이템 종류 설정
{
    Weapon,
    Armor,
    Potion,
    QuestItem       //다른 아이템 타입들을 추가 할 수 있다.
}

public class Inventory
{
    private Item[] items = new Item[16];

    //아이템 인덱서(Indexer)
    public Item this[int index]
    {
        get { return items[index]; }
        set { items[index] = value; }
    }

    //현재 인벤토리에 있는 아이템 수
    public int ItemCount
    {
        get
        {
            int count = 0;  //아이템 수 검사를 위한 변수
            foreach (Item item in items)
            {
                if (item != null) count++;  //아이쳄이 null이 아니면 +1
            }

            return count;
        }

    }

    //아이템 추가
    public bool AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)      //인벤토리 칸을 검사 (for 문으로 앞에서 부터 검사)
        {
            if  (items[i] == null)
            {
                items[i] = item;
                return true;
            }
        }
        return false;       //인벤토리가 가득 차 있을 경우
    }

    //아이템 제거
    public void RemoveItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)      //인벤토리 칸을 검사 (for 문으로 앞에서 부터 검사)
        {
            if (items[i] == item)       //동일한 아이템을 검사하고
            {
                items[i] = null;        //null 값을 넣는다
                break;
            }
        }
    }
}

public class ExGameSystem : MonoBehaviour
{

    private Inventory inventory = new Inventory();  //인벤토리 클래스 선언

    // Start is called before the first frame update
    void Start()
    {
        Item sword = new Item(0, "Sword", ItemType.Weapon);     //아이템 예시 칼 생성
        Item shield = new Item(100, "Shield", ItemType.Armor);  //아이템 예시 방패 생성

        for (int i = 0; i < 5; i++)
        {
            inventory.AddItem(sword);       //생성된 아이템을 가방에 넣는다.
            inventory.AddItem(shield);
        }

        Debug.Log("Player Inventory : " + GetInventoryAsString());
    }

    private string GetInventoryAsString()
    {
        string result = "";
        for (int i = 0; i < inventory.ItemCount; i++)   //인벤토리안에 Item이 있으면 해당 이름 출력
        {
            if(inventory[i] != null)
            {
                result += inventory[i].Name + ",";
            }
        }
        return result.TrimEnd(',');     //마지막 콤마 제거
    }
}
