using Godot;
using System;
using System.Linq;

public partial class Skeleton : CharacterBody2D
{
	private AnimatedSprite2D _sprite;
	private Vector2 _walkingDirection = Vector2.Left;
	private float _speed = 50.0f;

	public override void _Ready()
	{
		GD.Print("Skeleton ready!");
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_sprite.AnimationFinished += OnAnimatedFinished;
		_sprite.Play(SkeletonAnimations.Walk);
		if (GetTree().GetNodesInGroup("Player").FirstOrDefault() is Player player)
		{
			_walkingDirection = (player.Position - Position).Normalized();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = _walkingDirection * _speed;
		MoveAndSlide();
	}

	private void OnAnimatedFinished()
	{
		if (_sprite.Animation == SkeletonAnimations.Die)
		{
			QueueFree();
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
