using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetParametor : MonoBehaviour
{
    PlayerUnit PlayerObject;
    PlayerMove Playermove;
    WorldTime Timer;
    ScoreManager ScoreUI;
    EnergyManager ResetEnergy;
    Lifter lifter;
    GunController Bullets;
    DamageAndCostManager DCparam;
    GameObject[] Enemies, EnergyCubes;
    GameObject Player;
    public void Start()
    {
        Player = GameObject.Find("Player");
        PlayerObject = GameObject.Find("Player").GetComponent<PlayerUnit>();
        Playermove = GameObject.Find("Player").GetComponent<PlayerMove>();
        Timer = GameObject.Find("WorldTimer").GetComponent<WorldTime>();
        ScoreUI = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        ResetEnergy = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        Bullets = GameObject.Find("Shooting").GetComponent<GunController>();
        lifter = GameObject.Find("LifterCube").GetComponent<Lifter>();
        DCparam = GameObject.Find("ParamatorManager").GetComponent<DamageAndCostManager>();
    }
    public void ResetParam()
    {
        for(int i = 0; i < Playermove.ItemCount.Length; i++)
        {
            Playermove.ItemCount[i] = 0;
        }
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = new Vector3(0f, 1.41f, 0f);
        Player.GetComponent<CharacterController>().enabled = true;
        PlayerObject.Hp = 12;
        Timer.WorldTimeSeconds = 0f;
        ScoreUI.Score = 0;
        ResetEnergy.Energy = 50;
        Bullets.bullets = 24;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EnergyCubes = GameObject.FindGameObjectsWithTag("EnergyCube");
        foreach(GameObject enemy in Enemies)
        {
            Destroy(enemy);
        }
        foreach(GameObject Cubes in EnergyCubes)
        {
            Destroy(Cubes);
        }
        lifter.ResetPosition();
        Time.timeScale = 1.0f;
        DCparam.multiple = 1f;
    }
}
