using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainstagebgm : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip mainMenuClip; // ����� ����� Ŭ��
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource ������Ʈ�� �߰��ϰ� ����
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = mainMenuClip;
        audioSource.loop = true; // ���� �ݺ� ��� ����
        audioSource.playOnAwake = false; // ���� ���۵��ڸ��� �������� ����ϵ��� ����
        audioSource.volume = 0.5f; // ���� ������ 50%�� ����

        // ����� ���
        audioSource.Play();
    }
}
