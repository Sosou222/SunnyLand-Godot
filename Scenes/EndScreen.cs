using Godot;
using System;

public partial class EndScreen : Control
{
	[Export]
	private Label congratulationsLabel;
	[Export]
	private Sprite2D cherrySprite;
	[Export]
	private Label cherryLabel;
	[Export]
	private Label noteLabel;

	private Tween tween;

	private int currentCherryValue = 0;
	private int CurrentCherryValue
	{
		get { return currentCherryValue; }
		set { currentCherryValue = value; UpdateCherryCount(); }
	}

	public override void _Ready()
	{
		SetNoteText();
		congratulationsLabel.Visible = true;

		tween = GetTree().CreateTween();
		tween.TweenInterval(1.0f);
		tween.TweenProperty(cherrySprite, "visible", true, 0.05f);
		tween.TweenProperty(this, "CurrentCherryValue", GlobalPlayerInfo.CherryCount, 2.0f).From(0);
		tween.TweenInterval(1.0f);
		tween.TweenProperty(noteLabel, "visible", true, 0.05f);
		tween.TweenInterval(4.0f);

		tween.Play();
	}

	public override void _Process(double delta)
	{
		if (!tween.IsValid())
		{
			SceneManager.instance.LoadScene("MainMenu");
		}
	}

	private void SetNoteText()
	{
		if (GlobalPlayerInfo.CherryCount < 10)
		{
			noteLabel.Text = "Try better next time";
		}
		else if (GlobalPlayerInfo.CherryCount < 35)
		{
			noteLabel.Text = "You did good!";
		}
		else
		{
			noteLabel.Text = "Perfect. All cherries got!";
		}
	}

	private void UpdateCherryCount()
	{
		cherryLabel.Text = "x" + currentCherryValue;
	}
}
