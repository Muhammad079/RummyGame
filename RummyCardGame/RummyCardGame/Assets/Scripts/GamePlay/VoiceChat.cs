using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceChat : MonoBehaviourPun,IPunObservable
{
    private Recorder rec;
    PhotonView view;
    float Mic_Volume;
    bool coroutine_check;
    private void Start()
    {
        view = photonView;
        rec = GetComponent<Recorder>();
    }
    IEnumerator Waiting()
    {
        coroutine_check = true;
        yield return new WaitForSeconds(2);
        StopCoroutine(Waiting());
        coroutine_check = false;
    }
    private void FixedUpdate ()
    {
        Mic_Volume = rec.LevelMeter.CurrentAvgAmp;
        object[] Mic_Volume_sent = new object[] { Mic_Volume };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers=ReceiverGroup.All};
        PhotonNetwork.RaiseEvent(MultiPlayerEventsCallBack.Mic_Volume_notifier,Mic_Volume_sent,raiseEventOptions,SendOptions.SendReliable);

        if(rec.TransmitEnabled)
        {
            if (Mic_Volume <= 0.005 && !coroutine_check)
            {

                for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
                {
                    if (i <= 0)
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(true);
                    }
                    else
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(false);
                    }
                }
                StartCoroutine(Waiting());
            }
            else if (Mic_Volume <= 0.010
                && Mic_Volume > 0.005
                && !coroutine_check)
            {
                for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
                {
                    if (i <= 1)
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(true);
                    }
                    else
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(false);
                    }

                }
                StartCoroutine(Waiting());
            }
            else if (Mic_Volume <= 0.015
                && Mic_Volume > 0.010
                && !coroutine_check)
            {
                for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
                {
                    if (i <= 2)
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(true);
                    }
                    else
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(false);
                    }

                }
                StartCoroutine(Waiting());
            }
            else if (Mic_Volume <= 0.020
                && Mic_Volume > 0.015
                && !coroutine_check)
            {
                for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
                {
                    if (i <= 3)
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(true);
                    }
                    else
                    {
                        Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(false);
                    }

                }
                StartCoroutine(Waiting());
            }
            else if (Mic_Volume > 0.020
                && !coroutine_check)
            {
                for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
                {
                    Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(true);
                }
                StartCoroutine(Waiting());
            }
        }
        else
        {
            for (int i = 0; i < Gameplay_Handler.instance.Voice_Highlighter.Length; i++)
            {
                Gameplay_Handler.instance.Voice_Highlighter[i].SetActive(false);
            }
        }
        
    }
    public void StartTransmission()
    {
        if(view.IsMine)
        rec.TransmitEnabled = true;
    }
    public void StopTransmission()
    {
        if (view.IsMine)
            rec.TransmitEnabled = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
