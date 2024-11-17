using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ���� �� ������ �迭
    public float spawnInterval = 3f; // �� ���� ����
    public int maxEnemies = 5; // �ִ� �� ��
    public float spawnRange = 150f; // �÷��̾� �ֺ��� ���� ����

    private int currentEnemyCount = 0; // ���� ������ �� ��
    private GameObject player; // �÷��̾� GameObject�� ���� ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("�÷��̾ �����ϴ�! 'Player' �±װ� �����Ǿ� �ִ��� Ȯ���ϼ���.");
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
        // �÷��̾ �����ϴ��� Ȯ��
        if (player == null || enemyPrefabs.Length == 0) return;

        // ���� ���� ������ ������ x �� z ��ġ ����
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);

        // ������ �� ������ ����
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];

        // �����տ� ���� Y�� ��ġ ����
        float yPosition = randomIndex == 0 ? 4.5f : 20f; // ù ��° �������� Y�� 4.5, �� ��°�� 20���� ����

        // �÷��̾� ��ġ�� �������� �� ���� ��ġ ���
        Vector3 spawnPosition = new Vector3(
            player.transform.position.x + randomX,
            yPosition, // �����տ� ���� Y�� ��ġ
            player.transform.position.z + randomZ
        );

        // �� �������� �ش� ��ġ�� ����
        Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);

        currentEnemyCount++;
    }
}
