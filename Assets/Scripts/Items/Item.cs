using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public PlayerInventory playerInventory;
    public int ammount;
    public float weight;
    private Rigidbody2D rb;
    public GameObject itemNumText;
    private float counter = 0;
    public float itemLifespan = 10;

    public Item(int _ammount, float _weight)
    {
        ammount = _ammount;
        weight = _weight;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = RandomVector(-5f, 5f);
        itemNumText.GetComponent<ItemNum>().item = this;
        Instantiate(itemNumText, transform.position, Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime);

        if (ammount <= 0 || counter >= itemLifespan)
        {
            Destroy(this.gameObject);
        }
        counter += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerInventory>() != null)
        {
            playerInventory = collider.gameObject.GetComponent<PlayerInventory>();

            while ((playerInventory.weightCarrying + weight <= playerInventory.maxWeight) && (ammount > 0) )
            {
                ammount--;
                playerInventory.weightCarrying += weight;
                addToInventory();
            }
            if (playerInventory.weightCarrying + weight > playerInventory.maxWeight)
            {
                inventoryFull(collider.gameObject);
            }
            
        }
    }

    private Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector2(x, y);
    }

    public void inventoryFull(GameObject player) 
    {
        player.GetComponent<PlayerController>().PopUpText("IVENTORY FULL", 1f);
    }

    public void addToInventory()
    {
        playerInventory.EditItemAmmount(itemName, 1);
    }
}
