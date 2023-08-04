
using System;
using System.Collections;
using PlayerScripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject topTurel;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxAimVertical;
    [SerializeField] private float minAimVertical;
    [SerializeField] private GameObject aim;
    [SerializeField] private Player player;
    [SerializeField] private AudioSource audioSource;


    private void Start()
    {
        audioSource.Play();
    }

    internal bool pauseGame;
    void Update()
    {
        if (!player.playerLosing && !pauseGame)
        {
            Vector3 currentEulerAngles = new Vector3(topTurel.transform.eulerAngles.x,
                topTurel.transform.eulerAngles.y +
                Input.GetAxis("Mouse X") * rotationSpeed, topTurel.transform.eulerAngles.z);
            topTurel.transform.eulerAngles = currentEulerAngles;

            Vector3 aimPos = aim.transform.localPosition;
            aim.transform.localPosition = new Vector3(aimPos.x, aimPos.y, Mathf.Clamp(aimPos.z +
                Input.GetAxis("Mouse Y") *
                100, minAimVertical, maxAimVertical));
        }
        
        
        float diff = Input.GetAxis("Mouse X") * rotationSpeed;
        
        PlayRotationSound(diff);
    }

    private void PlayRotationSound(float diff)
    {
        if (diff < 0) diff *= -1;
        
        audioSource.volume = Mathf.Lerp(0.01f, 0.5f, diff);
    }
    
    private void StopRotationSound()
    {
        audioSource.volume = 0;
    }
}
