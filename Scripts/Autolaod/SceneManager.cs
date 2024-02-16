using Godot;
using System;

public partial class SceneManager : Node
{
    public static SceneManager instance { private set; get; }

    private string SceneFolder = "Scenes";

    public override void _Ready()
    {
        if (instance != null)
        {
            QueueFree();
            return;
        }
        instance = this;
    }

    public void LoadScene(string sceneName)
    {
        GetTree().ChangeSceneToFile($"res://{SceneFolder}/{sceneName}.tscn");
    }

    public void QuitGame()
    {
        GetTree().Quit();
    }
}
