using System.Collections;
using System.Collections.Generic;
using GameManagers;
using Unity.FPS.Gameplay;
using Unity.Netcode;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private UIDocument m_PauseDocument;

    private VisualElement m_Root;
    private bool m_Paused;

    private void Start()
    {
        m_Root = m_PauseDocument.rootVisualElement;
        SetPause(false);

        m_Root.Q<Button>("Resume").clicked += () =>
        {
            SetPause(false);
        };

        m_Root.Q<Button>("Quit").clicked += () =>
        {
            if (ScoreManager.Instance)
                Destroy(ScoreManager.Instance.gameObject);
            NetworkManager.Singleton.Shutdown();
            Destroy(NetworkManager.Singleton.gameObject);
            SceneManager.LoadScene("Base", LoadSceneMode.Single);
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!m_Paused);
        }
    }

    private void SetPause(bool pIsPause)
    {
        GameManager.Instance.IsClientPaused = pIsPause;
        m_Paused = pIsPause;
        UnityEngine.Cursor.lockState = pIsPause ? CursorLockMode.None : CursorLockMode.Locked;
        UnityEngine.Cursor.visible = pIsPause ? true : false;

        m_Root.style.display = pIsPause ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
