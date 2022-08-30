using Godot;
using System;

public class Options : Node
{   
    public override void _Ready()
    {
        var text1 = GetNode<Label>("CanvasLayer/MusicButton/Label");
        var text2 = GetNode<Label>("CanvasLayer/SFXButton/Label");


        if (Global.isMusicOn == false)
        {
            text1.Text = "OFF";
        }
        else
        {
            text1.Text = "ON";
        }

        if (Global.isSFXOn == false)
        {
            text2.Text = "OFF";
        }
        else
        {
            text2.Text = "ON";
        }
    }
    
    private void TurnMusic()
    {
        var text = GetNode<Label>("CanvasLayer/MusicButton/Label");

        if (Global.isMusicOn == false)
        {
            Global.menuMusic.Play();
            text.Text = "ON";
            Global.isMusicOn = true;
        }
        else
        {
            Global.menuMusic.Stop();
            text.Text = "OFF";
            Global.isMusicOn = false;
        }
    }

    private void TurnSFX()
    {
        var text = GetNode<Label>("CanvasLayer/SFXButton/Label");

        if (Global.isSFXOn == false)
        {
            text.Text = "ON";
            Global.isSFXOn = true;
        }
        else
        {
            text.Text = "OFF";
            Global.isSFXOn = false;
        }
    }

    private void GotoMenu()
    {
        Global.composer.GotoScene("res://MainMenu/Menu.tscn",1f);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
