using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour
{
    public CharacterStats characterStats; // CharacterStats의 인스턴스를 연결

    private void Start()
    {
        // 시작 시 커서를 숨기고 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // CharacterStats.isDead가 true일 때 커서 보이고 고정 해제
        if (characterStats.isDead)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // 커서 고정 해제
        }
    }
}
