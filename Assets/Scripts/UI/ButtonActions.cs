using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    public GameObject keyGuidePanel;
    public Button Button1;
    public Button Button2;
    public Button Button3;

    void Start()
    {
        // Hierarchy���� ��ư�� ã�Ƽ� �Ҵ�
        Button1 = GameObject.Find("Button1").GetComponent<Button>();
        Button2 = GameObject.Find("Button2").GetComponent<Button>();
        Button3 = GameObject.Find("Button3").GetComponent<Button>();

        // Canvas�� �ڽ� ������Ʈ���� ��Ȱ��ȭ�� Key Guide �г��� ã��
        keyGuidePanel = GameObject.Find("Canvas").transform.Find("Key Guide").gameObject;

        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ����
        Button1.onClick.AddListener(OnButton1Click);
        Button2.onClick.AddListener(OnButton2Click);
        Button3.onClick.AddListener(OnButton3Click);
    }

    // Button1�� ������ �� ����Ǵ� �޼���
    public void OnButton1Click()
    {
        SceneManager.LoadScene("SampleScene"); // "SampleScene" ������ �̵��մϴ�
    }

    // Button2�� ������ �� ����Ǵ� �޼���
    public void OnButton2Click()
    {
        if (keyGuidePanel != null)
        {
            keyGuidePanel.SetActive(!keyGuidePanel.activeSelf); // Ű ���̵� �г��� Ȱ��/��Ȱ�� ���¸� ��ȯ�մϴ�
        }
    }

    // Button3�� ������ �� ����Ǵ� �޼���
    public void OnButton3Click()
    {
        Application.Quit(); // ������ �����մϴ�
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ���̶�� Play ��带 �����մϴ�
#endif
    }
}

