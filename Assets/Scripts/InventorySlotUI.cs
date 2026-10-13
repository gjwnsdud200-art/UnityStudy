using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventorySlotUI : MonoBehaviour
{
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text amountText;
    public int slotIndex;
    public InventoryUI inventoryUI;

    public Image slotBackground; //  클릭한 슬롯 하이라이트용

    public void ClickSlot()
    {
        Debug.Log(slotIndex + "번째 슬롯 클릭");

        inventoryUI.SelectSlot(slotIndex);
    }

    public void Select()
    {
        slotBackground.color = Color.yellow;
    }

    public void Deselect()
    {
        slotBackground.color = Color.white;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
