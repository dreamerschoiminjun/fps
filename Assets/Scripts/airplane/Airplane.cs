using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCrash : MonoBehaviour
{
    private float time = 0f;
    public float limit_time = 5f; // 비행기 생성 간격
    public GameObject[] planePrefabs; // 여러 비행기 프리팹 배열
    public float spawnRange = 150f; // 생성 범위
    public float gravity = 9.8f; // Y축으로 떨어지는 중력 값
    public float destroy_time = 10f; // 파괴 시간
    public float y_limit = 0f; // 파괴 조건 (0 이하로 변경)
    public float fixedHeight = 150f; // 고정된 높이
    public float safeRadius = 75f; // 플레이어 주변 안전 반경

    private GameObject player; // 플레이어 GameObject에 대한 참조

    void Start()
    {
        // 플레이어 객체 찾기
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("플레이어가 없습니다! 'Player' 태그가 설정되어 있는지 확인하세요.");
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
        // 플레이어가 존재하지 않거나 프리팹 배열이 비어있으면 리턴
        if (player == null || planePrefabs.Length == 0) return;

        Vector3 spawnPosition;

        // 안전 반경 조건을 만족할 때까지 위치를 생성
        do
        {
            // 생성 범위 내에서 랜덤한 x 및 z 위치 설정
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);

            // 고정된 높이 사용
            float yPosition = fixedHeight;

            // 플레이어 위치를 기준으로 비행기 생성 위치 계산
            spawnPosition = new Vector3(
                player.transform.position.x + randomX,
                yPosition,
                player.transform.position.z + randomZ
            );

        } while (Vector3.Distance(player.transform.position, spawnPosition) <= safeRadius);

        // 랜덤한 비행기 프리팹 선택
        int randomIndex = Random.Range(0, planePrefabs.Length);
        GameObject selectedPlanePrefab = planePrefabs[randomIndex];

        // 비행기 생성
        GameObject planeInstance = Instantiate(
            selectedPlanePrefab,
            spawnPosition,
            Quaternion.Euler(90f, 0f, 0f) // X축으로 90도 회전
        );

        // 비행기의 이동 로직 추가
        planeInstance.AddComponent<PlaneGravity>().gravity = gravity;

        // 일정 시간이 지나면 자동 파괴
        Destroy(planeInstance, destroy_time);
    }
}

// 중력처럼 동작하게 하는 클래스
public class PlaneGravity : MonoBehaviour
{
    public float gravity = 9.8f; // 중력 값
    private float verticalSpeed = 0f; // Y축 속도

    void Update()
    {
        // 중력에 의해 속도가 증가
        verticalSpeed += gravity * Time.deltaTime;

        // Y축으로 내려가도록 이동
        transform.position -= new Vector3(0, verticalSpeed * Time.deltaTime, 0);

        // Y축이 특정 값 이하로 내려가면 파괴
        if (transform.position.y <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
