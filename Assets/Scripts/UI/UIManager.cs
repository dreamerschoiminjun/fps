using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CharacterStats characterStats; // ĳ���� ���� ��ũ��Ʈ ����
    public Score scoreManager;            // Score ��ũ��Ʈ ����

    [Header("UI Elements")]
    public Slider healthBar;              // ü�¹�
    public Text scoreText;                // ���� ǥ�� �ؽ�Ʈ
    public Text itemDescriptionText;      // ������ ���� �ؽ�Ʈ
    public Image crosshairImage;          // ���� �̹���

    private void Start()
    {
        UpdateHealthBar();
        UpdateScoreUI();
        UpdateItemDescriptionUI("");
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateScoreUI(); // �� �����Ӹ��� ���� UI ������Ʈ
    }

    // ü�¹� ������Ʈ
    private void UpdateHealthBar()
    {
        healthBar.value = characterStats.CurrentHealth / characterStats.maxHealth;
    }

    // ���� UI ������Ʈ
    private void UpdateScoreUI()
    {
        // Score ��ũ��Ʈ�� point ���� �����ͼ� UI�� �ݿ�
        scoreText.text = "Score: " + scoreManager.point.ToString("F0");
    }

    // ������ ���� UI ������Ʈ
    public void UpdateItemDescriptionUI(string description)
    {
        itemDescriptionText.text = "Item: " + description;
    }

    
}
