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
	private AudioStreamPlayer2D _pickupAudio;
	private bool _isPickedUp = false;


	public override void _Ready()
	{
		_pickupArea = GetNode<Area2D>("%PickupArea");
		_pickupAudio = GetNode<AudioStreamPlayer2D>("PickupSFX");
		_pickupAudio.Finished += OnPickupAudioFinished;
		ConnectPickupAreaSignals();
	}

	private void OnPickupAudioFinished()
	{
		QueueFree();
	}

	private void ConnectPickupAreaSignals()
	{
		if (_pickupArea == null) return;
		_pickupArea.BodyEntered += OnPickupAreaBodyEntered;
	}

	private void OnPickupAreaBodyEntered(Node2D body)
	{
		if (body is Player player && !_isPickedUp)
		{
			// Increase health
			player.Heal(1);
			// Disable picking up twice
			_isPickedUp = true;
			Visible = false;
			// Play pickup sound; side effect - destroy node when finished
			_pickupAudio.Play();
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
