using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Passive };
    public Type type;
    public int id;
    public GameItem gameItem;
    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        gameItem = CommonItem.instance.GetGameItemById(id); // id�� ������� gameItem ��������
    }


    private void Update()
    {
        if (type == Type.Passive)
        {
            // y�� �������� ȸ��
            transform.Rotate(20 * Time.deltaTime, 0, 0);
        }

    }


    public void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("�浹 ����: " + other.gameObject.name); // �浹�� ��ü�� �̸� �α� ���

        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("�÷��̾�� �浹"); // �÷��̾�� �浹������ �α� ���

            CharacterStats characterStats = other.GetComponent<CharacterStats>();

            UnityEngine.Debug.Log("CharacterStats ������Ʈ: " + characterStats); // CharacterStats ������Ʈ �α� ���

            if (type == Type.Passive)
            {
                if (gameItem != null)
                {
                    characterStats.AddToInventory(gameItem); // �������� �κ��丮�� �߰��մϴ�.
                    UnityEngine.Debug.Log(gameItem.itemName + "��(��) ȹ���Ͽ����ϴ�."); // ������ ȹ�� �α�
                    gameItem.ApplyEffect(characterStats);
                    Destroy(gameObject);
                }
                else
                {
                    UnityEngine.Debug.Log("gameItem�� null�Դϴ�.");
                }
            }
        }
    }
}
