using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : Button
{
    protected override void Awake()
    {
        onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
