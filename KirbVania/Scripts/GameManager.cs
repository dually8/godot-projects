using Godot;
using System;

public partial class GameManager : Node2D
{
	[Export] private Player _player { get; set; }
	[Export] private HUD _hud { get; set; }
	private int _score = 0;

	public override void _Ready()
	{
		InitializeUI();;
		_player.IncreaseScore += OnIncreaseScore;
	}

	private void OnIncreaseScore(int amount)
	{
		GD.Print("Increase score by " + amount);
		_score += amount;
		_hud.SetScore(_score);
	}

	private void InitializeUI()
	{
		_hud.SetScore(0);
		_hud.SetHealth(6);
	}
}
