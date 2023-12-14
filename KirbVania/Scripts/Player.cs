using Godot;
using System;
using KirbVania.Scripts;

public partial class Player : CharacterBody2D
{
	# region Signals

	[Signal]
	public delegate void IncreaseScoreEventHandler(int amount);

	[Signal]
	public delegate void DeathEventHandler();

	# endregion Signals

	# region Export Vars

	[Export] private AnimatedSprite2D _animatedSprite2D { get; set; }
	[Export] private Camera2D _camera { get; set; }

	# endregion Export Vars

	# region Public Vars

	public const float Speed = 100.0f;
	public const float JumpVelocity = -300.0f;

	public float AttackDistance => _animatedSprite2D.FlipH ? -16.0f : 16.0f;

	# endregion Public Vars

	# region Private Vars

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private GameManager _gameManager;
	private Area2D _whipHitBox;
	private CollisionShape2D _whipCollision;
	private AudioStreamPlayer2D _jumpAudio;
	private AudioStreamPlayer2D _landAudio;
	private AudioStreamPlayer2D _whipAudio;
	private AudioStreamPlayer2D _hurtAudio;
	private PackedScene _hitSpriteScene;

	private Vector2 _knockbackVelocity = Vector2.Zero;
	private bool _isAttacking = false;
	private int _hp = 6;
	private int _maxHp = 6;
	private bool _wasOnFloor = true;
	private bool _isKnockedBack = false;

	# endregion Private Vars

	# region Built In Methods

	public override void _Ready()
	{
		AddToGroup(Groups.Player.ToString());
		UseIdleAnimation();
		_jumpAudio = GetNode<AudioStreamPlayer2D>("JumpAudio");
		_landAudio = GetNode<AudioStreamPlayer2D>("LandAudio");
		_whipAudio = GetNode<AudioStreamPlayer2D>("WhipAudio");
		_hurtAudio = GetNode<AudioStreamPlayer2D>("HurtAudio");
		_animatedSprite2D.AnimationFinished += OnAnimatedFinished;
		_camera.Enabled = true;
		_camera.MakeCurrent();
		_gameManager = GetNode<GameManager>("/root/Main");
		_whipHitBox = GetNode<Area2D>("WhipHitBox");
		_whipHitBox.AddToGroup(Groups.Weapon.ToString());
		_whipHitBox.BodyEntered += OnWhipCollision;
		_whipHitBox.AreaEntered += OnWhipOverlap;
		_whipCollision = _whipHitBox.GetNode<CollisionShape2D>("WhipCollision");
		DisableWhip();
		_hitSpriteScene = ResourceLoader.Load<PackedScene>("res://Prefabs/hit_sprite.tscn");
	}

	private void OnWhipOverlap(Area2D area)
	{
		if (area is Destructable destructable)
		{
			SpawnHitSprite(area);
			destructable.Destroy();
			EmitSignal(SignalName.IncreaseScore, 50);
		}
	}

	private void OnWhipCollision(Node2D body)
	{
		if (body is Skeleton skeleton)
		{
			SpawnHitSprite(body);
			skeleton.Destroy();
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
		HandleMovement((float)delta);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed(InputActions.Attack))
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
		// Don't take damage while knocked back.
		// iframes baby
		if (_isKnockedBack) return;
		_hp = Math.Max(_hp - amount, 0); // Don't go below 0.
		_hurtAudio.Play();
		FlashHurt();
		_gameManager.UpdateHealth(_hp);
		if (_hp <= 0)
		{
			Die();
		}
	}

	public void ApplyKnockback(Vector2 direction, float force = 100.0f)
	{
		_isKnockedBack = true;
		_knockbackVelocity = new Vector2(direction.X, -1).Normalized() * force;
	}

	private void FlashHurt()
	{
		_animatedSprite2D.Modulate = new Color(1, 0, 0);
		GetTree().CreateTimer(0.1f).Timeout += () => { _animatedSprite2D.Modulate = new Color(1, 1, 1); };
	}

	public void Heal(int amount)
	{
		_hp = Math.Min(_hp + amount, _maxHp); // Don't go above max HP.
		_gameManager.UpdateHealth(_hp);
	}

	private void Die()
	{
		EmitSignal(SignalName.Death);
	}

	#endregion

	# region Private Methods

	private void SpawnHitSprite(Node2D node)
	{
		// Instantiate the hit sprite scene
		HitSprite hitSprite = _hitSpriteScene.Instantiate<HitSprite>();

		// Set the position of the hit sprite to the overlap point
		hitSprite.Position = (_whipHitBox.GlobalPosition + node.GlobalPosition) / 2;

		// Add the hit sprite to the scene tree
		GetNode("/root/Main").AddChild(hitSprite);
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
			DisableWhip();
			_isAttacking = false;
		}
	}


	private void HandleMovement(float delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += _gravity * delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed(InputActions.Jump) && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			// Play jump sound
			_jumpAudio.Play();
		}

		// Handle Knockback
		if (_isKnockedBack)
		{
			// Use 500.0f here for floating; regular gravity is too harsh
			_knockbackVelocity.Y += 500.0f * delta;
			velocity = _knockbackVelocity;
			ApplyMovement(velocity);
			if (IsOnFloor())
			{
				_isKnockedBack = false;
				_knockbackVelocity = Vector2.Zero;
			}
		}
		else
		{
			// Handle horizontal movement
			var direction = Input.GetAxis(InputActions.MoveLeft, InputActions.MoveRight);
			velocity.X = _isAttacking && IsOnFloor() ? 0 : direction * Speed;
		}

		HandleAnimations(velocity);

		if (IsOnFloor() && !_wasOnFloor)
		{
			// Play land sound
			_landAudio.Play();
		}

		_wasOnFloor = IsOnFloor();

		ApplyMovement(velocity);
	}

	private void ApplyMovement(Vector2 velocity)
	{
		Velocity = velocity;
		MoveAndSlide();
	}

	private void TryAttack()
	{
		_whipHitBox.Position = new Vector2(AttackDistance, _whipHitBox.Position.Y);
		EnableWhip();
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
		if (_isKnockedBack) return;
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
		_whipAudio.Play();
	}

	private void ResetTextureOffset()
	{
		_animatedSprite2D.Offset = new Vector2(0, 0);
	}

	private void DisableWhip()
	{
		_whipCollision.Disabled = true;
	}

	private void EnableWhip()
	{
		_whipCollision.Disabled = false;
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
