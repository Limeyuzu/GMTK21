using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float Shake_Timer;
    private float Shake_Timer_Total;
    private float Start_Intensity;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake_Camera(float Intensity, float Time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Intensity;
        Shake_Timer = Time;
        Shake_Timer_Total = Time;
        Start_Intensity = Intensity;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Shake_Timer > 0.0f)
        {
            Shake_Timer -= Time.deltaTime;

            if (Shake_Timer <= 0.0f)
            {
                // Timer Over
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(Start_Intensity, 0.0f, 1 - (Shake_Timer / Shake_Timer_Total));
            }
        }
    }
}
