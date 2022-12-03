using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExItem : MonoBehaviour
{
    [System.NonSerialized]
    public EnergyManager energyManager;
    [System.NonSerialized]
    public enemyUnit Enemy = new enemyUnit();
    [System.NonSerialized]
    public GameObject EnergyMng;
    // Start is called before the first frame update
    public void Start()
    {
        EnergyMng = GameObject.Find("EnergyManager");
        energyManager = EnergyMng.GetComponent<EnergyManager>();
    }

    public void EnergyCount(Enemyinfo Unit)
    {
        energyManager.Energy += Unit.getScore;
        Destroy(this.gameObject);
    }

}
