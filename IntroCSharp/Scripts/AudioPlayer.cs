using Godot;
using System;

public partial class AudioPlayer : AudioStreamPlayer2D
{
	[Export]
	private AudioStream _coinSound { get; set; }

	public void PlayCoinSound()
	{
		Stream = _coinSound;
		Play();
	}
}
