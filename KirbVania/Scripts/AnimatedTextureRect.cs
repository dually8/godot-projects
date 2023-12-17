using Godot;
using System;
using System.Diagnostics;

public partial class AnimatedTextureRect : TextureRect
{
	[Export] private SpriteFrames _spriteFrames { get; set; }
	[Export] private string _currentAnimation { get; set; } = "default";
	[Export] private int _frameIndex { get; set; }
	[Export] private float _speedScale { get; set; } = 1.0f;
	[Export] private bool _autoPlay { get; set; }
	[Export] private bool _isPlaying { get; set; }
	private double _refreshRate = 1.0;
	private double _fps = 30.0;
	private double _frameDelta = 0.0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_fps = _spriteFrames.GetAnimationSpeed(_currentAnimation);
		_refreshRate = _spriteFrames.GetFrameDuration(_currentAnimation, _frameIndex);
		if (_autoPlay)
		{
			Play(_currentAnimation);
		}
	}

	public void Play(string animationName)
	{
		_currentAnimation = animationName;
		_frameIndex = 0;
		_frameDelta = 0.0;
		GetAnimationData();
		_isPlaying = true;
	}

	private void GetAnimationData()
	{
		_refreshRate = _spriteFrames.GetFrameDuration(_currentAnimation, _frameIndex);
		_fps = _spriteFrames.GetAnimationSpeed(_currentAnimation);
	}

	public void Resume()
	{
		_isPlaying = true;
	}

	public void Pause()
	{
		_isPlaying = false;
	}

	public void Stop()
	{
		_frameIndex = 0;
		_isPlaying = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_spriteFrames == null || !_isPlaying)
		{
			return;
		}

		if (!_spriteFrames.HasAnimation(_currentAnimation))
		{
			_isPlaying = false;
			Debug.Assert(false, "Animation \"" + _currentAnimation + "\" does not exist.");
			return;
		}

		GetAnimationData();
		_frameDelta += _speedScale * delta;
		if (_frameDelta >= (_refreshRate / _fps))
		{
			Texture = GetNextFrame();
			_frameDelta = 0.0;
		}
	}

	private Texture2D GetNextFrame()
	{
		_frameIndex++;
		var frameCount = _spriteFrames.GetFrameCount(_currentAnimation);
		if (_frameIndex >= frameCount)
		{
			if (!_spriteFrames.GetAnimationLoop(_currentAnimation))
			{
				_isPlaying = false;
				_frameIndex = frameCount - 1;
			}
			else
			{
				_frameIndex = 0;
			}
		}

		GetAnimationData();
		return _spriteFrames.GetFrameTexture(_currentAnimation, _frameIndex);
	}
}
