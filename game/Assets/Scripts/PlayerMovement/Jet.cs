using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    EnergyManager energyManager;
    Rigidbody jetRb;
    AudioSource JetSound;
    ItemChange itemChange;
    public AudioClip Jetsound;
    Vector3 force;
    bool costs;
    // Start is called before the first frame update
    void Start()
    {
        costs = true;
        JetSound = gameObject.GetComponent<AudioSource>();
        jetRb = gameObject.GetComponent<Rigidbody>();
        energyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        itemChange = GameObject.Find("ItemChangeManager").GetComponent<ItemChange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && jetRb.velocity.y < 9f && energyManager.Energy >= 15f && itemChange.ItemOn[1] == true)
        {
            JetSound.clip = Jetsound;
            //JetSound.PlayOneShot(Jetsound);
            if(jetRb.velocity.y < 2f)
            {
                force = new Vector3(0f, 6f, 0f);
                jetRb.AddForce(force);
            }
            else
            {
                force = new Vector3(0f, 2.5f, 0f);
                jetRb.AddForce(force);
            }
            if (costs == true)
            {
                StartCoroutine(JetPack());
            }
        }
    }

    IEnumerator JetPack()
    {
        costs = !costs;
        energyManager.Energy -= 15;
        yield return new WaitForSeconds(0.25f);
        costs = !costs;
    }
}
