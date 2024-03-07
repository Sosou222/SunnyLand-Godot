using Godot;
using System;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        AudioManager.PlayMusic("Menu");
    }

    private void OnStartButtonPressed()
    {
        SceneManager.instance.LoadScene("TestLevel");
    }

    private void OnQuitButtonPressed()
    {
        SceneManager.instance.QuitGame();
    }
}
