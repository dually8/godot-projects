using Godot;
using System;

public partial class GameOverScreen : Control
{
	private Button _restartButton;
	private AnimatedTextureRect _kirbyCry;

	public override void _Ready()
	{
		_restartButton = GetNode<Button>("VBoxContainer/RestartBtn");
		_restartButton.Pressed += OnRestartPressed;
		_kirbyCry = GetNode<AnimatedTextureRect>("KirbyCry");
	}

	private void OnRestartPressed()
	{
		GetTree().ReloadCurrentScene();
	}

	public void ShowScreen()
	{
		Show();
		_restartButton.GrabFocus();
		_kirbyCry.Play("default");
	}
}
