using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class Score : MonoBehaviour
{
    [SerializeField] public float point; // �ܺο��� ���� ������ ���� ����
    private float timeAlive; // ���� ������ ���� �ð� ����
    [SerializeField] public float scorePerKillIncreasePerSecond = 10f; // �ʴ� ���� ������

    private TextMeshProUGUI scoreText; // TextMeshProUGUI ������Ʈ ����

    void Start()
    {
        // ĵ�������� "scoreText" �̸��� TextMeshProUGUI ������Ʈ�� ã��
        scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();

        if (scoreText == null)
        {
            Debug.LogError("scoreText�� ã�� �� �����ϴ�. ĵ������ scoreText ������Ʈ�� �ִ��� Ȯ���ϼ���.");
        }
    }

    void Update()
    {
        // �ð� ����
        timeAlive += Time.deltaTime;

        // �� ������ ���� �κи� TextMeshPro�� ǥ��
        if (scoreText != null)
        {
            scoreText.text = $"Score: {(int)point}";
        }
    }

    public void AddKillScore(float score)
    {
        // ���� óġ�� �� ���� �߰�
        point += score;
        Debug.Log($"���� ����: {score}, �� ����: {point}"); // ���� ��ȭ ����� �޽���
    }

    public void ApplyStatIncreases(MonoBehaviour stats)
    {
        // EnemyStats Ÿ���� ��� ó��
        if (stats is EnemyStats enemyStats)
        {
            enemyStats.scorePerKill += scorePerKillIncreasePerSecond * Time.deltaTime;
        }
        // AlienStats Ÿ���� ��� ó��
        else if (stats is AlienStats alienStats)
        {
            alienStats.scorePerKill += scorePerKillIncreasePerSecond * Time.deltaTime;
        }
    }
}
