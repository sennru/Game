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
    int ExItemNumber;
    // Start is called before the first frame update
    public void Start()
    {
        EnergyMng = GameObject.Find("EnergyManager");
        energyManager = EnergyMng.GetComponent<EnergyManager>();
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
                    EnergyCount(Enemy.firstEnemy);
                    break;
                case "ExCube2(Clone)":
                    EnergyCount(Enemy.secondEnemy);
                    break;
                case "ExCube3(Clone)":
                    EnergyCount(Enemy.thirdEnemy);
                    break;
                case "ExCube4(Clone)":
                    EnergyCount(Enemy.lastEnemy);
                    break;
                case "ExCube5(Clone)":
                    EnergyCount(Enemy.boss);
                    break;
            }

            Debug.Log("Hit");
        }
    }
}
