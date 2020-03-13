using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Transform Spawnpoint;
    public GameObject Visitor;

    public Text VisitorCountText;
    int VisitorCount = 0;
    public Text VisitorCountInHouseText;
    public int VisitorCountInHouse = 0;
    
    public Text MoneyText;
    float Money = 0;
    float MoneyTick = 10;

    float Timer = 0;
    float TimerTick = 2;
    
    List<GameObject> Visitors = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        VisitorCountText.text = "People: " + VisitorCount.ToString();
        MoneyText.text = "Money: " + Money.ToString();
        VisitorCountInHouseText.text = "InHouse: " + VisitorCountInHouse.ToString();
        Timer += Time.deltaTime;
        if (Timer >= TimerTick && VisitorCountInHouse > 0)
        {
            Money += MoneyTick * VisitorCountInHouse;
            Timer = 0;
        }        
    }
    
    public void AddVisitor()
    {
        GameObject g = Instantiate(Visitor, Spawnpoint.position, Quaternion.identity);
        g.transform.SetParent(this.transform);
        Visitors.Add(g);
        VisitorCount++;        
    }

    public void RemoveVisitor(GameObject g)
    {
        VisitorCount--;
        Visitors.Remove(g);
        Destroy(g);
    }
}
