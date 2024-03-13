using Godot;
using System;

public partial class GlobalPlayerInfo : Node
{
    private GlobalPlayerInfo instance = null;

    private static int cherryRemeber = 0;
    private static int gemRemeber = 0;
    public static int CherryCount = 0;
    public static int GemCount = 0;
    public static int MaxHealth = 0;

    public override void _Ready()
    {
        if (instance != null)
        {
            QueueFree();
            return;
        }
        instance = this;
    }

    public static void RemeberCollecibles()
    {
        cherryRemeber = CherryCount;
        gemRemeber = GemCount;
    }

    public static void LoadRemeberedCollectibles()
    {
        CherryCount = cherryRemeber;
        GemCount = gemRemeber;
    }

    public static void ResetInfo()
    {
        CherryCount = 0;
        GemCount = 0;
        MaxHealth = 0;
        gemRemeber = 0;
        cherryRemeber = 0;
    }

}
