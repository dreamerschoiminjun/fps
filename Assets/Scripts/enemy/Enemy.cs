using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;  // ������ ��� (�Ϲ������� �÷��̾�)
    private float lastAttackTime;
    private bool isAttacking = false; // ���� ������ ����
    public Beam beam; // Beam ��ũ��Ʈ ���� �߰�
    private Rigidbody rb; // Rigidbody ���� �߰�
    public EnemyStats enemyStats; // EnemyStats�� �ν��Ͻ��� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Player �±׸� ���� ������Ʈ�� ã�� playerTransform�� ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�! 'Player' �±װ� �����Ǿ� �ִ��� Ȯ���ϼ���.");
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
                // ���� ���� ���� �������� �ʵ��� ����
                StopMovement();
            }
            else
            {
                if (isAttacking)
                {
                    StopAttacking();
                }
                // ���� ���� �ۿ� ���� ���� ����
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        // �÷��̾ ���� �̵�, Y�� �̵� ����
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        playerDirection.y = 0;  // Y�� ���� ����

        transform.Translate(playerDirection * enemyStats.moveSpeed * Time.deltaTime, Space.World);
    }

    private void StopMovement()
    {
        // �߰������� �̵��� ���ߵ��� �ʿ��� ������ ���� ��� �̰��� �ۼ�
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; // ���ӵ��� �ʱ�ȭ
        }
    }

    private void StartAttacking()
    {
        isAttacking = true;
        beam.ActivateBeam(); // �� Ȱ��ȭ
    }

    private void StopAttacking()
    {
        isAttacking = false;
        beam.DeactivateBeam(); // �� ��Ȱ��ȭ
    }

    void OnCollisionEnter(Collision collision)
    {
        // Bullet �±׿� �浹�� ���
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // ������ ���� (�ʿ��� ���� �߰�)
            TakeDamageFromBullet();

            // �浹 �� ������ �� ����
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ
                rb.angularVelocity = Vector3.zero; // ���ӵ� �ʱ�ȭ
            }
        }
    }

    private void TakeDamageFromBullet()
    {
        // �������� �޴� ���� ���� (��: ü�� ����)
        Debug.Log("�Ѿ˿� �¾� �������� �޾ҽ��ϴ�.");
    }

    // ���� ������ �ð������� ǥ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // ���� ������ ���������� ����
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackRange);  // ���� ���� ǥ��
    }
}
