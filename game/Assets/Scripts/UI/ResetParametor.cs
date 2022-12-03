using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetParametor : MonoBehaviour
{
    PlayerUnit PlayerObject;
    WorldTime Timer;
    ScoreManager ScoreUI;
    EnergyManager ResetEnergy;
    GunController Bullets;
    GameObject[] Enemies, EnergyCubes;
    public void Start()
    {
        PlayerObject = GameObject.Find("Player").GetComponent<PlayerUnit>();
        Timer = GameObject.Find("WorldTimer").GetComponent<WorldTime>();
        ScoreUI = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        ResetEnergy = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        Bullets = GameObject.Find("Shooting").GetComponent<GunController>();
    }
    public void ResetParam()
    {
        PlayerObject.Hp = 12;
        Timer.WorldTimeSeconds = 0f;
        ScoreUI.Score = 0;
        ResetEnergy.Energy = 0;
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
    }
}
