using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Inventory inventory;

    private bool isOpen = false;

    public GameObject slotPrefab;
    public List<GameObject> slots = new List<GameObject>();


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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpen = false;
        inventoryPanel.SetActive(false);

        for (int i = 0; i < inventory.maxSlots; i++)
        {
            GameObject newSlot = Instantiate(
                slotPrefab,
                inventoryPanel.transform
                );

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
    }
}
