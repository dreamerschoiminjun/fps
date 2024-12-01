using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCrash : MonoBehaviour
{
    private float time = 0f;
    public float limit_time = 5f; // ����� ���� ����
    public GameObject[] planePrefabs; // ���� ����� ������ �迭
    public float spawnRange = 150f; // ���� ����
    public float gravity = 9.8f; // Y������ �������� �߷� ��
    public float destroy_time = 10f; // �ı� �ð�
    public float y_limit = 0f; // �ı� ���� (0 ���Ϸ� ����)
    public float fixedHeight = 150f; // ������ ����
    public float safeRadius = 75f; // �÷��̾� �ֺ� ���� �ݰ�

    private GameObject player; // �÷��̾� GameObject�� ���� ����

    void Start()
    {
        // �÷��̾� ��ü ã��
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("�÷��̾ �����ϴ�! 'Player' �±װ� �����Ǿ� �ִ��� Ȯ���ϼ���.");
            return;
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > limit_time)
        {
            SpawnPlane();
            time = 0f;
        }
    }

    void SpawnPlane()
    {
        // �÷��̾ �������� �ʰų� ������ �迭�� ��������� ����
        if (player == null || planePrefabs.Length == 0) return;

        Vector3 spawnPosition;

        // ���� �ݰ� ������ ������ ������ ��ġ�� ����
        do
        {
            // ���� ���� ������ ������ x �� z ��ġ ����
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            // ������ ���� ���
            float yPosition = fixedHeight;

            // �÷��̾� ��ġ�� �������� ����� ���� ��ġ ���
            spawnPosition = new Vector3(
                player.transform.position.x + randomX,
                yPosition,
                player.transform.position.z + randomZ
            );

        } while (Vector3.Distance(player.transform.position, spawnPosition) <= safeRadius);

        // ������ ����� ������ ����
        int randomIndex = Random.Range(0, planePrefabs.Length);
        GameObject selectedPlanePrefab = planePrefabs[randomIndex];

        // ����� ����
        GameObject planeInstance = Instantiate(
            selectedPlanePrefab,
            spawnPosition,
            Quaternion.Euler(90f, 0f, 0f) // X������ 90�� ȸ��
        );

        // ������� �̵� ���� �߰�
        planeInstance.AddComponent<PlaneGravity>().gravity = gravity;

        // ���� �ð��� ������ �ڵ� �ı�
        Destroy(planeInstance, destroy_time);
    }
}

// �߷�ó�� �����ϰ� �ϴ� Ŭ����
public class PlaneGravity : MonoBehaviour
{
    public float gravity = 9.8f; // �߷� ��
    private float verticalSpeed = 0f; // Y�� �ӵ�

    void Update()
    {
        // �߷¿� ���� �ӵ��� ����
        verticalSpeed += gravity * Time.deltaTime;

        // Y������ ���������� �̵�
        transform.position -= new Vector3(0, verticalSpeed * Time.deltaTime, 0);

        // Y���� Ư�� �� ���Ϸ� �������� �ı�
        if (transform.position.y <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
