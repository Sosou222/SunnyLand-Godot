using Godot;
using System;

public partial class SceneManager : Node
{
    public static SceneManager instance { private set; get; }

    private string SceneFolder = "Scenes";

    private PackedScene loadingScene = null;
    private PackedScene settingsScene = null;

    private string sceneToLoad = "";

    public override void _Ready()
    {
        if (instance != null)
        {
            QueueFree();
            return;
        }
        instance = this;

        loadingScene = ResourceLoader.Load<PackedScene>($"res://{SceneFolder}/LoadingScreen.tscn");
        settingsScene = ResourceLoader.Load<PackedScene>($"res://{SceneFolder}/Settings.tscn");
    }

    public override void _Process(double delta)
    {
        if (sceneToLoad == "")
            return;
        var sceneLoadStatus = ResourceLoader.LoadThreadedGetStatus(sceneToLoad);
        if (sceneLoadStatus == ResourceLoader.ThreadLoadStatus.Loaded)
        {
            var newScene = ResourceLoader.LoadThreadedGet(sceneToLoad) as PackedScene;
            GetTree().ChangeSceneToPacked(newScene);
            sceneToLoad = "";
        }
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "Settings")
        {
            GetTree().ChangeSceneToPacked(settingsScene);
            return;
        }
        GetTree().ChangeSceneToPacked(loadingScene);
        sceneToLoad = $"res://{SceneFolder}/{sceneName}.tscn";
        ResourceLoader.LoadThreadedRequest(sceneToLoad);
    }

    public void ReloadScene()
    {
        GetTree().ReloadCurrentScene();
    }

    public void QuitGame()
    {
        GetTree().Quit();
    }

}
