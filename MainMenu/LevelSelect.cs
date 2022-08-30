using Godot;
using System;

public class LevelSelect : Node
{

    private int level = 1;

    private TextureButton prevButton;
    private TextureButton nextButton;

    private Label levelText;

    public override void _Ready()
    {
        Global.isGameOn = false;

        level = 1;
        prevButton = GetNode<TextureButton>("CanvasLayer/PrevButton");
        nextButton = GetNode<TextureButton>("CanvasLayer/NextButton");
        levelText = GetNode<Label>("CanvasLayer/Label3");

        prevButton.Hide();
        nextButton.Show();

        LoadPreview();

        if (Global.isMusicOn && !Global.menuMusic.Playing)
        {
            Global.menuMusic.Play();
        }
    }

    private void LoadPreview()
    {
        var cv = GetNode<CanvasLayer>("CanvasLayer");
        for (int i = 0;i<cv.GetChildCount();i++)
        {
            var child = cv.GetChild(i);
            if (child.Name.Contains("Level") == true)
            {
                child.QueueFree();
                break;
            }
        }

        var levelScenePath = "res://Game/Levels/Level" + level + ".tscn";
        var levelPackedScene = (PackedScene)ResourceLoader.Load(levelScenePath);
        var levelScene = (TileMap)levelPackedScene.Instance();

        cv.AddChild(levelScene);
        levelScene.Scale = new Vector2(0.3f,0.3f);
        levelScene.GlobalPosition = new Vector2(360,240);
    }

    private void PrevLevel()
    {
        level--;
        if (level == 1)
        {
            prevButton.Hide();
        }
        else
        {
            prevButton.Show();
        }

        nextButton.Show();

        levelText.Text = "Level: " + level;
        LoadPreview();
    }

    private void NextLevel()
    {
        level++;
        if (level == 10)
        {
            nextButton.Hide();
        }
        else
        {
            nextButton.Show();
        }

        prevButton.Show();

        levelText.Text = "Level: " + level;
        LoadPreview();
    }

    private void GotoGame()
    {
        Global.menuMusic.Stop();
        Global.level = Convert.ToString(level);
        Global.tries = 1;
        Global.composer.GotoScene("res://Game/Game.tscn",2f);
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
