using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugOnly : MonoBehaviour
{
    EnergyManager energyManager;
    public GameObject En;
    public Text scoreNumber;
    int score;
    [SerializeField] bool IsCheat;
    // Start is called before the first frame update
    void Start()
    {
        energyManager = En.GetComponent<EnergyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCheat == true)
        {
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
            {
                Time.timeScale = 10f;
            }
            else if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.CapsLock))
            {
                Time.timeScale = 0.3f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
            if (Input.GetKeyDown("q"))
            {
                energyManager.Energy += 10000;
            }
            if (Input.GetKeyDown("v"))
            {
                score = int.Parse(scoreNumber.text);
                score += 1000;
                scoreNumber.text = score.ToString();
            }

        }
    }
}
