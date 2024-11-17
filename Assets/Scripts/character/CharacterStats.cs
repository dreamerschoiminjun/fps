using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ��Ҹ� ����ϱ� ���� �߰�
using TMPro; // TextMeshPro ����� ���� �߰�

public class CharacterStats : MonoBehaviour
{
    // ü�°���
    public float maxHealth = 200; // �⺻ �ִ� ü��
    public float CurrentHealth; // ���� ü��

    // UI ����
    private Scrollbar healthScrollbar; // ü�� ��ũ�ѹ�
    private TextMeshProUGUI healthText; // TextMeshProUGUI�� ����Ͽ� ü�� ��ġ�� ǥ��

    // ���Ÿ� ���� ����
    public float rangedAttackPower = 10; // ���Ÿ� ���ݷ�
    public float bulletspeed = 1000f; // �߻�ü �ӵ�
    public float bulletfireRate = 0.3f; // �߻� �ӵ�
    public float fireDelay; // �߻� ���� �ð�

    // �̵�����
    public float MoveSpeed = 2.0f; // �⺻ �̵� �ӵ�
    public float SprintSpeed = 5f; // �⺻ �޸��� �ӵ�
    public float JumpHeight = 1.2f; // ��������
    public float jumppower = 10; // ��������

    [Header("�κ��丮")]
    public List<GameItem> inventoryItems = new List<GameItem>();

    private float healthRegenTimer = 0f; // ü�� ȸ�� Ÿ�̸�

    private void Start()
    {
        // ü�� �ʱ�ȭ
        CurrentHealth = maxHealth;

        // Canvas �ȿ� �ִ� Scrollbar�� TextMeshProUGUI�� �ڵ����� ã��
        healthScrollbar = GameObject.Find("Canvas").GetComponentInChildren<Scrollbar>();
        healthText = GameObject.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>();

        if (healthScrollbar == null)
        {
            Debug.LogError("Canvas�� Scrollbar�� ã�� �� �����ϴ�. Scrollbar�� ����� �����Ǿ� �ִ��� Ȯ���ϼ���.");
        }

        if (healthText == null)
        {
            Debug.LogError("Canvas�� TextMeshProUGUI�� ã�� �� �����ϴ�. TextMeshProUGUI�� ����� �����Ǿ� �ִ��� Ȯ���ϼ���.");
        }

        UpdateHealthUI();
    }

    private void Update()
    {
        // 10�ʸ��� ü�� 10�� ȸ��
        healthRegenTimer += Time.deltaTime;
        if (healthRegenTimer >= 10f)
        {
            CurrentHealth += 10f;
            if (CurrentHealth > maxHealth)
            {
                CurrentHealth = maxHealth; // �ִ� ü���� �ʰ����� �ʵ��� ����
            }
            healthRegenTimer = 0f;
            Debug.Log($"ü�� ȸ��: ���� ü���� {CurrentHealth}�Դϴ�.");
        }

        // ü�� UI ������Ʈ
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        // ü�� ������ ��ũ�ѹ� �� ����
        if (healthScrollbar != null)
        {
            healthScrollbar.size = CurrentHealth / maxHealth;
        }

        // ü�� ��ġ�� �ؽ�Ʈ�� ������Ʈ
        if (healthText != null)
        {
            healthText.text = $"{CurrentHealth} / {maxHealth}";
        }
    }

    public void AddToInventory(GameItem newItem)
    {
        // ���ο� �������� �κ��丮�� �߰��մϴ�.
        inventoryItems.Add(newItem);
        Debug.Log($"������ {newItem.id} �߰� �Ϸ�");
    }
}
