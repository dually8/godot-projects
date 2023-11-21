using Godot;
using System;

public partial class Skeleton : CharacterBody2D
{
	public override void _Ready()
	{
		GD.Print("Skeleton ready!");
	}
}
