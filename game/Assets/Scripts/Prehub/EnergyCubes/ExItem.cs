using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExItem : MonoBehaviour
{
    [System.NonSerialized]
    public EnergyManager energyManager;
    [System.NonSerialized]
    public PropertyManager Enemy;

    public void Start()
    {
        Enemy = GameObject.Find("ParamatorManager").GetComponent<PropertyManager>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();

    }

    public void EnergyCount(Enemyinfo Unit)
    {
        energyManager.Energy += Unit.getScore;
        Destroy(this.transform.parent.gameObject);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch(transform.parent.name)
            {
                case "ExCube1(Clone)":
                    EnergyCount(Enemy.Enemies[0]);
                    break;
                case "ExCube2(Clone)":
                    EnergyCount(Enemy.Enemies[1]);
                    break;
                case "ExCube3(Clone)":
                    EnergyCount(Enemy.Enemies[2]);
                    break;
                case "ExCube4(Clone)":
                    EnergyCount(Enemy.Enemies[3]);
                    break;
                case "ExCube5(Clone)":
                    EnergyCount(Enemy.Enemies[4]);
                    break;
            }

            Debug.Log("Hit");
        }
    }
}
