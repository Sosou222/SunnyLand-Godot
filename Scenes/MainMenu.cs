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
        GlobalPlayerInfo.ResetInfo();
        SceneManager.instance.LoadScene("Level1");
        AudioManager.PlayMusic("Level");
    }

    private void OnSettingsButtonPressed()
    {
        SceneManager.instance.LoadScene("Settings");
    }

    private void OnQuitButtonPressed()
    {
        SceneManager.instance.QuitGame();
    }
}
