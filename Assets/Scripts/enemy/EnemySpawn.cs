using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 여러 적 프리팹 배열
    public float spawnInterval = 3f; // 적 생성 간격
    public int maxEnemies = 5; // 최대 적 수
    public float spawnRange = 150f; // 플레이어 주변의 생성 범위

    private int currentEnemyCount = 0; // 현재 생성된 적 수
    private GameObject player; // 플레이어 GameObject에 대한 참조

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("플레이어가 없습니다! 'Player' 태그가 설정되어 있는지 확인하세요.");
            return;
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // 플레이어가 존재하는지 확인
        if (player == null || enemyPrefabs.Length == 0) return;

        // 생성 범위 내에서 랜덤한 x 및 z 위치 설정
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);

        // 랜덤한 적 프리팹 선택
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];

        // 프리팹에 따라 Y축 위치 설정
        float yPosition = randomIndex == 0 ? 4.5f : 20f; // 첫 번째 프리팹은 Y축 4.5, 두 번째는 20으로 설정

        // 플레이어 위치를 기준으로 적 생성 위치 계산
        Vector3 spawnPosition = new Vector3(
            player.transform.position.x + randomX,
            yPosition, // 프리팹에 따른 Y축 위치
            player.transform.position.z + randomZ
        );

        // 적 프리팹을 해당 위치에 생성
        Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);

        currentEnemyCount++;
    }
}
