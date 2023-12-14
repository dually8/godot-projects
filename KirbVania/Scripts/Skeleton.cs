using Godot;
using System;
using System.Linq;
using KirbVania.Scripts;

public partial class Skeleton : CharacterBody2D
{
	[Signal]
	public delegate void DestroyedEventHandler();

	/// <summary>
	/// Number of pixels to raycast down to the floor.
	/// </summary>
	[Export] private float _floorRayCastLength = 16.0f;

	private RayCast2D _floorRayCast;
	private AnimatedSprite2D _sprite;
	private Vector2 _walkingDirection = Vector2.Left;
	private Player _player;
	private Area2D _hitBox;

	private bool IsFacingLeft => _sprite.FlipH == false;
	private float _speed = 50.0f;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	/// <summary>
	/// john is kill
	/// no
	/// but srsly, this is used to stop the skeleton from moving
	/// after it has been killed
	/// </summary>
	private bool _isKill = false;

	public override void _Ready()
	{
		AddToGroup(Groups.Enemy.ToString());
		InitializePlayer();
		InitializeSprite();
		SetFloorSnapLength();
		InitializeRayCasts();
		InitializeHitBox();
	}

	private void SetFloorSnapLength()
	{
		var spriteSize = _sprite.SpriteFrames.GetFrameTexture("Walk", 0).GetSize();
		FloorSnapLength = spriteSize.Y / 2;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_isKill)
		{
			Velocity = Vector2.Zero;
			MoveAndSlide();
			return;
		}
		// Apply gravity
		var velocity = Velocity;
		if (!IsOnFloor())
			velocity.Y += _gravity * (float)delta;
		SetWalkingDirection(); // Comment this if you want the skeleton to not follow the player
		velocity.X = _walkingDirection.X * _speed;
		HandleFacingDirection(velocity);
		Velocity = velocity;
		SnapToFloor();
		MoveAndSlide();
	}

	public void Destroy()
	{
		if (_isKill) return;
		// Side effect; stop moving (see PhysicsProcess)
		_isKill = true;
		// Play death animation
		// Side effect is that the skeleton will
		// be removed from the scene tree
		_sprite.Play(SkeletonAnimations.Die);
		EmitSignal(SignalName.Destroyed);
	}

	private void InitializeHitBox()
	{
		_hitBox = GetNode<Area2D>("HitBox");
		_hitBox.BodyEntered += OnHitBoxBodyEntered;
	}

	private void OnHitBoxBodyEntered(Node2D body)
	{
		// Can't be hurtin' the player if you're dead lol
		if (_isKill) return;

		if (body is Player)
		{
			_player.TakeDamage(1); // Take 1 damage
			_player.ApplyKnockback(IsFacingLeft ? Vector2.Left : Vector2.Right);
		}
	}

	private void InitializePlayer()
	{
		_player = GetTree()
			.GetNodesInGroup(Groups.Player.ToString())
			.FirstOrDefault() as Player;
		SetWalkingDirection();
	}

	private void SetWalkingDirection()
	{
		if (IsInstanceValid(_player))
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

	private void InitializeRayCasts()
	{
		_floorRayCast = GetNode<RayCast2D>("%FloorRayCast");
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
