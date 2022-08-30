using Godot;
using System;

public class Global : Node
{

    public static Background background;

    public static Composer composer;

    public static AudioStreamPlayer menuMusic;
    public static AudioStreamPlayer buttonClick;
    
    public static bool isMusicOn = true;
    public static bool isSFXOn = true;

    public static int time = 0;
    public static int tries = 1;
    public static int score = 0;

    public static int moves = 0;
    public static int pushes = 0;

    public static string level = "1";

    public static bool isGameOn = false;

    public override void _Ready()
    {
        isGameOn = false;
        composer = (Composer)GetNode("/root/Composer");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
