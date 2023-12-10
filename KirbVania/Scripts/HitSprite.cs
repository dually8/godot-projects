using Godot;
using System;

public partial class HitSprite : Sprite2D
{
	private Timer _timer;

	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimerTimeout;
	}

	private void OnTimerTimeout()
	{
		QueueFree();
	}
}
