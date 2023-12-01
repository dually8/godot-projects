using Godot;
using System;

public partial class GameManager : Node2D
{
	[Export] private Player _player { get; set; }
	[Export] private HUD _hud { get; set; }
	private int _score = 0;

	public override void _Ready()
	{
		InitializeUI();
	}

	public void OnSkeletonDestroyed()
	{
		_score += 100;
		_hud.SetScore(_score);
	}

	public void OnLightDestroyed()
	{
		_score += 50;
		_hud.SetScore(_score);
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
