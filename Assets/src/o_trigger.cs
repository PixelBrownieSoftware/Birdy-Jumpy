using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class s_event {

    public enum EVENT_TYPE {
        END = -1,
        LABEL = 0,
        DIALOGUE,
        CAMERA_POSITION,
        PLAY_DIRECTOR,
        ENABLE_PLAYER,
        DISABLE_PLAYER,
        FADE,
        CHANGE_MAP,
        CHECK_FLAG_EQUAL,
        CHECK_FLAG_GREATER,
        CHECK_FLAG_LESS,
        CHECK_FLAG_NOT_EQUAL,
        SET_FLAG
    }
    public EVENT_TYPE event_type;
    public PlayableDirector director;
    public string dialogue;
    public Vector3 position;
    public Vector3 direction;
    public Color colour;
}

public class o_trigger : MonoBehaviour {
    public s_event[] events;
    public bool isPlay = false;

    private void OnTriggerStay(Collider other)
    {
        if (!isPlay)
        {
            if (other.GetComponent<o_player>())
            {
                isPlay = true;
                s_cutscenehandler ct = s_cutscenehandler.GetInstance();
                ct.StartCoroutine(ct.StartCutscene(this));
            }
        }
    }
}
