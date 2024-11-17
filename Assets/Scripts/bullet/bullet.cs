using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonArsenal
{
    public class Bullet : MonoBehaviour
    {
        public float destroyTime = 5f;  // �Ѿ��� �ڵ����� ���ŵǴ� �ð�

        private Rigidbody rb;
        private CharacterStats characterStats;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            // ĳ���� ��ü�� ã�Ƽ� CharacterStats ������Ʈ ��������
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                characterStats = player.GetComponent<CharacterStats>();
            }
        }

        void FixedUpdate()
        {
            // Destroy Timer�� ���� �Ѿ� ����
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0f)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // �浹�� ��ü�� ���� ��� ������ ����
            if (other.CompareTag("enemy"))
            {
                if (characterStats != null)
                {
                    int damage = Mathf.RoundToInt(characterStats.rangedAttackPower); // CharacterStats�� rangedAttackPower�� �������� ���

                    // EnemyStats �Ǵ� AlienStats�� �������� ����
                    EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                    AlienStats alienStats = other.GetComponent<AlienStats>();

                    if (enemyStats != null)
                    {
                        enemyStats.TakeDamage(damage);
                        Debug.Log($"������ {damage} ������ ���� (EnemyStats).");
                    }
                    else if (alienStats != null)
                    {
                        alienStats.TakeDamage(damage);
                        Debug.Log($"������ {damage} ������ ���� (AlienStats).");
                    }
                }
            }

            // �Ѿ� �ı�
            Destroy(gameObject);
        }

    }
}
