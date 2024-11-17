using System.Collections;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Transform playerTransform;          // �÷��̾��� Transform
    private bool isExploding = false;          // ���� ������ ����
    private AlienStats alienStats;             // AlienStats ������Ʈ�� ����

    void Start()
    {
        // AlienStats ������Ʈ�� �����ɴϴ�.
        alienStats = GetComponent<AlienStats>();

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

            if (distanceToPlayer <= alienStats.explodeRange && !isExploding)
            {
                StartCoroutine(SelfDestruct());
            }
            else if (!isExploding)
            {
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        // �÷��̾ ���� �̵�, Y�� �̵� ����
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        playerDirection.y = 0; // Y�� ���� ����

        // �÷��̾ �ٶ󺸰� ����
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

        // �̵� ó��
        transform.Translate(playerDirection * alienStats.moveSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator SelfDestruct()
    {
        isExploding = true;
        Debug.Log("���� �غ� ��...");

        yield return new WaitForSeconds(0.1f); // 0.1�� �� ����

        // ���� ����
        Debug.Log("���� �߻�!");
        Collider[] colliders = Physics.OverlapSphere(transform.position, alienStats.explodeRange);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                // CharacterStats ������Ʈ�� ������ �������� ����
                CharacterStats playerStats = nearbyObject.GetComponent<CharacterStats>();
                if (playerStats != null)
                {
                    playerStats.CurrentHealth -= alienStats.explodePower; // ���� ������ ����
                    Debug.Log("�÷��̾�� ���� �������� �������ϴ�!");
                }
            }
        }

        alienStats.TakeDamage(alienStats.maxHealth); // �������� ���� ��� ó��
    }

    // ���� ������ �ð������� ǥ��
    private void OnDrawGizmosSelected()
    {
        if (alienStats != null)
        {
            Gizmos.color = Color.red; // ���� ������ ���������� ǥ��
            Gizmos.DrawWireSphere(transform.position, alienStats.explodeRange);
        }
    }
}
