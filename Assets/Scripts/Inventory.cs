using UnityEngine;
using System.Collections.Generic; // 리스트 자료형을 불러오기위해.

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
        foreach (InventoryItem item in items)
        {
            if (item.itemName == itemName)
            {
                item.amount++;
                return; // 브레이크 대신에 이걸쓰는건 함수자체를 종료하기위해서.
            }
        }
        // 만약 종료되지않고(처음먹는아이템이면) 아래 코드를 실행.
        InventoryItem newItem = new InventoryItem();
        newItem.itemName = itemName;
        newItem.amount = 1;

        items.Add(newItem);
    }



    void Start()
    {
        // null로 채우는건 리스트는 중간노드를 삭제하면 앞으로 당기기때문에 그걸 방지하고자 null로 만들고
        // 삭제하는 행위는 null로 바꾸는 형태로 한다. 이러면 아이템 위치 구분이 가능해짐.
        for (int i = 0; i < maxSlots; i++) 
        {
            items.Add(null);
        }
    }

    void Update()
    {
        
    }
}
