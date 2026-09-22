using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    private string itemName;
    // OnTriggerEnter2D()는마음대로 만든 일반 함수가 아니라 Unity가 정해놓은 메시지 함수(Message Function)
    // 따라서 호출하지않아도 작동한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Item Get!");

            Inventory inventory = collision.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem(itemName);
            }

            Destroy(gameObject); // 먹으면 아이템이 사라지도록
        }
    }


    void Start()
    {
        itemName = gameObject.name;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
