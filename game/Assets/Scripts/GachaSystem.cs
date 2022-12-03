using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    /*public Gacha ItemGacha1 = new Gacha("Slow", "Use", 1, 1);
    public Gacha ItemGacha2 = new Gacha("Cure", "Use", 2, 1);
    public Gacha ItemGacha3 = new Gacha("Bomb", "Use", 3, 1);
    public Gacha ItemGacha4 = new Gacha("Heist", "Use", 4, 1);
    public Gacha ItemGacha5 = new Gacha("ExpItem", "Exp", 1, 10);*/
    List<Gacha> ItemGachaList = new List<Gacha>();
    List<Gacha> SameRateGachaList = new List<Gacha>();
    Gacha GetItem; 

    // Start is called before the first frame update
    void Start()
    {
        /*ItemGachaList.Add(ItemGacha1);
        ItemGachaList.Add(ItemGacha2);
        ItemGachaList.Add(ItemGacha3);
        ItemGachaList.Add(ItemGacha4);
        ItemGachaList.Add(ItemGacha5);*/
    }

    public Gacha ItemGacha(List<Gacha> Item)
    {
        var randomNumber = Random.Range(1, 101);
        var IdProb = Random.Range(0, 4);
        List<Gacha> SameRateGachaList = new List<Gacha>();
        Gacha GetItem;
        if (randomNumber == 1)
        {
            for (int i = 0; i <= Item.Count - 1; i++)
            {
                if (Item[i].ItemType == "Use")
                {
                    SameRateGachaList.Add(Item[i]);
                }
            }
            GetItem = SameRateGachaList[IdProb];

            Debug.Log(GetItem.Itemname);
        }
        else
        {
            for (int i = 0; i <= Item.Count - 1; i++)
            {

                //Debug.Log(ItemGachaList[i].ItemType);
                if (Item[i].ItemType == "Exp")
                {
                    SameRateGachaList.Add(Item[i]);
                }
            }
            GetItem = SameRateGachaList[0];
        }
        SameRateGachaList.Clear();
        Debug.Log(GetItem.Itemname);
        return GetItem;
    }
}

public struct Gacha
{
    public string Itemname;
    public string ItemType;
    public int id;
    public int GachaRate;
    public GameObject ItemObject;
    public Gacha(string Itemname, string ItemType, int id, int GachaRate, GameObject ItemObject)
    {
        this.Itemname = Itemname;
        this.ItemType = ItemType;
        this.id = id;
        this.GachaRate = GachaRate;
        this.ItemObject = ItemObject;
    }
}
