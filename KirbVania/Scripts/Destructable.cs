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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup(Groups.Destructable.ToString());
	}

	public void Destroy()
	{
		SpawnPickup();
		QueueFree();
	}

	private bool ShouldSpawnPickup()
	{
		return new Random().Next(0, 100) < ChanceToDrop;
	}

	private void SpawnPickup()
	{
		if (!ShouldSpawnPickup()) return;
		var pickupScene = ResourceLoader.Load<PackedScene>("res://Prefabs/pickup.tscn");
		Pickup pickup = pickupScene.Instantiate<Pickup>();
		pickup.Position = Position;
		pickup.Scale = new Vector2(0.75f, 0.75f);
		GetParent().AddChild(pickup);
	}
}
