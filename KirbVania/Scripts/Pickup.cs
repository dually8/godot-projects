using Godot;
using System;
using KirbVania.Scripts;

public partial class Pickup : CharacterBody2D
{
	private float _gravity = 50.0f;

	/// <summary>
	/// Determines how far the pickup will sway left and right.
	/// </summary>
	[Export] private float _horizontalMovement = 25.0f;

	/// <summary>
	/// Frequency of the oscillating movement. A lower number means faster movement.
	/// </summary>
	[Export] private float _swaySpeed = 200.0f;

	private Area2D _pickupArea;


	public override void _Ready()
	{
		_pickupArea = GetNode<Area2D>("%PickupArea");
		ConnectPickupAreaSignals();
	}

	private void ConnectPickupAreaSignals()
	{
		if (_pickupArea == null) return;
		_pickupArea.BodyEntered += OnPickupAreaBodyEntered;
	}

	private void OnPickupAreaBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			// Increase health
			player.Heal(1);
			// Destroy pickup
			QueueFree();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsOnFloor()) return;
		// Float down in zig zag pattern
		Vector2 velocity = Velocity;
		velocity.Y += _gravity * (float)delta;
		velocity.X = (float)(_horizontalMovement * Math.Sin(Time.GetTicksMsec() / _swaySpeed));
		Velocity = velocity;
		MoveAndSlide();
	}
}
