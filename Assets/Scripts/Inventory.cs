using UnityEngine;
using System.Collections.Generic; // 리스트 자료형을 불러오기위해.

[System.Serializable] // InventoryItem은 Unity가 저장/표시해도 되는 데이터 구조라고 알려주는것
public class InventoryItem
{
    public string itemName;
    public int amount;
}


public class Inventory : MonoBehaviour
{
    public int maxSlots = 10;

    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(string itemName)
    {
        Debug.Log("===== AddItem 시작 =====");
        Debug.Log("습득 시도 : [" + itemName + "]");
        Debug.Log("현재 슬롯 수 : " + items.Count);

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                Debug.Log("Slot " + i + " : null");
            }
            else
            {
                Debug.Log(
                    "Slot " + i +
                    " : [" + items[i].itemName +
                    "] x " + items[i].amount
                );
            }
        }

        foreach (InventoryItem item in items)
        {
            if (item != null && item.itemName == itemName) //칸이 비어있지않고 똑같은 이름이 있을때.
            {
             
                item.amount++;
                Debug.Log("같은 아이템 발견! 현재 개수 : " + item.amount);

                return; // 브레이크 대신에 이걸쓰는건 함수자체를 종료하기위해서.
            }
        }
        // 만약 종료되지않고(처음먹는아이템이면) 아래 코드를 실행.

        for (int i = 0; i < items.Count; i++)
        {
            if (string.IsNullOrEmpty(items[i].itemName))
            {
                InventoryItem item_ = new InventoryItem();
                item_.amount = 1;
                item_.itemName = itemName;
                items[i] = item_;

                return;
            }
        }
        Debug.Log("아이템 슬롯이 다 찼습니다..");
    }

     

    void Start()
    {
        Debug.Log("Inventory Start 실행");

        items.Clear();

        // null로 채우는건 리스트는 중간노드를 삭제하면 앞으로 당기기때문에 그걸 방지하고자 null로 만들고
        // 삭제하는 행위는 null로 바꾸는 형태로 한다. 이러면 아이템 위치 구분이 가능해짐.
        for (int j = 0; j < maxSlots; j++) 
        {
            InventoryItem emptyItem = new InventoryItem();
            items.Add(emptyItem);
        }
        Debug.Log("초기화 후 Count : " + items.Count);

    }


    void Update()
    {
        
    }
}
