using Godot;
using System;

public partial class HUD : Control
{
	private Label _scoreAmount;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_scoreAmount = GetNode<Label>("%ScoreAmount");
	}

	public void SetScore(int score)
	{
		_scoreAmount.Text = score.ToString();
	}
}
