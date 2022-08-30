using Godot;
using System;

public class Background : CanvasLayer
{

    private static string ImagePath = "res://UI/Images/background.png";

    private static Vector2 velocity = new Vector2(100,100);

    private static bool isRunning = true;

    public override void _Ready()
    {
        
    }

    public void ChangeSpeed(Vector2 newVel)
    {
        velocity = newVel;
    }

    public void ChangeRun(bool shouldRun)
    {
        isRunning = shouldRun;
    }

    public void ChangeImage(string newPath)
    {
        var sprite = GetNode<Sprite>("ParallaxBackground/ParallaxLayer/Sprite");
        ImagePath = newPath;

        sprite.Texture = (Texture)ResourceLoader.Load(newPath);
    }

    public override void _Process(float delta)
    {
        if (isRunning == true)
        {
            var bg = GetNode<ParallaxBackground>("ParallaxBackground");
            bg.ScrollOffset += velocity * delta;
        }
    }
}
