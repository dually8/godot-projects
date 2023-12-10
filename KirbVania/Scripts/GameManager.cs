using Godot;
using System;
using KirbVania.Scripts;

public partial class GameManager : Node2D
{
	[Export] private Player _player { get; set; }
	[Export] private HUD _hud { get; set; }
	[Export] private PauseMenu _pauseMenu { get; set; }

	private int _score = 0;
	private bool _isPaused = false;
	private AudioStreamPlayer2D _levelMusic;

	public override void _Ready()
	{
		_levelMusic = GetNode<AudioStreamPlayer2D>("LevelMusic");
		_levelMusic.Finished += OnMusicFinished;
		_player.IncreaseScore += OnPlayerScoreIncrease;
		InitializeUI();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed(InputActions.TogglePause))
		{
			PauseGame();
		}
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

	private void PauseGame()
	{
		if (_isPaused) return;
		_isPaused = true;
		_pauseMenu.Show();
		_levelMusic.StreamPaused = true;
		GetTree().Paused = true;
	}

	public void UnpauseGame()
	{
		if (!_isPaused) return;
		_isPaused = false;
		_levelMusic.StreamPaused = false;
		GetTree().Paused = false;
	}
}
