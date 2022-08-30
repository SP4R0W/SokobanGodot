using Godot;
using System;

public class Composer : Node
{

    public Node CurrentScene { get; set; }

    private Vector2 center;

    private bool isEnteringNewScene = false;

    public override void _Ready()
    {
        center = new Vector2(512,384);
    }

    public void GotoScene(string newScene,float duration,bool isQuiet=false)
    {
        if (!isEnteringNewScene)
        {
            isEnteringNewScene = true;

            if (!isQuiet && Global.isSFXOn)
            {
                Global.buttonClick.Play();
            }
            CallDeferred(nameof(DefferedGotoScene),newScene,duration);
        }
    }

    private async void DefferedGotoScene(string newScene,float duration)
    {
        var root = GetTree().Root.GetNode<Node>("Area");

        if (CurrentScene != null)
        {
            var controlRoot = new CanvasLayer();
            var donut = new Donut();
            var tween = new Tween();
            
            controlRoot.PauseMode = PauseModeEnum.Process;

            var r = center.DistanceTo(new Vector2(1024,0));

            root.AddChild(controlRoot);
            controlRoot.AddChild(donut);
            donut.GlobalPosition = center;
            donut.center = Vector2.Zero;
            donut.innerRadius = r;
            donut.outerRadius = r + 10;

            controlRoot.AddChild(tween);

            tween.InterpolateProperty(donut,"innerRadius",r,0,duration/2,Tween.TransitionType.Linear,Tween.EaseType.InOut);
            tween.Start();

            await ToSignal(tween,"tween_all_completed");

            var scene = root.GetNode<Node>(CurrentScene.Name);
            scene.QueueFree();

            var nextLevel = (PackedScene)ResourceLoader.Load(newScene);
            root.AddChild(nextLevel.Instance());

            await ToSignal(GetTree().CreateTimer(0.2f),"timeout");

            tween.InterpolateProperty(donut,"innerRadius",0,r,duration/2,Tween.TransitionType.Linear,Tween.EaseType.InOut);
            tween.Start();

            await ToSignal(tween,"tween_all_completed");

            CurrentScene = root.GetChild(root.GetChildCount() - 1);
            GD.Print(CurrentScene.Name);

            controlRoot.QueueFree();
        }
        else
        {
            var nextLevel = (PackedScene)ResourceLoader.Load(newScene);
            root.AddChild(nextLevel.Instance());

            CurrentScene = root.GetChild(root.GetChildCount() - 1);          
        }

        isEnteringNewScene = false;
    }
}