using Godot;
using System;

public partial class Coin : Area2D
{
	[Export]
	private int _value = 1;
	private float _rotationSpeed = 90.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotationDegrees += _rotationSpeed * (float)delta;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Character character)
		{
			character.CollectCoin(_value);
			QueueFree();
		}
	}

}
