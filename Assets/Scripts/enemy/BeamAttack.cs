using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    [Header("References")]
    public GameObject beamStart; // �� ���� ����
    public GameObject beamEnd; // �� �� ����
    public GameObject beam; // �� ��ü (������ LineRenderer�� �߰��Ǿ� ����)

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; // ������ �� ��ġ ����
    private List<Vector3> playerPositions; // �÷��̾� ��ġ ������ ����Ʈ
    private GameObject player; // �÷��̾� ������Ʈ
    private float positionDelay = 0.2f; // 0.2�� �� ��ġ�� ����
    private int frameBuffer; // �����Ӹ��� ����� ����
    private bool isFiring = true; // �ڵ� ���� ����
    private float damageInterval = 0.1f; // ������ �ִ� ����
    private float attackDuration = 2f; // ���� ���� �ð�
    private float attackCooldown = 0.5f; // ���� ��Ÿ��
    private float damageTimer = 0f; // ������ Ÿ�̸�
    private bool isDamageActive = false; // ������ Ȱ��ȭ ����

    // �ʱ�ȭ
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerPositions = new List<Vector3>();

        // 0.2�� ������ ���� ���� ��� (�뷫 5������ �� ��ġ�� ����)
        frameBuffer = Mathf.CeilToInt(positionDelay / Time.fixedDeltaTime);
    }

    // �� �����Ӹ��� �÷��̾� ��ġ�� ����
    void FixedUpdate()
    {
        if (player != null)
        {
            // ���� �÷��̾� ��ġ�� ����Ʈ�� �߰�
            playerPositions.Add(player.transform.position);

            // ���� ũ�⸦ �Ѿ�� ���� ������ ��ġ ����
            if (playerPositions.Count > frameBuffer)
            {
                playerPositions.RemoveAt(0);
            }
        }
    }

    // �� �����Ӹ��� ����
    void Update()
    {
        if (isFiring && playerPositions.Count >= frameBuffer)
        {
            // 0.2�� �� �÷��̾� ��ġ�� �� �߻�
            Vector3 delayedPosition = playerPositions[0]; // 0.2�� �� ��ġ (���� ������ ��)
            Vector3 direction = delayedPosition - transform.position;
            ShootBeamInDir(transform.position, direction);

            // ������ ���������� ������ ������
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Player")) // �÷��̾ �±װ� "Player"�� ���
                {
                    if (isDamageActive)
                    {
                        damageTimer += Time.deltaTime;
                        if (damageTimer >= damageInterval)
                        {
                            TakeDamage(hit.collider.gameObject); // �÷��̾�� ������ ������
                            damageTimer = 0f; // Ÿ�̸� �ʱ�ȭ
                        }
                    }
                }
            }
        }

        // �� ���� ���� �߰�
        if (beam != null)
        {
            Destroy(beamStart, attackDuration);
            Destroy(beamEnd, attackDuration);
            Destroy(beam, attackDuration);
            isFiring = false; // ���� ���¸� ��Ȱ��ȭ
            Invoke("StartAttackCooldown", attackCooldown); // ��Ÿ�� �� �����
        }
    }

    // �������� �÷��̾�� ������ �Լ�
    private void TakeDamage(GameObject player)
    {
        CharacterStats characterStats = player.GetComponent<CharacterStats>(); // CharacterStats�� ����
        EnemyStats enemyStats = GetComponent<EnemyStats>(); // EnemyStats ����
        if (characterStats != null)
        {
            characterStats.CurrentHealth -= enemyStats.attackPower; // CharacterStats�� CurrentHealth�� ����
        }
    }

    // ���� Ư�� �������� �߻��ϴ� �Լ�
    void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        LineRenderer line = beam.GetComponent<LineRenderer>(); // ������ LineRenderer ��������
        line.positionCount = 2;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
            end = hit.point - (dir.normalized * beamEndOffset);
        else
            end = start + (dir * 100);

        beamEnd.transform.position = end;
        line.SetPosition(1, end);
        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);
    }

    // ���� ��Ÿ�� �� ���� �����
    private void StartAttackCooldown()
    {
        isFiring = true; // ���� ���¸� Ȱ��ȭ
        isDamageActive = true; // ������ Ȱ��ȭ
        damageTimer = 0f; // Ÿ�̸� �ʱ�ȭ
    }
}
