using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _playButton;
	private Button _optionsButton;
	private Button _exitButton;

	public override void _Ready()
	{
		_playButton = GetNode<Button>("MarginContainer/VBoxContainer/PlayBtn");
		_playButton.Pressed += OnPlayPressed;
		_playButton.GrabFocus();
		_optionsButton = GetNode<Button>("MarginContainer/VBoxContainer/OptionsBtn");
		_optionsButton.Pressed += OnOptionsPressed;
		_exitButton = GetNode<Button>("MarginContainer/VBoxContainer/ExitButton");
		_exitButton.Pressed += OnExitPressed;
	}

	private void OnExitPressed()
	{
		GetTree().Quit();
	}

	private void OnOptionsPressed()
	{
		GetTree().ChangeSceneToFile("res://Prefabs/options_menu.tscn");
	}

	private void OnPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/main.tscn");
	}
}
