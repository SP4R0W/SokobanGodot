using Godot;
using System;

public class Level8 : TileMap
{

    [Signal]
    public delegate void GameOver();

    private int totalBoxes;
    private int boxesSecured = 0;

    public override void _Ready()
    {
        boxesSecured = 0;

        for (int i = 0;i<GetChildCount();i++)
        {
            var child = GetChild(i);
            if (((Node)child).Name.Contains("Box") == true)
            {
                totalBoxes++;
            }
        }
    }

    private void BoxSecure()
    {
        boxesSecured++;
        if (boxesSecured == totalBoxes)
        {
            Global.score += totalBoxes * 100;

            EmitSignal("GameOver");
        }
    }

    private void BoxLeft()
    {
        boxesSecured--;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
