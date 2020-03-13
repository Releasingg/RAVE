using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour {

    public float WaitingAmount;
    public GameObject item;

    Transform ItemSlot;

	// Use this for initialization
	void Start () {
        ItemSlot = this.gameObject.transform.GetChild(0);
        

        if(item == null)
        {
            WaitingAmount = 1000;
        }
        else
        {
            GameObject g = Instantiate(item, ItemSlot.position, Quaternion.identity);
            g.transform.SetParent(ItemSlot);
            g.name = g.GetComponent<Item>().itemName;
            WaitingAmount = g.GetComponent<Item>().howLongToLook;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float ReturnWaitingAmount()
    {
        return WaitingAmount;
    }
}
