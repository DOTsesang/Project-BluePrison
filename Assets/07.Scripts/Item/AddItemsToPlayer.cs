using UnityEngine;

public class AddItemsToPlayer : MonoBehaviour
{
     [Header("인벤토리")]
    public Inventory inventory;
    
    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Item")){
            if(Input.GetKeyDown(KeyCode.E)){
                CheckObject(other.transform.parent.gameObject);
            }
        }
    }
    void CheckObject(GameObject rootedItem) {
        if ( rootedItem != null) {
            IObjectItem clickInterface = rootedItem.GetComponent<IObjectItem>();
            Item item = clickInterface.ClickItem();
            print($"{item.itemName}");
            inventory.AddItem(item);
        }
    }
}
