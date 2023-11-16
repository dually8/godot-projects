using Godot;
using System;

public partial class CameraController : Camera2D
{
	[Export]
	// You need the { get; set; } in order for the inspector to show the property
	private Character _character { get; set; }

	public override void _Process(double delta)
	{
		Position = new Vector2(_character.Position.X, Position.Y);
	}
}
