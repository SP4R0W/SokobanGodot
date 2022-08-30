using Godot;
using System;

public class Player : Area2D
{
	private AnimatedSprite animSprite;

	private Tween tween;

	private RayCast2D ray;
	
	private string direction = "";

	private AudioStreamPlayer walkSound;

	private Godot.Collections.Dictionary<string,Vector2> inputs = new Godot.Collections.Dictionary<string,Vector2>()
	{
		{"walkUp", Vector2.Up},
		{"walkDown",Vector2.Down},
		{"walkLeft",Vector2.Left},
		{"walkRight",Vector2.Right}
	};

	public override void _Ready()
	{
		walkSound = GetNode<AudioStreamPlayer>("Walk");

		tween = GetNode<Tween>("Tween");
		ray = GetNode<RayCast2D>("RayCast2D");
		animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

	public override void _Process(float delta)
	{
		if (tween.IsActive())
		{
			animSprite.Play();
			return;
		}
		else
		{
			animSprite.Stop();
			animSprite.Frame = 0;
		}

		if (Global.isGameOn == true)
		{
			if (Input.IsActionPressed("walkUp"))
			{
				direction = "up";
				animSprite.Animation = "walkUp";
			}

			if (Input.IsActionPressed("walkLeft"))
			{
				direction = "left";			
				animSprite.Animation = "walkLeft";
			}

			if (Input.IsActionPressed("walkDown"))
			{
				direction = "down";
				animSprite.Animation = "walkDown";
			}

			if (Input.IsActionPressed("walkRight"))
			{
				direction = "right";
				animSprite.Animation = "walkRight";
			}

			if (!Input.IsActionPressed("walkUp") && !Input.IsActionPressed("walkDown") && !Input.IsActionPressed("walkLeft") && !Input.IsActionPressed("walkRight"))
			{
				direction = "";
			}

			foreach(string dir in inputs.Keys)
			{
				if (Input.IsActionPressed(dir))
				{
					Move(dir);
				}
			} 
		}
	}

	private void Move(string direction)
	{
		ray.CastTo = inputs[direction] * 64;
		ray.ForceRaycastUpdate();

		if (ray.IsColliding())
		{
			var collider = ray.GetCollider();

			if (((Node)collider).Name.Contains("Box") == true)
			{
				var box = (Area2D)collider;
				var boxRay = box.GetNode<RayCast2D>("RayCast2D");
				var boxTween = box.GetNode<Tween>("Tween");

				boxRay.CastTo = inputs[direction] * 64;
				boxRay.ForceRaycastUpdate();

				if (boxRay.IsColliding())
				{
					if (CheckBoxCollider(boxRay.GetCollider()) == true)
					{
						if (Global.isSFXOn)
						{
							walkSound.Play();
						}

						Global.moves++;
						Global.pushes++;

						boxTween.InterpolateProperty(box,"position",box.Position,box.Position + inputs[direction] * 64,1.0f/3f,Tween.TransitionType.Sine,Tween.EaseType.InOut);
						boxTween.Start();

						tween.InterpolateProperty(this,"position",Position,Position + inputs[direction] * 64,1.0f/3f,Tween.TransitionType.Sine,Tween.EaseType.InOut);
						tween.Start();
					}
				}
				else
				{
					if (Global.isSFXOn)
					{
						walkSound.Play();
					}

					Global.moves++;
					Global.pushes++;

					boxTween.InterpolateProperty(box,"position",box.Position,box.Position + inputs[direction] * 64,1.0f/3f,Tween.TransitionType.Sine,Tween.EaseType.InOut);
					boxTween.Start();
					tween.InterpolateProperty(this,"position",Position,Position + inputs[direction] * 64,1.0f/3f,Tween.TransitionType.Sine,Tween.EaseType.InOut);
					tween.Start();								
				}
			}
		}

		else
		{
			if (Global.isSFXOn)
			{
				walkSound.Play();
			}

			Global.moves++;

			tween.InterpolateProperty(this,"position",Position,Position + inputs[direction] * 64,1.0f/3f,Tween.TransitionType.Sine,Tween.EaseType.InOut);
			tween.Start();
		}		
	}

	private bool CheckBoxCollider(Godot.Object collider)
	{		
		if (((Node)collider).Name.Contains("Coin") == true)
		{
			var coinSound = GetNode<AudioStreamPlayer>("Coin");
			var coin = (Area2D)collider;
			coin.QueueFree();
			Global.score += 50;
			if (Global.isSFXOn)
			{
				coinSound.Play();
			}
			return true;
		}
		else if (((Node)collider).Name.Contains("SecureZone") == true)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
