using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // �⺻ ü��, �ӵ�, ���ݷ�
    [SerializeField]
    public float maxHealth = 100f;                 // �ִ� ü��
    public float currentHealth;                     // ���� ü��
    public float attackPower = 10f;                 // ���ݷ�
    public float attackRange = 25f;                 // �÷��̾���� ���� ����
    public float moveSpeed = 2.0f;                  // �̵� �ӵ�
    public float scorePerKill = 100;                // ���� ����� �� �߰� ����

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
        Vector3 newPosition = new Vector3(randomX, 20, randomZ);

        // ��ġ�� �̵�
        transform.position = newPosition;
    }
}
