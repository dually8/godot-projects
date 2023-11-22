using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -300.0f;

	public float AttackDistance
	{
		get { return _animatedSprite2D.FlipH ? -26.0f : 26.0f; }
	}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private bool _isAttacking = false;

	[Export] private AnimatedSprite2D _animatedSprite2D { get; set; }
	[Export] private RayCast2D _rayCast2D { get; set; }
	[Export] private Camera2D _camera { get; set; }

	public override void _Ready()
	{
		GD.Print("Ready!");
		UseIdleAnimation();
		_animatedSprite2D.AnimationFinished += OnAnimatedFinished;
		_camera.Enabled = true;
		_camera.MakeCurrent();
	}

	private void OnAnimatedFinished()
	{
		if (_animatedSprite2D.Animation == PlayerAnimations.Jump && !IsOnFloor())
		{
			_animatedSprite2D.Play(PlayerAnimations.Falling);
			return;
		}

		if (_animatedSprite2D.Animation == PlayerAnimations.Whip)
		{
			ResetTextureOffset();
			UseIdleAnimation();
			HandleAnimations(Velocity);
			_isAttacking = false;
		}
	}

	public override void _Process(double delta)
	{
		_camera.Position = new Vector2(Position.X, _camera.Position.Y);
		_camera.ForceUpdateScroll();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
			GetTree().Quit();

		HandleMovement(delta);
		// QueueRedraw();
	}

	private void HandleMovement(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Handle horizontal movement
		var direction = Input.GetAxis("move_left", "move_right");
		velocity.X = _isAttacking && IsOnFloor() ? 0 : direction * Speed;

		HandleAnimations(velocity);

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("attack"))
		{
			_isAttacking = true;
			UseWhipAnimation();
			TryAttack();
		}
	}

	// public override void _Draw()
	// {
	// 	if (_rayCast2D.Enabled)
	// 	{
	// 		var rayColor = _rayCast2D.IsColliding() ? Colors.Red : Colors.Green;
	// 		DrawLine(Vector2.Zero, _rayCast2D.TargetPosition, rayColor, 2.0f);
	// 	}
	// }

	private void TryAttack()
	{
		_rayCast2D.TargetPosition = new Vector2(AttackDistance, 0);
		_rayCast2D.ForceRaycastUpdate();
		if (_rayCast2D.IsColliding())
		{
			// var enemy = _rayCast2D.GetCollider() as Enemy;
			// if (enemy != null)
			// {
			// 	enemy.TakeDamage();
			// }
			GD.Print("Hit an enemy! -- ", _rayCast2D.GetCollider().GetClass());
		}
	}

	private void HandleAnimations(Vector2 velocity)
	{
		if (_animatedSprite2D.Animation == PlayerAnimations.Whip)
		{
			return;
		}
		HandleFacingDirection(velocity);
		if (IsOnFloor())
		{
			if (Math.Abs(velocity.X) > 0)
			{
				UseWalkAnimation();
			}
			else
			{
				UseIdleAnimation();
			}
		}
		else
		{
			if (velocity.Y < 0)
			{
				UseJumpAnimation();
			}
			else
			{
				UseFallingAnimation();
			}
		}
	}

	private void HandleFacingDirection(Vector2 velocity)
	{
		if (velocity.X < 0)
		{
			_animatedSprite2D.FlipH = true;
		}
		else if (velocity.X > 0)
		{
			_animatedSprite2D.FlipH = false;
		}
	}

	private void UseFallingAnimation()
	{
		if (_animatedSprite2D.Animation == PlayerAnimations.Falling)
		{
			return;
		}
		_animatedSprite2D.Play(PlayerAnimations.Falling);
	}

	private void UseIdleAnimation()
	{
		if (_animatedSprite2D.Animation == PlayerAnimations.Idle)
		{
			return;
		}
		_animatedSprite2D.Play(PlayerAnimations.Idle);
	}

	private void UseJumpAnimation()
	{
		if (_animatedSprite2D.Animation == PlayerAnimations.Jump)
		{
			return;
		}
		_animatedSprite2D.Play(PlayerAnimations.Jump);
	}

	private void UseWalkAnimation()
	{
		if (_animatedSprite2D.Animation == PlayerAnimations.Walk)
		{
			return;
		}
		_animatedSprite2D.Play(PlayerAnimations.Walk);
	}

	private void UseWhipAnimation()
	{
		if (_animatedSprite2D.Animation == PlayerAnimations.Whip)
		{
			return;
		}
		var x = _animatedSprite2D.FlipH ? -8.0f : 8.0f;
		_animatedSprite2D.Offset = new Vector2(x, 0);
		_animatedSprite2D.Play(PlayerAnimations.Whip);
	}

	private void ResetTextureOffset()
	{
		_animatedSprite2D.Offset = new Vector2(0, 0);
	}
}

public class PlayerAnimations
{
	public static string Idle => "Idle";
	public static string Falling => "Falling";
	public static string Jump => "Jump";
	public static string Walk => "Walk";
	public static string Whip => "Whip";
}
