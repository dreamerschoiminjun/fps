using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소를 사용하기 위해 추가
using TMPro; // TextMeshPro 사용을 위해 추가

public class CharacterStats : MonoBehaviour
{
    // 체력관련
    public float maxHealth = 200; // 기본 최대 체력
    public float CurrentHealth; // 현재 체력

    // UI 관련
    private Scrollbar healthScrollbar; // 체력 스크롤바
    private TextMeshProUGUI healthText; // TextMeshProUGUI를 사용하여 체력 수치를 표시

    // 원거리 공격 관련
    public float rangedAttackPower = 10; // 원거리 공격력
    public float bulletspeed = 1000f; // 발사체 속도
    public float bulletfireRate = 0.3f; // 발사 속도
    public float fireDelay; // 발사 지연 시간

    // 이동관련
    public float MoveSpeed = 2.0f; // 기본 이동 속도
    public float SprintSpeed = 5f; // 기본 달리기 속도
    public float JumpHeight = 1.2f; // 점프높이
    public float jumppower = 10; // 점프세기

    [Header("인벤토리")]
    public List<GameItem> inventoryItems = new List<GameItem>();

    private float healthRegenTimer = 0f; // 체력 회복 타이머

    private void Start()
    {
        // 체력 초기화
        CurrentHealth = maxHealth;

        // Canvas 안에 있는 Scrollbar와 TextMeshProUGUI를 자동으로 찾음
        healthScrollbar = GameObject.Find("Canvas").GetComponentInChildren<Scrollbar>();
        healthText = GameObject.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>();

        if (healthScrollbar == null)
        {
            Debug.LogError("Canvas에 Scrollbar를 찾을 수 없습니다. Scrollbar가 제대로 설정되어 있는지 확인하세요.");
        }

        if (healthText == null)
        {
            Debug.LogError("Canvas에 TextMeshProUGUI를 찾을 수 없습니다. TextMeshProUGUI가 제대로 설정되어 있는지 확인하세요.");
        }

        UpdateHealthUI();
    }

    private void Update()
    {
        // 10초마다 체력 10씩 회복
        healthRegenTimer += Time.deltaTime;
        if (healthRegenTimer >= 10f)
        {
            CurrentHealth += 10f;
            if (CurrentHealth > maxHealth)
            {
                CurrentHealth = maxHealth; // 최대 체력을 초과하지 않도록 고정
            }
            healthRegenTimer = 0f;
            Debug.Log($"체력 회복: 현재 체력은 {CurrentHealth}입니다.");
        }

        // 체력 UI 업데이트
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        // 체력 비율로 스크롤바 값 설정
        if (healthScrollbar != null)
        {
            healthScrollbar.size = CurrentHealth / maxHealth;
        }

        // 체력 수치를 텍스트로 업데이트
        if (healthText != null)
        {
            healthText.text = $"{CurrentHealth} / {maxHealth}";
        }
    }

    public void AddToInventory(GameItem newItem)
    {
        // 새로운 아이템을 인벤토리에 추가합니다.
        inventoryItems.Add(newItem);
        Debug.Log($"아이템 {newItem.id} 추가 완료");
    }
}
