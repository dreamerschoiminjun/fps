using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menubgm : MonoBehaviour
{
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

        // ����� ���
        audioSource.Play();
    }
}
