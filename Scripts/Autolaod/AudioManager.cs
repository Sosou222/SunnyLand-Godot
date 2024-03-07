using Godot;
using System;

public partial class AudioManager : Node
{
    public static AudioManager instance { private set; get; }

    private AudioStreamPlayer currentMusicPlayer = null;

    private enum AudioType { Music, SFX }

    [Export]
    private Node musicParent;
    [Export]
    private Node sfxParent;

    public override void _Ready()
    {
        if (instance != null)
        {
            QueueFree();
            return;
        }
        instance = this;
    }

    private AudioStreamPlayer FindAudio(string name, AudioType audioType)
    {
        Node parent = null;
        if (audioType == AudioType.Music)
        {
            parent = musicParent;
        }
        if (audioType == AudioType.SFX)
        {
            parent = sfxParent;
        }

        foreach (var child in parent.GetChildren())
        {
            if (child is AudioStreamPlayer audioStreamPlayer)
            {
                if (audioStreamPlayer.Name == name)
                {
                    var audioDupe = audioStreamPlayer.Duplicate() as AudioStreamPlayer;
                    AddChild(audioDupe);
                    return audioDupe;
                }
            }
        }

        return null;

    }

    public static void PlayMusic(string musicName)
    {
        var newMusic = instance.FindAudio(musicName, AudioType.Music);
        if (newMusic == null)
        {
            GD.Print($"Could not find music named:{musicName}");
            return;
        }
        if (instance.currentMusicPlayer != null)
        {
            instance.currentMusicPlayer.Stop();
            instance.currentMusicPlayer.QueueFree();
        }
        instance.currentMusicPlayer = newMusic;
        instance.currentMusicPlayer.Play();
    }

    public static void PlayeSound(string sfxName)
    {
        var newSFX = instance.FindAudio(sfxName, AudioType.SFX);
        if (newSFX == null)
        {
            GD.Print($"Could not find sfx named:{sfxName}");
            return;
        }

        newSFX.Finished += () => instance.DestroySFX(newSFX);
        newSFX.Play();
    }

    private void DestroySFX(AudioStreamPlayer audioStreamPlayer)
    {
        audioStreamPlayer.QueueFree();
    }
}
