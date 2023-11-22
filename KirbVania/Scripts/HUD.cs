using Godot;
using System;
using System.Collections.Generic;

public partial class HUD : Control
{
	private Texture2D _heartFull;
	private Texture2D _heartHalf;
	private Texture2D _heartEmpty;

	private TextureRect _heart1;
	private TextureRect _heart2;
	private TextureRect _heart3;
	private Label _scoreAmount;

	public override void _Ready()
	{
		_heartEmpty = ResourceLoader.Load("res://Assets/HeartEmpty.png") as Texture2D;
		_heartHalf = ResourceLoader.Load("res://Assets/HeartHalf.png") as Texture2D;
		_heartFull = ResourceLoader.Load("res://Assets/HeartFull.png") as Texture2D;
		_heart1 = GetNode<TextureRect>("Container/Heart1");
		_heart2 = GetNode<TextureRect>("Container/Heart2");
		_heart3 = GetNode<TextureRect>("Container/Heart3");
		_scoreAmount = GetNode<Label>("Container/ScoreAmount");
	}

	public void SetScore(int score)
	{
		_scoreAmount.Text = score.ToString();
	}

	public void SetHealth(int hp)
	{
		// Max HP is 6
		switch (hp)
		{
			case 6:
				_heart1.Texture = _heartFull;
				_heart2.Texture = _heartFull;
				_heart3.Texture = _heartFull;
				break;
			case 5:
				_heart1.Texture = _heartFull;
				_heart2.Texture = _heartFull;
				_heart3.Texture = _heartHalf;
				break;
			case 4:
				_heart1.Texture = _heartFull;
				_heart2.Texture = _heartFull;
				_heart3.Texture = _heartEmpty;
				break;
			case 3:
				_heart1.Texture = _heartFull;
				_heart2.Texture = _heartHalf;
				_heart3.Texture = _heartEmpty;
				break;
			case 2:
				_heart1.Texture = _heartFull;
				_heart2.Texture = _heartEmpty;
				_heart3.Texture = _heartEmpty;
				break;
			case 1:
				_heart1.Texture = _heartHalf;
				_heart2.Texture = _heartEmpty;
				_heart3.Texture = _heartEmpty;
				break;
			default:
				_heart1.Texture = _heartEmpty;
				_heart2.Texture = _heartEmpty;
				_heart3.Texture = _heartEmpty;
				break;
		}
	}
}
