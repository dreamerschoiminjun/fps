using System.Collections;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Transform playerTransform;          // 플레이어의 Transform
    private bool isExploding = false;          // 자폭 중인지 여부
    private AlienStats alienStats;             // AlienStats 컴포넌트를 참조

    void Start()
    {
        // AlienStats 컴포넌트를 가져옵니다.
        alienStats = GetComponent<AlienStats>();

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
        // 플레이어를 향해 이동, Y축 이동 제거
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        playerDirection.y = 0; // Y축 성분 제거

        // 플레이어를 바라보게 설정
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

        // 이동 처리
        transform.Translate(playerDirection * alienStats.moveSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator SelfDestruct()
    {
        isExploding = true;
        Debug.Log("자폭 준비 중...");

        yield return new WaitForSeconds(0.1f); // 0.1초 후 자폭

        // 자폭 로직
        Debug.Log("자폭 발생!");
        Collider[] colliders = Physics.OverlapSphere(transform.position, alienStats.explodeRange);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                // CharacterStats 컴포넌트를 가져와 데미지를 입힘
                CharacterStats playerStats = nearbyObject.GetComponent<CharacterStats>();
                if (playerStats != null)
                {
                    playerStats.CurrentHealth -= alienStats.explodePower; // 폭발 데미지 적용
                    Debug.Log("플레이어에게 폭발 데미지를 입혔습니다!");
                }
            }
        }

        alienStats.TakeDamage(alienStats.maxHealth); // 자폭으로 인해 사망 처리
    }

    // 자폭 범위를 시각적으로 표시
    private void OnDrawGizmosSelected()
    {
        if (alienStats != null)
        {
            Gizmos.color = Color.red; // 자폭 범위를 빨간색으로 표시
            Gizmos.DrawWireSphere(transform.position, alienStats.explodeRange);
        }
    }
}
