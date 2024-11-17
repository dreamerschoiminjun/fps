using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    private float time;
    public int limit_time = 60;
    public GameObject player;

    // 6개의 아이템 프리팹을 저장할 리스트
    public List<GameObject> itemPrefabs = new List<GameObject>();

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > limit_time)
        {
            time = 0f;
            Vector3 player_pos = player.GetComponent<Transform>().position;

            // 플레이어 주변의 무작위 위치 계산 (y축 고정)
            position = new Vector3(
                Random.Range(player_pos.x - 20f, player_pos.x + 20f),
                5f, // y축 위치를 5로 고정
                Random.Range(player_pos.z - 20f, player_pos.z + 20f)
            );

            // 아이템 목록에서 무작위로 아이템 선택 및 생성
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            GameObject selectedItem = itemPrefabs[randomIndex];

            GameObject go = Instantiate(selectedItem);
            go.transform.position = position;
        }
    }
}
