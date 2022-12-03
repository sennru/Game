using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExItem3 : ExItem
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnergyCount(Enemy.thirdEnemy);
        }
    }
}
