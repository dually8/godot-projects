using Godot;
using System;
using System.Linq;
using KirbVania.Scripts;

public partial class Destructable : Area2D
{
	/// <summary>
	/// Percentage chance to drop a pickup.
	/// </summary>
	private const int ChanceToDrop = 50;

	private AudioStreamPlayer2D _hitAudio;
	private PackedScene _pickupScene;
	private bool _isDestroyed = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup(Groups.Destructable.ToString());
		_pickupScene = ResourceLoader.Load<PackedScene>("res://Prefabs/pickup.tscn");
		_hitAudio = GetNode<AudioStreamPlayer2D>("HitSFX");
		_hitAudio.Finished += OnHitAudioFinished;
	}

	private void OnHitAudioFinished()
	{
		QueueFree();
	}

	public void Destroy()
	{
		if (_isDestroyed) return;
		_isDestroyed = true;
		Visible = false;
		// CallDeferred is used to ensure the physics state is updated before spawning the pickup.
		CallDeferred(nameof(SpawnPickup));
		// Side effect; destroys the node on audio finished
		_hitAudio.Play();
	}

	private bool ShouldSpawnPickup()
	{
		return new Random().Next(0, 100) < ChanceToDrop;
	}

	private void SpawnPickup()
	{
		if (!ShouldSpawnPickup()) return;
		Pickup pickup = _pickupScene.Instantiate<Pickup>();
		pickup.Position = Position;
		pickup.Scale = new Vector2(0.75f, 0.75f);
		GetParent().AddChild(pickup);
	}
}
