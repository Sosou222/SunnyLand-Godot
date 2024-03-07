using Godot;
using System;

public partial class Settings : Control
{

	private int busMusicID = AudioServer.GetBusIndex("Music");
	private int busSFXID = AudioServer.GetBusIndex("SFX");

	private void OnValueChangedMusic(float value)
	{
		AudioServer.SetBusVolumeDb(busMusicID, Mathf.LinearToDb(value));
		AudioServer.SetBusMute(busMusicID, value < 0.05f);
	}
	private void OnValueChangedSFX(float value)
	{
		AudioServer.SetBusVolumeDb(busSFXID, Mathf.LinearToDb(value));
		AudioServer.SetBusMute(busSFXID, value < 0.05f);
	}

	private void OnDragEndedSFX(bool value)
	{
		if (value)
			AudioManager.PlayeSound("PlayerJump");
	}

	private void OnBackButtonPressed()
	{
		SceneManager.instance.LoadScene("MainMenu");
	}
}
