using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_ending : MonoBehaviour
{
    void Start()
    {
        Destroy(s_globals.GetInstance().currentPlayer);
    }
    
}
