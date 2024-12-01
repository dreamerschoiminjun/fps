using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainstagebgm : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip mainMenuClip; // 재생할 오디오 클립
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource 컴포넌트를 추가하고 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = mainMenuClip;
        audioSource.loop = true; // 무한 반복 재생 설정
        audioSource.playOnAwake = false; // 씬이 시작되자마자 수동으로 재생하도록 설정
        audioSource.volume = 0.5f; // 사운드 볼륨을 50%로 감소

        // 오디오 재생
        audioSource.Play();
    }
}
