using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class s_cutscenehandler : s_singleton<s_cutscenehandler>
{
    public Image fade;
    public Text text;
    public o_trigger currentTrigger;

    public IEnumerator StartCutscene(o_trigger tr) {
        currentTrigger = tr;
        foreach (s_event ev in currentTrigger.events)
        {
            float t = 0;
            Color col = fade.color;
            switch (ev.event_type) {
                case s_event.EVENT_TYPE.DIALOGUE:
                    text.text = ev.dialogue;
                    break;

                case s_event.EVENT_TYPE.DISABLE_PLAYER:
                    s_globals.GetInstance().currentPlayer.isControl = false;
                    s_globals.GetInstance().currentPlayer.movedirFin = new Vector3(0, 
                        s_globals.GetInstance().currentPlayer.movedirFin.y, 0);
                    break;

                case s_event.EVENT_TYPE.ENABLE_PLAYER:
                    s_globals.GetInstance().currentPlayer.isControl = true;
                    break;

                case s_event.EVENT_TYPE.PLAY_DIRECTOR:
                    ev.director.Play();
                    while(ev.director.state != UnityEngine.Playables.PlayState.Paused)
                        yield return new WaitForSeconds(Time.deltaTime);
                    break;

                case s_event.EVENT_TYPE.FADE:
                    while (fade.color != ev.colour) {
                        fade.color = Color.Lerp(col, ev.colour, t);
                        t += Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    } 
                    break;

                case s_event.EVENT_TYPE.CHANGE_MAP:
                    while (fade.color != Color.black)
                    {
                        fade.color = Color.Lerp(col, Color.black, t);
                        t += Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    s_globals.GetInstance().LoadScene(ev.dialogue);
                    while (fade.color != Color.clear)
                    {
                        fade.color = Color.Lerp(col, Color.clear, t);
                        t += Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    break;

                case s_event.EVENT_TYPE.END:
                    yield return null;
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
