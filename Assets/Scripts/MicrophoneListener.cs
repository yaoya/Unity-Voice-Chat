﻿using UnityEngine;
using System.Collections;
using System;

public class MicrophoneListener : MonoBehaviour
{
    private string selectedDevice = null;
    [SerializeField]
    private int recordingFrequency = 10000;
    private AudioClip audioClip;
	private bool isRecording = false;

    void Awake()
    {
        var options = Microphone.devices;
        if (options.Length == 0)
        {
            throw new InvalidOperationException("There is no recording device detected.");
        }
        else
        {
            this.selectedDevice = options[0];
        }
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        foreach (var device in Microphone.devices)
        {
            if (this.selectedDevice == device)
            {
                GUI.contentColor = Color.cyan;
            }
            else
            {
                GUI.contentColor = Color.white;
            }
            if (GUILayout.Button(device))
            {
                this.selectedDevice = device;
            }
        }
        GUI.contentColor = Color.white;

        GUILayout.Label("Recording frequency:");
        string newFreqString = GUILayout.TextField(recordingFrequency.ToString());
        int.TryParse(newFreqString, out recordingFrequency);

        GUILayout.Space(50);
        if (selectedDevice != null)
        {
            if (GUILayout.Button(isRecording ? "Stop" : "Record"))
            {
				isRecording = !isRecording;
				if (isRecording) {
                	RecordButtonPressed();
				} else {
					StopButtonPressed();
				}
            }
            if (GUILayout.Button("Play"))
            {
				PlaySoundClip();
            }
        }
        GUILayout.EndVertical();
    }

    private void RecordButtonPressed()
    {
        this.audioClip = Microphone.Start(selectedDevice, false, 10, recordingFrequency);
    }

    private void StopButtonPressed()
    {
        Microphone.End(selectedDevice);
    }

	private void PlaySoundClip() 
	{
		this.audio.PlayOneShot(audioClip);
	}
}
