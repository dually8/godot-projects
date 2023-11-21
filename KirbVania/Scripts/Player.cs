using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150.0f;
	public const float JumpVelocity = -300.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	[Export]
	private AnimatedSprite2D _animatedSprite2D { get; set; }

	public override void _Ready()
	{
		GD.Print("Ready!");
		UseIdleAnimation();
		_animatedSprite2D.AnimationLooped += OnAnimationLooped;
	}

	private void OnAnimationLooped()
	{
		GD.Print("Animation looped");
		GD.Print("Animation: " + _animatedSprite2D.Animation);
		if (_animatedSprite2D.Animation == PlayerAnimations.Jump && !IsOnFloor())
		{
			_animatedSprite2D.Play(PlayerAnimations.Falling);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
			GetTree().Quit();

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

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		var direction = Input.GetAxis("move_left", "move_right");
		velocity.X = direction * Speed;

		HandleAnimations(velocity);

		Velocity = velocity;
		MoveAndSlide();
	}

	private void HandleAnimations(Vector2 velocity)
	{
		if (IsOnFloor())
		{
			if (Math.Abs(velocity.X) > 0)
			{
				UseWalkAnimation();
				_animatedSprite2D.FlipH = velocity.X < 0;
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

	private void UseFallingAnimation()
	{
		_animatedSprite2D.Play(PlayerAnimations.Falling);
	}

	private void UseIdleAnimation()
	{
		_animatedSprite2D.Play(PlayerAnimations.Idle, 0.5f);
	}

	private void UseJumpAnimation()
	{
		_animatedSprite2D.Play(PlayerAnimations.Jump);
	}

	private void UseWalkAnimation()
	{
		_animatedSprite2D.Play(PlayerAnimations.Walk);
	}
}

public class PlayerAnimations
{
	public static string Idle { get; } = "Idle";
	public static string Falling { get; } = "Falling";
	public static string Jump { get; } = "Jump";
	public static string Walk { get; } = "Walk";
	public static string Whip { get; } = "Whip";
}
