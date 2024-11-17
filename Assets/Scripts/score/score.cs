using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

public class Score : MonoBehaviour
{
    [SerializeField] public float point; // 외부에서 접근 가능한 점수 변수
    private float timeAlive; // 점수 증가를 위한 시간 변수
    [SerializeField] public float scorePerKillIncreasePerSecond = 10f; // 초당 점수 증가량

    private TextMeshProUGUI scoreText; // TextMeshProUGUI 컴포넌트 변수

    void Start()
    {
        // 캔버스에서 "scoreText" 이름의 TextMeshProUGUI 컴포넌트를 찾음
        scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();

        if (scoreText == null)
        {
            Debug.LogError("scoreText를 찾을 수 없습니다. 캔버스에 scoreText 오브젝트가 있는지 확인하세요.");
        }
    }

    void Update()
    {
        // 시간 증가
        timeAlive += Time.deltaTime;

        // 총 점수를 정수 부분만 TextMeshPro에 표시
        if (scoreText != null)
        {
            scoreText.text = $"Score: {(int)point}";
        }
    }

    public void AddKillScore(float score)
    {
        // 적을 처치할 때 점수 추가
        point += score;
        Debug.Log($"점수 증가: {score}, 총 점수: {point}"); // 점수 변화 디버그 메시지
    }

    public void ApplyStatIncreases(MonoBehaviour stats)
    {
        // EnemyStats 타입일 경우 처리
        if (stats is EnemyStats enemyStats)
        {
            enemyStats.scorePerKill += scorePerKillIncreasePerSecond * Time.deltaTime;
        }
        // AlienStats 타입일 경우 처리
        else if (stats is AlienStats alienStats)
        {
            alienStats.scorePerKill += scorePerKillIncreasePerSecond * Time.deltaTime;
        }
    }
}
