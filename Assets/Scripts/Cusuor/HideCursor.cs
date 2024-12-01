using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
    public CharacterStats characterStats; // CharacterStats�� �ν��Ͻ��� ����

    private void Start()
    {
        // ���� �� Ŀ���� ����� ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // CharacterStats.isDead�� true�� �� Ŀ�� ���̰� ���� ����
        if (characterStats.isDead)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Ŀ�� ���� ����
        }
    }
}
