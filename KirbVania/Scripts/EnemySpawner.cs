using Godot;
using System;
using KirbVania.Scripts;

public partial class EnemySpawner : Node2D
{
	private Timer _spawnTimer;
	private VisibleOnScreenNotifier2D _notifier;
	private PackedScene _enemyScene;
	private int _maxEnemies = 5;

	public override void _Ready()
	{
		_notifier = GetNode<VisibleOnScreenNotifier2D>("OnScreenNotifier");
		_notifier.ScreenEntered += OnScreenEntered;
		_notifier.ScreenExited += OnScreenExited;
		_enemyScene = ResourceLoader.Load<PackedScene>("res://Prefabs/skeleton.tscn");

		InitTimer();
	}

	private void OnScreenExited()
	{
		_spawnTimer.Start();
	}

	private void OnScreenEntered()
	{
		_spawnTimer.Stop();
	}

	private void OnSpawnTimerTimeout()
	{
		var enemies = GetTree().GetNodesInGroup(Groups.Enemy.ToString()).Count;
		if (!_notifier.IsOnScreen() && enemies < _maxEnemies)
		{
			SpawnSkeleton();
		}
	}

	private void SpawnSkeleton()
	{
		Skeleton skeleton = _enemyScene.Instantiate<Skeleton>();
		skeleton.Position = Position;
		GetTree().Root.AddChild(skeleton);
	}

	private void InitTimer()
	{
		_spawnTimer = GetNode<Timer>("SpawnTimer");
		_spawnTimer.Timeout += OnSpawnTimerTimeout;
		GetTree().CreateTimer(1.0).Timeout += () =>
		{
			if (!_notifier.IsOnScreen())
			{
				GD.Print("Starting enemy spawner timer...");
				_spawnTimer.Start();
			}
		};
	}
}
