using Godot;
using System;
using System.Linq;

public partial class Skeleton : CharacterBody2D
{
	/// <summary>
	/// Number of pixels to raycast down to the floor.
	/// </summary>
	[Export] private float _floorRayCastLength = 16.0f;
	/// <summary>
	/// Negative attacks left. Positive attacks right.
	/// </summary>
	[Export] private float _attackDistance = -18.0f;
	/// <summary>
	/// Attack Rate in seconds.
	/// </summary>
	[Export] private float _attackRate = 1.0f;

	private RayCast2D _floorRayCast;
	private RayCast2D _attackRayCast;
	private Timer _attackTimer;
	private AnimatedSprite2D _sprite;
	private Vector2 _walkingDirection = Vector2.Left;
	private Player _player;

	private bool IsFacingLeft => _sprite.FlipH == false;
	private float _speed = 50.0f;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		GD.Print("Skeleton ready!");
		InitializePlayer();
		InitializeSprite();
		InitializeAttackTimer();
		InitializeRayCasts();
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

	public void Die()
	{
		// Stop motion
		Velocity = Vector2.Zero;
		// Play death animation
		_sprite.Play(SkeletonAnimations.Die);
		// TODO: Add sound effect here
		// Side effect is that the skeleton will
		// be removed from the scene tree
	}

	private void OnAttackTimerTimeout()
	{
		if (!IsFacingLeft && _attackDistance < 0) _attackDistance *= -1;
		if (_attackRayCast == null) return;
		_attackRayCast.TargetPosition = new Vector2(_attackDistance, _attackRayCast.Position.Y);
		_attackRayCast.ForceRaycastUpdate();
		if (_attackRayCast.IsColliding())
		{
			var collider = _attackRayCast.GetCollider() as Node2D;
			if (collider is Player)
			{
				GD.Print("Attack player!");
				_player.TakeDamage(1); // Take 1 damage
			}
		}
	}

	private void InitializePlayer()
	{
		_player = GetTree().GetNodesInGroup("Player").FirstOrDefault() as Player;
		if (_player != null)
		{
			_walkingDirection = (_player.Position - Position).Normalized();
		}
	}

	private void InitializeSprite()
	{
		_sprite = GetNode<AnimatedSprite2D>("%Sprite");
		_sprite.AnimationFinished += OnAnimatedFinished;
		_sprite.Play(SkeletonAnimations.Walk);
	}

	private void InitializeAttackTimer()
	{
		_attackTimer = GetNode<Timer>("%AttackTimer");
		_attackTimer.WaitTime = _attackRate;
		_attackTimer.Start();
		_attackTimer.Timeout += OnAttackTimerTimeout;
	}

	private void InitializeRayCasts()
	{
		_floorRayCast = GetNode<RayCast2D>("%FloorRayCast");
		_attackRayCast = GetNode<RayCast2D>("%AttackRayCast");

		_floorRayCast.TargetPosition = new Vector2(0.0f, _floorRayCastLength);
	}

	private void HandleFacingDirection(Vector2 velocity)
	{
		if (velocity.X < 0)
		{
			_sprite.FlipH = false; // Facing left
		}
		else if (velocity.X > 0)
		{
			_sprite.FlipH = true; // Facing right
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
		if (!IsOnFloor() && _floorRayCast.IsColliding())
		{
			Vector2 groundPosition = _floorRayCast.GetCollisionPoint();
			// Position will snap to location just above the raycast
			Position = new Vector2(Position.X, groundPosition.Y - _floorRayCastLength);
		}
	}
}

public class SkeletonAnimations
{
	public static string Die => "Die";
	public static string Walk => "Walk";
}
