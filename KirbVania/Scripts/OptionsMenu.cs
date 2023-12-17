using Godot;
using System;

public partial class OptionsMenu : Control
{
	private Button _backButton;

	// TODO: Make some options -- video options, audio options, etc.

	public override void _Ready()
	{
		_backButton = GetNode<Button>("VBoxContainer/BackButton");
		_backButton.Pressed += OnBackButtonPressed;
		_backButton.GrabFocus();
	}

	private void OnBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Prefabs/main_menu.tscn");
	}
}
