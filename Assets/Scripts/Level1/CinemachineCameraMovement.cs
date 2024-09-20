using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCameraMovement : MonoBehaviour
{
    public static CinemachineCameraMovement Instance; 
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float timeMovement, timeTotalMovement, initialIntencity;

    private void Awake(){
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void MoveCamera(float intensity, float frecuency, float time){
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frecuency;
        initialIntencity = intensity;
        timeTotalMovement = time;
        timeMovement = time;
    }

    private void Update(){
        if(timeMovement > 0){
            timeMovement -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(initialIntencity, 0, 1 - (timeMovement / timeTotalMovement));
        }
    }
}
