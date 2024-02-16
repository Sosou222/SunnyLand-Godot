using Godot;
using System;

public partial class MainMenu : Control
{
    private void OnStartButtonPressed()
    {
        SceneManager.instance.LoadScene("TestLevel");
    }

    private void OnQuitButtonPressed()
    {
        SceneManager.instance.QuitGame();
    }
}
