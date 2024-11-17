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
        // Hierarchy에서 버튼을 찾아서 할당
        Button1 = GameObject.Find("Button1").GetComponent<Button>();
        Button2 = GameObject.Find("Button2").GetComponent<Button>();
        Button3 = GameObject.Find("Button3").GetComponent<Button>();

        // Canvas의 자식 오브젝트에서 비활성화된 Key Guide 패널을 찾음
        keyGuidePanel = GameObject.Find("Canvas").transform.Find("Key Guide").gameObject;

        // 버튼 클릭 이벤트에 메서드 연결
        Button1.onClick.AddListener(OnButton1Click);
        Button2.onClick.AddListener(OnButton2Click);
        Button3.onClick.AddListener(OnButton3Click);
    }

    // Button1이 눌렸을 때 실행되는 메서드
    public void OnButton1Click()
    {
        SceneManager.LoadScene("SampleScene"); // "SampleScene" 씬으로 이동합니다
    }

    // Button2가 눌렸을 때 실행되는 메서드
    public void OnButton2Click()
    {
        if (keyGuidePanel != null)
        {
            keyGuidePanel.SetActive(!keyGuidePanel.activeSelf); // 키 가이드 패널의 활성/비활성 상태를 전환합니다
        }
    }

    // Button3이 눌렸을 때 실행되는 메서드
    public void OnButton3Click()
    {
        Application.Quit(); // 게임을 종료합니다
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중이라면 Play 모드를 종료합니다
#endif
    }
}

