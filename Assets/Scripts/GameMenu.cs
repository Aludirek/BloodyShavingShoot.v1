using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static GameMenu Instance;
    public enum PlayerMovementInputType
    {
        ButtonBased, PointerBased, TiltInput
    }

    private string playerMovmentTypeKey = "PMIT";
    private PlayerMovementInputType _pp;
    public PlayerMovementInputType CurrenPMIT { get { return _pp; } }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SwitchToTilt()
    {
        _pp = PlayerMovementInputType.TiltInput;
        PlayerPrefs.SetInt(playerMovmentTypeKey, 2);
    }

    public void SwitchToPointer()
    {
        _pp = PlayerMovementInputType.PointerBased;
        PlayerPrefs.SetInt(playerMovmentTypeKey, 0);
    }

    public void SwitchToButton()
    {
        _pp = PlayerMovementInputType.ButtonBased;
        PlayerPrefs.SetInt(playerMovmentTypeKey, 1);
    }

    private void Start()
    {
        _pp = (PlayerMovementInputType)PlayerPrefs.GetInt(playerMovmentTypeKey);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
