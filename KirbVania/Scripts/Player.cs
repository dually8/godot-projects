using Godot;
using System;
using KirbVania.Scripts;

public partial class Player : CharacterBody2D
{
	# region Signals

	[Signal]
	public delegate void IncreaseScoreEventHandler(int amount);

	# endregion Signals

	# region Export Vars

	[Export] private AnimatedSprite2D _animatedSprite2D { get; set; }
	[Export] private Camera2D _camera { get; set; }

	# endregion Export Vars

	# region Public Vars

	public const float Speed = 100.0f;
	public const float JumpVelocity = -300.0f;

	// public float AttackDistance => _animatedSprite2D.FlipH ? -26.0f : 26.0f;
	public float AttackDistance => _animatedSprite2D.FlipH ? -16.0f : 16.0f;

	# endregion Public Vars

	# region Private Vars

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private RayCast2D _attackRayCast;
	private GameManager _gameManager;
	private Area2D _whipHitBox;

	private bool _isAttacking = false;
	private int _hp = 6;
	private int _maxHp = 6;

	# endregion Private Vars

	# region Built In Methods

	public override void _Ready()
	{
		GD.Print("Ready!");
		UseIdleAnimation();
		_animatedSprite2D.AnimationFinished += OnAnimatedFinished;
		_camera.Enabled = true;
		_camera.MakeCurrent();
		_gameManager = GetNode<GameManager>("/root/Main");
		_whipHitBox = GetNode<Area2D>("WhipHitBox");
		_whipHitBox.BodyEntered += OnWhipCollision;
		_whipHitBox.AreaEntered += OnWhipOverlap;
		_attackRayCast = GetNode<RayCast2D>("AttackRayCast");
		DisableWhip();
	}

	private void OnWhipOverlap(Area2D area)
	{
		GD.Print("Whip overlap " + area.Name);
		if (area.IsInGroup("Destroyable"))
		{
			area.QueueFree();
			EmitSignal(SignalName.IncreaseScore, 100);
		}
	}

	private void OnWhipCollision(Node2D body)
	{
		GD.Print("Whip hit " + body.Name);
		if (body is Skeleton skeleton)
		{
			skeleton.Die();
			EmitSignal(SignalName.IncreaseScore, 100);
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
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("attack"))
		{
			_isAttacking = true;
			TryAttack();
			UseWhipAnimation();
		}
	}

	# endregion Built In Methods

	#region Public Methods

	public void TakeDamage(int amount)
	{
		_hp = Math.Max(_hp - amount, 0); // Don't go below 0.
		// TODO: Player hurt sound
		_gameManager.UpdateHealth(_hp);
		GD.Print("Took " + amount + " damage!");
		GD.Print("HP is now " + _hp);
		if (_hp <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// TODO: Display game over screen
		GetTree().ReloadCurrentScene();
	}

	#endregion

	# region Private Methods

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
			DisableWhip();
			_isAttacking = false;
		}
	}


	private void HandleMovement(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += _gravity * (float)delta;
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

	private void TryAttack()
	{
		_whipHitBox.Position = new Vector2(AttackDistance, _whipHitBox.Position.Y);
		EnableWhip();
		// _attackRayCast.Position = new Vector2(_animatedSprite2D.FlipH ? -8.0f : 8.0f, _attackRayCast.Position.Y);
		// _attackRayCast.TargetPosition = new Vector2(AttackDistance, _attackRayCast.Position.Y);
		// _attackRayCast.ForceRaycastUpdate();
		// if (_attackRayCast.IsColliding())
		// {
		// 	var collider = _attackRayCast.GetCollider() as Node2D;
		// 	// if (collider is Skeleton skeleton)
		// 	// {
		// 	// 	skeleton.Die();
		// 	// 	EmitSignal(SignalName.IncreaseScore, 100);
		// 	// }
		// 	GD.Print("Collided with " + collider.Name);
		// 	/*else*/ if (collider != null && collider.IsInGroup("Destroyable"))
		// 	{
		// 		collider.QueueFree();
		// 		EmitSignal(SignalName.IncreaseScore, 100);
		// 	}
		// }
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

	private void DisableWhip()
	{
		GD.Print("Disable whip");
		_whipHitBox.SetCollisionMaskValue((int)Layers.Destructable, false);
		_whipHitBox.SetCollisionMaskValue((int)Layers.Enemy, false);
	}
	private void EnableWhip()
	{
		GD.Print("Enable whip");
		_whipHitBox.SetCollisionMaskValue((int)Layers.Destructable, true);
		_whipHitBox.SetCollisionMaskValue((int)Layers.Enemy, true);
	}

	# endregion Private Methods
}

public static class PlayerAnimations
{
	public static string Idle => "Idle";
	public static string Falling => "Falling";
	public static string Jump => "Jump";
	public static string Walk => "Walk";
	public static string Whip => "Whip";
}

// Layer 1 = Level
// 2 = Enemy
// 3 = Destructable
// 4 = Pickup
// 5 = Player
// 6 = Weapon
