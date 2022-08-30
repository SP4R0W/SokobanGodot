using Godot;
using System;

public class Area : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Global.background = (Background)GetNode<CanvasLayer>("Background");
		Global.background.ChangeRun(false);
		Global.menuMusic = GetNode<AudioStreamPlayer>("MenuMusic");
		Global.buttonClick = GetNode<AudioStreamPlayer>("ButtonClick");
		Global.composer.GotoScene("res://MainMenu/Menu.tscn",0f,true);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
