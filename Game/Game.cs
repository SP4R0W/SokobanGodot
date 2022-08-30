using Godot;
using System;

public class Game : Node
{
	private Label timeText;
	private Label scoreText;
	private Label triesText;
	private Label moveText;
	private Label pushText;
	private Label levelText;

	private AudioStreamPlayer music;

	public override void _Ready()
	{
		Global.isGameOn = true;

		music = GetNode<AudioStreamPlayer>("GameMusic");

		timeText = GetNode<Label>("UI/TimeText");
		scoreText = GetNode<Label>("UI/ScoreText");
		triesText = GetNode<Label>("UI/TriesText");
		levelText = GetNode<Label>("UI/LevelText");
		moveText = GetNode<Label>("UI/MoveText");
		pushText = GetNode<Label>("UI/PushText");

		Global.score = 0;
		Global.time = 0;
		Global.moves = 0;
		Global.pushes = 0;

		for (int i = 0;i<GetChildCount();i++)
		{
			var child = GetChild(i);
			if (((Node)child).Name.Contains("Level") == true)
			{
				child.QueueFree();
			}
		}

		LoadLevel(Global.level);
		
		if (Global.isMusicOn)
		{
			music.Play();
		}
	}

	private void GotoMenu()
	{
		music.Stop();
		Global.composer.GotoScene("res://MainMenu/LevelSelect.tscn",1f);
	}

	private void AddTime()
	{
		Global.time++;
	}

	private async void FinishGame()
	{
		Global.score += 100;
		Global.isGameOn = false;
		await ToSignal(GetTree().CreateTimer(1),"timeout");
		music.Stop();
		Global.composer.GotoScene("res://MainMenu/GameOver.tscn",1f);
	}

	private void LoadLevel(string level)
	{
		levelText.Text = "Level: " + level;

		string levelName = "res://Game/Levels/Level" + level + ".tscn";
		PackedScene levelScenePath = (PackedScene)ResourceLoader.Load(levelName);
		var levelScene = levelScenePath.Instance();
		AddChild(levelScene);

		levelScene.Connect("GameOver",this,"FinishGame");
	}

	public override void _Process(float delta)
	{
		timeText.Text = "Time: " + Global.time;
		scoreText.Text = "Score: " + Global.score;
		triesText.Text = "Tries: " + Global.tries;
		moveText.Text = "Moves: " + Global.moves;
		pushText.Text = "Push: " + Global.pushes;

		if (Input.IsActionJustPressed("reset"))
		{
			Global.score = 0;
			Global.time = 0;
			Global.moves = 0;
			Global.pushes = 0;
			Global.tries++;
			Global.composer.GotoScene("res://Game/Game.tscn",0f,true);
		}
	}
}
