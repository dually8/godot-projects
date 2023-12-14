using Godot;
using System;

public partial class GameOverScreen : Control
{
	private Button _restartButton;

	public override void _Ready()
	{
		_restartButton = GetNode<Button>("VBoxContainer/RestartBtn");
		_restartButton.Pressed += OnRestartPressed;
	}

	private void OnRestartPressed()
	{
		GetTree().ReloadCurrentScene();
	}

	public void ShowScreen()
	{
		Show();
		_restartButton.GrabFocus();
	}
}
