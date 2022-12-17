using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public GameObject WarpPoints;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var pos = WarpPoints.transform.position;
            pos.y += WarpPoints.transform.localScale.y / 2f + 5f;
            collision.gameObject.GetComponent<CharacterController>().enabled = false;
            collision.gameObject.transform.position = pos;
            collision.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
