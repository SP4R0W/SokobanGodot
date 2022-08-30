using Godot;
using System;

public class Donut : Node2D
{

    public Vector2 center;

    public float innerRadius;
    public float outerRadius;
    public Color color;
    

    public override void _Ready()
    {
        center = new Vector2(512,384);
        innerRadius = 32;
        outerRadius = 64;
        color = new Color(63/255f, 165/255f, 224/255f);
    }

    public override void _Draw()
    {
        DrawDonut();
    }

    public override void _PhysicsProcess(float delta)
    {
        Update();
    }

    private void DrawDonut()
    {
        var nbPoints = 32;
        var points = new System.Collections.Generic.List<Vector2>();
        Color[] colors = {color};

        for (int i = 0;i<nbPoints+1;i++)
        {
            var anglePoint = (i * 360) / nbPoints - 35;
            points.Add(center + new Vector2(Mathf.Cos(Mathf.Deg2Rad(anglePoint)),Mathf.Sin(Mathf.Deg2Rad(anglePoint))) * outerRadius);
        }

        for (int i = nbPoints;i>-1;i--)
        {
            var anglePoint = (i * 360) / nbPoints - 35;
            points.Add(center + new Vector2(Mathf.Cos(Mathf.Deg2Rad(anglePoint)),Mathf.Sin(Mathf.Deg2Rad(anglePoint))) * innerRadius);
        }

        Vector2[] pointsArc = points.ToArray();

        DrawPolygon(pointsArc,colors);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
