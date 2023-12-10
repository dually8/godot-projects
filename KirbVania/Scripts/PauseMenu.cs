using Godot;
using System;

public partial class PauseMenu : Control
{
	private Button _resumeButton;
	private Button _optionsButton;
	private Button _exitButton;
	private GameManager _gameManager;

	public override void _Ready()
	{
		_resumeButton = GetNode<Button>("MarginContainer/VBoxContainer/ResumeBtn");
		_resumeButton.Pressed += OnResumePressed;
		_resumeButton.GrabFocus();
		_optionsButton = GetNode<Button>("MarginContainer/VBoxContainer/OptionsBtn");
		_optionsButton.Pressed += OnOptionsPressed;
		_exitButton = GetNode<Button>("MarginContainer/VBoxContainer/ExitBtn");
		_exitButton.Pressed += OnExitPressed;
		_gameManager = GetNode<GameManager>("/root/Main");
		VisibilityChanged += OnVisibilityChanged;
	}

	private void OnVisibilityChanged()
	{
		if (Visible)
		{
			_resumeButton.GrabFocus();
		}
		else
		{
			_resumeButton.ReleaseFocus();
		}
	}

	private void OnResumePressed()
	{
		// Unpause the game
		_gameManager.UnpauseGame();
	}

	private void OnOptionsPressed()
	{
		// Go to options menu
		// GetTree().ChangeSceneToFile("res://Prefabs/options_menu.tscn");
		GD.Print("TODO: Implement options menu that can come back to this thing");
	}

	private void OnExitPressed()
	{
		// Exit to main menu
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile("res://Prefabs/main_menu.tscn");
	}
}
