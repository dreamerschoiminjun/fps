using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameItem
{
    public int id; // ������ ID
    public string itemName; // ������ �̸�
    public string description; // ������ ����
    public Sprite icon; // ������ ������ (UI�� ���)
    public ItemType itemType; // ������ ���� (������)

    public enum ItemType
    {
        Buff, // ���� ������
        Debuff, // ����� ������
        Consumable // �Ҹ� ������
    }

    // ������ ȿ�� ���� �Լ�
    public void ApplyEffect(CharacterStats characterStats)
    {
        switch (id)
        {
            case 1:
                characterStats.StartCoroutine(ApplyItem1Effect(characterStats));
                break;
            case 2:
                characterStats.StartCoroutine(ApplyItem2Effect(characterStats));
                break;
            case 3:
                characterStats.StartCoroutine(ApplyItem3Effect(characterStats));
                break;
            case 4:
                ApplyItem4Effect(characterStats);
                break;
            case 5:
                characterStats.StartCoroutine(ApplyItem5Effect(characterStats));
                break;
            default:
                Debug.Log("ȿ���� ���ǵ��� ���� �������Դϴ�.");
                break;
        }
    }

    // ������ ȿ�� ���� (�ڷ�ƾ ȣ��)
    private IEnumerator ApplyItem1Effect(CharacterStats characterStats)
    {
        characterStats.rangedAttackPower *= 2;
        Debug.Log("20�� ���� ���ݷ� 2�� ����: " + characterStats.rangedAttackPower);
        yield return new WaitForSeconds(20f);
        characterStats.rangedAttackPower /= 2;
        Debug.Log("���ݷ� ����: " + characterStats.rangedAttackPower);
    }

    private IEnumerator ApplyItem2Effect(CharacterStats characterStats)
    {
        float moveSpeedIncrease = characterStats.MoveSpeed * 0.5f;
        float sprintSpeedIncrease = characterStats.SprintSpeed * 0.5f;
        characterStats.MoveSpeed += moveSpeedIncrease;
        characterStats.SprintSpeed += sprintSpeedIncrease;
        Debug.Log("20�� ���� �̵� �ӵ� 1.5�� ����: " + characterStats.MoveSpeed + ", " + characterStats.SprintSpeed);
        yield return new WaitForSeconds(20f);
        characterStats.MoveSpeed -= moveSpeedIncrease;
        characterStats.SprintSpeed -= sprintSpeedIncrease;
        Debug.Log("�̵� �ӵ� ����: " + characterStats.MoveSpeed + ", " + characterStats.SprintSpeed);
    }

    private IEnumerator ApplyItem3Effect(CharacterStats characterStats)
    {
        characterStats.CurrentHealth = Mathf.Infinity; // ���� ����
        Debug.Log("5�ʰ� ���� ����");
        yield return new WaitForSeconds(5f);
        characterStats.CurrentHealth = characterStats.maxHealth; // ���� ����
        Debug.Log("���� ���� ����");
    }

    private void ApplyItem4Effect(CharacterStats characterStats)
    {
        float healAmount = characterStats.maxHealth / 2; // �ִ� HP�� ���ݸ�ŭ ȸ��
        characterStats.CurrentHealth = Mathf.Min(characterStats.CurrentHealth + healAmount, characterStats.maxHealth); // �ִ� HP�� ���� �ʵ��� ����
        Debug.Log("HP ���� ȸ��: " + characterStats.CurrentHealth);
    }

    private IEnumerator ApplyItem5Effect(CharacterStats characterStats)
    {
        characterStats.rangedAttackPower += 500;
        Debug.Log("5�� ���� ���ݷ� +500: " + characterStats.rangedAttackPower);
        yield return new WaitForSeconds(5f);
        characterStats.rangedAttackPower -= 500;
        Debug.Log("���ݷ� ����: " + characterStats.rangedAttackPower);
    }
    // �Ѿ� ũ�⸦ 20�� ���� 5��� ������Ű�� ȿ��
    private IEnumerator ApplyItem6Effect(player player)
    {
        // BulletPrefab�� ũ�⸦ 5��� ����
        player.BulletPrefab.transform.localScale *= 5f;
        Debug.Log("�Ѿ� ũ�� 5�� ����: " + player.BulletPrefab.transform.localScale);

        // 20�� ���� ����
        yield return new WaitForSeconds(20f);

        // ũ�⸦ ������� ���� (/5)
        player.BulletPrefab.transform.localScale /= 5f;
        Debug.Log("�Ѿ� ũ�� ����: " + player.BulletPrefab.transform.localScale);
    }


    private void ApplyItem7Effect(CharacterStats characterStats, player player)
    {
        // �Ѿ� �������� �������� ����
        int randomBulletIndex = Random.Range(0, player.BulletPrefabs.Length);
        GameObject selectedBulletPrefab = player.BulletPrefabs[randomBulletIndex];

        // ���õ� �Ѿ� ���������� ����
        player.BulletPrefab = selectedBulletPrefab;
        Debug.Log("���� �Ѿ� ������ ����: " + selectedBulletPrefab.name);

    }

}
