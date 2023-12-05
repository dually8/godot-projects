using Godot;
using System;

public partial class GameManager : Node2D
{
	[Export] private Player _player { get; set; }
	[Export] private HUD _hud { get; set; }
	private int _score = 0;
	private AudioStreamPlayer2D _levelMusic;

	public override void _Ready()
	{
		_levelMusic = GetNode<AudioStreamPlayer2D>("LevelMusic");
		_levelMusic.Finished += OnMusicFinished;
		_player.IncreaseScore += OnPlayerScoreIncrease;
		InitializeUI();
	}

	private void OnPlayerScoreIncrease(int amount)
	{
		_score += amount;
		_hud.SetScore(_score);
	}

	private void OnMusicFinished()
	{
		_levelMusic.Play();
	}

	public void UpdateHealth(int amount)
	{
		_hud.SetHealth(amount);
	}

	private void InitializeUI()
	{
		_hud.SetScore(0);
		_hud.SetHealth(6);
	}
}
