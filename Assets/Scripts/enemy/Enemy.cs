using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;  // 추적할 대상 (일반적으로 플레이어)
    private float lastAttackTime;
    private bool isAttacking = false; // 공격 중인지 여부
    public Beam beam; // Beam 스크립트 참조 추가
    private Rigidbody rb; // Rigidbody 변수 추가
    public EnemyStats enemyStats; // EnemyStats의 인스턴스를 참조

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Player 태그를 가진 오브젝트를 찾아 playerTransform에 설정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("플레이어를 찾을 수 없습니다! 'Player' 태그가 설정되어 있는지 확인하세요.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= enemyStats.attackRange)
            {
                if (!isAttacking)
                {
                    StartAttacking();
                }
                // 공격 중일 때는 움직이지 않도록 설정
                StopMovement();
            }
            else
            {
                if (isAttacking)
                {
                    StopAttacking();
                }
                // 공격 범위 밖에 있을 때는 추적
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        // 플레이어를 향해 이동, Y축 이동 제거
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        playerDirection.y = 0;  // Y축 성분 제거

        transform.Translate(playerDirection * enemyStats.moveSpeed * Time.deltaTime, Space.World);
    }

    private void StopMovement()
    {
        // 추가적으로 이동을 멈추도록 필요한 로직이 있을 경우 이곳에 작성
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; // 각속도도 초기화
        }
    }

    private void StartAttacking()
    {
        isAttacking = true;
        beam.ActivateBeam(); // 빔 활성화
    }

    private void StopAttacking()
    {
        isAttacking = false;
        beam.DeactivateBeam(); // 빔 비활성화
    }

    void OnCollisionEnter(Collision collision)
    {
        // Bullet 태그와 충돌할 경우
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 데미지 적용 (필요한 로직 추가)
            TakeDamageFromBullet();

            // 충돌 시 물리적 힘 무시
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // 속도 초기화
                rb.angularVelocity = Vector3.zero; // 각속도 초기화
            }
        }
    }

    private void TakeDamageFromBullet()
    {
        // 데미지를 받는 로직 구현 (예: 체력 감소)
        Debug.Log("총알에 맞아 데미지를 받았습니다.");
    }

    // 공격 범위를 시각적으로 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // 공격 범위를 빨간색으로 설정
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackRange);  // 공격 범위 표시
    }
}
