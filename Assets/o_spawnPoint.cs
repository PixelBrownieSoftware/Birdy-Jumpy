using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o_spawnPoint : MonoBehaviour
{
    private void Start()
    {
        s_globals.GetInstance().currentPlayer.transform.position = transform.position;
        s_globals.GetInstance().spawnPoint = transform.position;
        print("OK");
    }
}
