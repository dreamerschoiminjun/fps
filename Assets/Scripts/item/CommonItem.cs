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
            itemName = "탄약",
            description = "20초 동안 공격력이 2배로 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 2
        gameItems.Add(new GameItem
        {
            id = 2,
            itemName = "물통",
            description = "20초 동안 이동 속도가 1.5배로 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 3
        gameItems.Add(new GameItem
        {
            id = 3,
            itemName = "방패",
            description = "5초간 무적 상태가 됩니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 4
        gameItems.Add(new GameItem
        {
            id = 4,
            itemName = "힐팩",
            description = "HP가 최대 HP의 절반만큼 회복됩니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 5
        gameItems.Add(new GameItem
        {
            id = 5,
            itemName = "소음기",
            description = "5초 동안 공격력이 500 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 6
        gameItems.Add(new GameItem
        {
            id = 6,
            itemName = "탄창",
            description = "총알 크기가 20초 동안 5배로 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 7
        gameItems.Add(new GameItem
        {
            id = 7,
            itemName = "랜덤박스",
            description = "총알 변화!",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });
    }

    public List<GameItem> GetGameItems()
    {
        return gameItems;
    }

    // id 값을 기반으로 GameItem을 가져오는 메서드
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
