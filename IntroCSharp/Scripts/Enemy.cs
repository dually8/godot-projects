using Godot;
using System;

public partial class Enemy : Area2D
{
	// https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_exports.html#doc-c-sharp-exports
	[ExportGroup("Movement")]
	[Export]
	private float _speed = 100.0f;
	[Export]
	private float _moveDistance = 100.0f;

	private float _startX;
	private float _targetX;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_startX = Position.X;
		_targetX = _startX + _moveDistance;
		BodyEntered += OnBodyEntered;
		GD.Print("Enemy ready!");
	}

	public override void _Process(double delta)
	{
		var xPos = MoveTo(Position.X, _targetX, _speed * (float)delta);
		if (xPos == _targetX)
		{
			if (_targetX == _startX)
			{
				_targetX = _startX + _moveDistance;
			}
			else
			{
				_targetX = _startX;
			}
		}
		Position = new Vector2(xPos, Position.Y);
	}

	public float MoveTo(float current, float to, float step)
	{
		var temp = current;
		if (temp < to)
		{
			temp += step;
			if (temp > to)
				temp = to;
		}
		else
		{
			temp -= step;
			if (temp < to)
				temp = to;
		}

		return temp;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Character character)
		{
			character.Die();
		}
	}
}


