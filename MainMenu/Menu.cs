using Godot;
using System;

public class Menu : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override async void _Ready()
    {
        var label = GetNode<Label>("CanvasLayer/Label");
        var tween = GetNode<Tween>("CanvasLayer/Label/Tween");
        var sound = GetNode<AudioStreamPlayer>("Slide");
        tween.InterpolateProperty(label,"rect_position:y",-500,0,2,Tween.TransitionType.Bounce,Tween.EaseType.InOut);
        tween.Start();

        await ToSignal(GetTree().CreateTimer(1),"timeout");

        if (Global.isSFXOn)
        {
            sound.Play();
        }
    }

    private void GotoPlay()
    {
        ActivateMenu();
        Global.composer.GotoScene("res://MainMenu/LevelSelect.tscn",1f);
    }

    private void GotoOptions()
    {
        ActivateMenu();
        Global.composer.GotoScene("res://MainMenu/Options.tscn",1f);
    }

    private void GotoCredits()
    {
        ActivateMenu();
        Global.composer.GotoScene("res://MainMenu/Credits.tscn",1f);
    }

    public override void _Process(float delta)
    {
        Tween tween = GetNode<Tween>("CanvasLayer/Label/Tween");
        if (!tween.IsActive())
        {
            ActivateMenu();
        }
    }

    private void ActivateMenu()
    {
        if (Global.isMusicOn && !Global.menuMusic.Playing)
        {
            Global.menuMusic.Play();
        }      

        Global.background.ChangeRun(true);
    }
}
