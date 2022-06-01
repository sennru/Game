using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class class_instance : MonoBehaviour
{
    Class_test class_test;
    public int time = 60;
    public void Start()
    {

    }

    public void Update()
    {
        class_test = new Class_test();
        class_test.GetText(class_test.Timer(time));
    }

}
