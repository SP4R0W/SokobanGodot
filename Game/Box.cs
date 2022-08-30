using Godot;
using System;

public class Box : Area2D
{
	[Signal]
	public delegate void boxSecure(); 

	[Signal]
	public delegate void boxLeft();

	private string zoneType;
	private string boxType;

	public override void _Ready()
	{
		boxType = GetNode<AnimatedSprite>("AnimatedSprite").Animation;
	}

	private void ZoneEntered(Area2D area)
	{
		if (area.IsInGroup("secure"))
		{
			string zoneType = area.GetNode<AnimatedSprite>("AnimatedSprite").Animation;

			if (boxType == zoneType)
			{
				var sound = GetNode<AudioStreamPlayer>("Secure");

				EmitSignal("boxSecure");
				Modulate = new Color(0,1,0);
				if (Global.isSFXOn && Global.isGameOn)
				{
					sound.Play();
				}
			}
			else
			{
				Modulate = new Color(1,0,0);
			}
		}
	}

	private void ZoneExited(Area2D area)
	{
		if (area.IsInGroup("secure"))
		{
			EmitSignal("boxLeft");
			Modulate = new Color(1,1,1,1);
		}
	}
}
