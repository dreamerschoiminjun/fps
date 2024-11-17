using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class Beam : MonoBehaviour
{
    [Header("Beam Settings")]
    public GameObject volumetricLinePrefab; // Volumetric Line ������ ����
    private VolumetricLineBehavior volumetricLine; // VolumetricLineBehavior ��ũ��Ʈ ����

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f;
    public float textureScrollSpeed = 8f;
    public float textureLengthScale = 3;

    [Header("Damage Settings")]
    public float damageInterval = 0.1f;
    private float damageTimer = 0f;
    private bool isDamageActive = false;
    private GameObject player; // �÷��̾� ������Ʈ

    void Start()
    {
        // Volumetric Line �������� �ν��Ͻ�ȭ�ϰ� ����
        GameObject lineInstance = Instantiate(volumetricLinePrefab, transform.position, Quaternion.identity, transform);
        volumetricLine = lineInstance.GetComponent<VolumetricLineBehavior>();

        player = GameObject.FindWithTag("Player");
        DeactivateBeam();
    }

    void Update()
    {
        if (player != null && isDamageActive)
        {
            Vector3 direction = player.transform.position - transform.position;
            UpdateVolumetricLine(transform.position, player.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    damageTimer += Time.deltaTime;
                    if (damageTimer >= damageInterval)
                    {
                        TakeDamage(hit.collider.gameObject);
                        damageTimer = 0f;
                    }
                }
            }
        }
    }

    private void UpdateVolumetricLine(Vector3 start, Vector3 end)
    {
        // VolumetricLineBehavior�� ����Ͽ� ���۰� �� ��ġ�� ���� ��ǥ�� ����
        volumetricLine.StartPos = transform.InverseTransformPoint(start);
        volumetricLine.EndPos = transform.InverseTransformPoint(end);
    }

    public void ActivateBeam()
    {
        volumetricLine.gameObject.SetActive(true);
        isDamageActive = true;
    }

    public void DeactivateBeam()
    {
        volumetricLine.gameObject.SetActive(false);
        isDamageActive = false;
    }

    private void TakeDamage(GameObject player)
    {
        CharacterStats characterStats = player.GetComponent<CharacterStats>();
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        if (characterStats != null && enemyStats != null)
        {
            characterStats.CurrentHealth -= enemyStats.attackPower;
        }
    }
}
