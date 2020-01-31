using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZ : MonoBehaviour
{
    public void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.GetComponent<Movement>().Reset();
    }
}
