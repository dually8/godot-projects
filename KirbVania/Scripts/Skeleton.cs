using Godot;
using System;
using System.Linq;

public partial class Skeleton : CharacterBody2D
{
	private AnimatedSprite2D _sprite;
	private Vector2 _walkingDirection = Vector2.Left;
	private float _speed = 50.0f;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	[Export] private RayCast2D _rayCast2D;
	[Export] private float _rayCastLength = 16.0f;

	public override void _Ready()
	{
		GD.Print("Skeleton ready!");
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_sprite.AnimationFinished += OnAnimatedFinished;
		_sprite.Play(SkeletonAnimations.Walk);
		_rayCast2D.TargetPosition = new Vector2(0.0f, _rayCastLength);
		if (GetTree().GetNodesInGroup("Player").FirstOrDefault() is Player player)
		{
			_walkingDirection = (player.Position - Position).Normalized();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// Apply gravity
		var velocity = Velocity;
		if (!IsOnFloor())
			velocity.Y += _gravity * (float)delta;
		velocity.X = _walkingDirection.X * _speed;
		HandleFacingDirection(velocity);
		Velocity = velocity;
		SnapToFloor();
		MoveAndSlide();
	}

	private void HandleFacingDirection(Vector2 velocity)
	{
		if (velocity.X < 0)
		{
			_sprite.FlipH = false;
		}
		else if (velocity.X > 0)
		{
			_sprite.FlipH = true;
		}
	}

	private void OnAnimatedFinished()
	{
		if (_sprite.Animation == SkeletonAnimations.Die)
		{
			QueueFree();
		}
	}

	private void SnapToFloor()
	{
		if (!IsOnFloor() && _rayCast2D.IsColliding())
		{
			Vector2 groundPosition = _rayCast2D.GetCollisionPoint();
			// Position will snap to location just above the raycast
			Position = new Vector2(Position.X, groundPosition.Y - _rayCastLength);
		}
	}

	public void Die()
	{
		// Play death animation
		_sprite.Play(SkeletonAnimations.Die);
		// TODO: Add sound effect here
		// Side effect is that the skeleton will
		// be removed from the scene tree
	}
}

public class SkeletonAnimations
{
	public static string Die => "Die";
	public static string Walk => "Walk";
}
