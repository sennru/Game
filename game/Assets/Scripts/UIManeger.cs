using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour
{
    public GameObject get;
    public GameObject timer;
    private int i = 0;
    int time;
    bool OnResultScreen = false;
    [SerializeField] GameObject Startmenu;
    [SerializeField] GameObject Option;
    [SerializeField] GameObject SelectMenu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject pause_select;
    [SerializeField] GameObject result;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (i == 0)
            {
                ToStartmenu();
            }
            else if (i == 1)
            {
                pause();
            }
        }
        if (get == GameObject.Find("result"))
        {
            i = 2;
        }

        string remain = timer.GetComponent<Text>().text;
        time = int.Parse(remain);
        if (time == 0)
        {
            Toresult();
        }
    }

    public void ToOption()
    {
        i = 0;
        get.SetActive(false);
        Option.SetActive(true);
        get = GameObject.Find("Option");
        if(i == 0)
        {
            pause_select.SetActive(false);
        }
    }
    public void ToSelectMenu()
    {
        i = 0;
        get.SetActive(false);
        SelectMenu.SetActive(true);
        get = GameObject.Find("SelectMenu");
        if (game.activeSelf == true)
        {
            game.SetActive(false);
        }
        if (OnResultScreen)
        {
            result.SetActive(false);
            OnResultScreen = false;
        }
    }
    public void ToStartmenu()
    {
        i = 0;
        get.SetActive(false);
        Startmenu.SetActive(true);
        get = GameObject.Find("Startmenu");
        if (OnResultScreen)
        {
            result.SetActive(false);
            OnResultScreen = false;
        }
    }

    public void Togame()
    {
        get.SetActive(false);
        game.SetActive(true);
        get = GameObject.Find("game");
        i = 1;
    }

    // Update is called once per frame
    public void pause()
    {
        if(get == GameObject.Find("game"))
        {
            Option.SetActive(true);
            get = GameObject.Find("Option");
        }
        else if (get == GameObject.Find("Option"))
        {
            Option.SetActive(false);
            get = GameObject.Find("game");
        }
    }

    public void Toresult()
    {
        OnResultScreen = true;
        game.SetActive(false);
        result.SetActive(OnResultScreen);
        get = GameObject.Find("result");
    }

}
