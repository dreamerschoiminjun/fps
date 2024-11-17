using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonArsenal
{
    public class Bullet : MonoBehaviour
    {
        public float destroyTime = 5f;  // 총알이 자동으로 제거되는 시간

        private Rigidbody rb;
        private CharacterStats characterStats;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            // 캐릭터 객체를 찾아서 CharacterStats 컴포넌트 가져오기
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                characterStats = player.GetComponent<CharacterStats>();
            }
        }

        void FixedUpdate()
        {
            // Destroy Timer에 따른 총알 제거
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0f)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // 충돌한 물체가 적일 경우 데미지 적용
            if (other.CompareTag("enemy"))
            {
                if (characterStats != null)
                {
                    int damage = Mathf.RoundToInt(characterStats.rangedAttackPower); // CharacterStats의 rangedAttackPower를 데미지로 사용

                    // EnemyStats 또는 AlienStats에 데미지를 적용
                    EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                    AlienStats alienStats = other.GetComponent<AlienStats>();

                    if (enemyStats != null)
                    {
                        enemyStats.TakeDamage(damage);
                        Debug.Log($"적에게 {damage} 데미지 적용 (EnemyStats).");
                    }
                    else if (alienStats != null)
                    {
                        alienStats.TakeDamage(damage);
                        Debug.Log($"적에게 {damage} 데미지 적용 (AlienStats).");
                    }
                }
            }

            // 총알 파괴
            Destroy(gameObject);
        }

    }
}
