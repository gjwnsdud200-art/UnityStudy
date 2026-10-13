using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Inventory inventory;

    public Transform slotContainer;

    private bool isOpen = false;

    public GameObject slotPrefab;
    public List<GameObject> slots = new List<GameObject>();

    private int selectedsSlotIndex = -1; // -1 = 아직 아무 슬롯도 선택하지 않음
    void UpdateInventoryUI()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            InventoryItem item = inventory.items[i];

            InventorySlotUI slotUI = slots[i].GetComponent<InventorySlotUI>();
            /*이건 해당 GameObject에 붙어 있는 컴포넌트 중에서
            "InventorySlotUI 타입의 컴포넌트 있으면 가져와."라는 뜻*/

            //프리팹에 InventorySlotUI.cs를 붙여놓았음.

            if (string.IsNullOrEmpty(item.itemName))
            {
                slotUI.itemNameText.text = "";
                slotUI.amountText.text = "";
                slotUI.itemImage.sprite = null;
                slotUI.itemImage.enabled = false;

                continue;
            }

            slotUI.itemNameText.text = item.itemName;
            slotUI.amountText.text = "x" + item.amount;

            slotUI.itemImage.enabled = true;
            slotUI.itemImage.sprite = item.itemSprite;
        }
    }

    public void CloseInventory()
    {
        isOpen = false;
        inventoryPanel.SetActive(isOpen);
    }

    public void SelectSlot(int slotIndex)
    {
        InventoryItem item = inventory.items[slotIndex];

        if (string.IsNullOrEmpty(item.itemName))
        {
            Debug.Log("빈 슬롯");
            return;
        }

        for (int i = 0; i < slots.Count; i++) // 모든 슬롯 선택표시 해제
        {
            InventorySlotUI slotUI = slots[i].GetComponent<InventorySlotUI>();

            slotUI.Deselect();
        }
        //클릭한 슬롯만 선택표시
        InventorySlotUI selectedSlot = slots[slotIndex].GetComponent<InventorySlotUI>();

        selectedSlot.Select();


        selectedsSlotIndex = slotIndex;
        Debug.Log("InventoryUI가 받은 슬롯 번호 : " + slotIndex);
        Debug.Log("선택한 아이템 : " + item.itemName);
    }

    public void DropSelectedItem()
    {
        if (selectedsSlotIndex == -1)
        {
            Debug.Log("선택된 아이템이 없습니다.");
            return;
        }

        InventoryItem item = inventory.items[selectedsSlotIndex];

        item.amount--;

        if (item.amount <= 0)
        {
            item.itemName = "";
            item.amount = 0;
            item.itemSprite = null;

            InventorySlotUI slotUI = slots[selectedsSlotIndex].GetComponent<InventorySlotUI>();

            slotUI.Deselect();

            selectedsSlotIndex = -1;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpen = false;
        inventoryPanel.SetActive(false);

        for (int i = 0; i < inventory.maxSlots; i++)
        {
            GameObject newSlot = Instantiate(
                slotPrefab,
                slotContainer
                );
            InventorySlotUI slotUI = newSlot.GetComponent<InventorySlotUI>();// 클래스 자체의 존재 여부는 게임 실행 전에 이미 확인되기때문
            slotUI.slotIndex = i; // 각 인벤토리 슬롯이 몇번인지 알기위해.
            //InventorySlotUI라는 클래스가 존재하는지는 C# 컴파일러가 확인하고, 
            //그 클래스의 컴포넌트가 특정 GameObject에 실제로 붙어 있는지는 GetComponent가 런타임에 확인한다.

            slotUI.inventoryUI = this;

            slots.Add(newSlot);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;

            inventoryPanel.SetActive(isOpen);
            /*
             * isOpen = true
                → SetActive(true)
                → InventoryPanel 보임

                isOpen = false
                → SetActive(false)
                → InventoryPanel 사라짐
             */

            if (isOpen)
            {
                UpdateInventoryUI();
            }
        }

        if (isOpen)
        {
            UpdateInventoryUI();
        }
    }
}
