using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    private float time;
    public int limit_time = 60;
    public GameObject player;

    // 6���� ������ �������� ������ ����Ʈ
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

            // �÷��̾� �ֺ��� ������ ��ġ ��� (y�� ����)
            position = new Vector3(
                Random.Range(player_pos.x - 20f, player_pos.x + 20f),
                5f, // y�� ��ġ�� 5�� ����
                Random.Range(player_pos.z - 20f, player_pos.z + 20f)
            );

            // ������ ��Ͽ��� �������� ������ ���� �� ����
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            GameObject selectedItem = itemPrefabs[randomIndex];

            GameObject go = Instantiate(selectedItem);
            go.transform.position = position;
        }
    }
}
