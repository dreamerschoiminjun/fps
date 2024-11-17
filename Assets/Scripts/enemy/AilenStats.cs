using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStats : MonoBehaviour
{
    // �⺻ ü��, �ӵ�, ���ݷ�
    [SerializeField]
    public float maxHealth = 100f;                 // �ִ� ü��
    public float currentHealth;                     // ���� ü��
    public float explodePower = 50f;                 // ���� ���ݷ�
    public float explodeRange = 1f;                 // �÷��̾���� ���� ����
    public float moveSpeed = 6.0f;                  // �̵� �ӵ�
    public float scorePerKill = 50;                  // ���� ����� �� �߰� ����

    public Score scoreManager;                     // Score ������Ʈ�� �����ϱ� ���� ����

    void Start()
    {
        currentHealth = maxHealth;

        // Score ������Ʈ�� ã�� Score ������Ʈ�� �����ɴϴ�
        GameObject scoreObject = GameObject.Find("Score");
        if (scoreObject != null)
        {
            scoreManager = scoreObject.GetComponent<Score>();
            if (scoreManager == null)
            {
                Debug.LogWarning("Score ������Ʈ�� ã�� �� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogWarning("Score ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    void Update()
    {
        // ���� ��ȭ ����
        if (scoreManager != null)
        {
            scoreManager.ApplyStatIncreases(this);
        }
    }

    // �������� �޴� �Լ�
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ���� ������� �� ó��
    void Die()
    {
        Debug.Log("�� ���");

        if (scoreManager != null)
        {
            // ���� ����� �� �߰��� ������ ������Ŵ
            scoreManager.AddKillScore(scorePerKill);
        }
        // ���ο� ���� ��ġ ����
        float randomX = Random.Range(-250f, 250f);
        float randomZ = Random.Range(-250f, 250f);
        Vector3 newPosition = new Vector3(randomX, 5, randomZ);

        // ��ġ�� �̵�
        transform.position = newPosition;
    }
}
