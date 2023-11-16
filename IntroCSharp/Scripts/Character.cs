using Godot;
using System;

// https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_differences.html
/**
 * Directions
 * x-				x+
 * ------------
 * |					| y-
 * |					|
 * |					| y+
 * ------------
 */
public partial class Character : CharacterBody2D
{
	private int _score = 0;
	private Sprite2D _sprite;
	private HUD _hud;
	private AudioPlayer _audioPlayer;


	private const float Speed = 200.0f;
	private const float JumpForce = -600.0f; // Negative is up
	private const float Gravity = 800.0f; // Positive is down

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite");
		_hud = GetNode<HUD>("/root/Main/UI/HUD");
		_audioPlayer = GetNode<AudioPlayer>("%AudioPlayer");
		GD.Print("Character ready!");
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
		if (Input.IsActionJustPressed("quit"))
			GetTree().Quit();
	}

	public void Die()
	{
		GD.Print("You died!");
		GetTree().ReloadCurrentScene();
	}

	public void CollectCoin(int value)
	{
		_score += value;
		_hud.SetScore(_score);
		_audioPlayer.PlayCoinSound();
		GD.Print($"Score: {_score}");
	}

	private void HandleMovement(double delta)
	{
		var velocity = Velocity;
		if (!IsOnFloor())
			velocity.Y += Gravity * (float)delta;
		else if (Input.IsActionJustPressed("jump"))
			velocity.Y = JumpForce;

		// Handle left and right movement
		var direction = Input.GetAxis("move_left", "move_right"); // -1 is left, 1 is right
		velocity.X = direction * Speed;
		// Flip the sprite when moving directions
		HandleSpriteDirection(velocity);

		Velocity = velocity;
		MoveAndSlide();
	}

	private void HandleSpriteDirection(Vector2 velocity)
	{
		if (velocity.X < 0)
			_sprite.FlipH = true;
		else if (velocity.X > 0)
			_sprite.FlipH = false;
	}
}
