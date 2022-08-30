using Godot;
using System;

public class GameOver : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (Global.isMusicOn)
        {
            Global.menuMusic.Play();
        }

        Global.isGameOn = false;

        var score = GetNode<Label>("CanvasLayer/ScoreText");
        score.Text = "Total score: " + Global.score;

        var time = GetNode<Label>("CanvasLayer/TimeText");
        time.Text = "Time spent: " + Global.time;

        var retry = GetNode<Label>("CanvasLayer/RetryText");
        retry.Text = "Tries: " + Global.tries;

        var move = GetNode<Label>("CanvasLayer/MoveText");
        move.Text = "Total moves: " + Global.moves;

        var push = GetNode<Label>("CanvasLayer/PushText");
        push.Text = "Total pushes: " + Global.pushes;
    }

    private void GotoLevel()
    {
        Global.menuMusic.Stop();
        Global.composer.GotoScene("res://Game/Game.tscn",1f);
    }

    private void GotoNextLevel()
    {
        Global.menuMusic.Stop();
        Global.level = Convert.ToString(Convert.ToInt32(Global.level) + 1);
        if (Global.level == "11")
        {
            Global.level = "1";
        }
        Global.composer.GotoScene("res://Game/Game.tscn",1f);
    }
    private void GotoMenu()
    {
        Global.composer.GotoScene("res://MainMenu/LevelSelect.tscn",1f);
    }
}
