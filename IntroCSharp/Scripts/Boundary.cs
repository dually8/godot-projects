using Godot;
using System;

public partial class Boundary : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	public void OnBodyEntered(Node body)
	{
		if (body is Character character)
		{
			character.Die();
		}
	}
}
