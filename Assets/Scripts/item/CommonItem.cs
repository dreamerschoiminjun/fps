using System.Collections.Generic;
using UnityEngine;

public class CommonItem : MonoBehaviour
{
    public static CommonItem instance;

    public List<GameItem> gameItems;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameItems = new List<GameItem>();

        // Item 1
        gameItems.Add(new GameItem
        {
            id = 1,
            itemName = "ź��",
            description = "20�� ���� ���ݷ��� 2��� �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 2
        gameItems.Add(new GameItem
        {
            id = 2,
            itemName = "����",
            description = "20�� ���� �̵� �ӵ��� 1.5��� �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 3
        gameItems.Add(new GameItem
        {
            id = 3,
            itemName = "����",
            description = "5�ʰ� ���� ���°� �˴ϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 4
        gameItems.Add(new GameItem
        {
            id = 4,
            itemName = "����",
            description = "HP�� �ִ� HP�� ���ݸ�ŭ ȸ���˴ϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 5
        gameItems.Add(new GameItem
        {
            id = 5,
            itemName = "������",
            description = "5�� ���� ���ݷ��� 500 �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 6
        gameItems.Add(new GameItem
        {
            id = 6,
            itemName = "źâ",
            description = "�Ѿ� ũ�Ⱑ 20�� ���� 5��� �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 7
        gameItems.Add(new GameItem
        {
            id = 7,
            itemName = "�����ڽ�",
            description = "�Ѿ� ��ȭ!",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });
    }

    public List<GameItem> GetGameItems()
    {
        return gameItems;
    }

    // id ���� ������� GameItem�� �������� �޼���
    public GameItem GetGameItemById(int id)
    {
        foreach (GameItem item in gameItems)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
