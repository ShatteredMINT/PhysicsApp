using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class smooth_path : Path2D
{
	private bool _smooth = false;
	private bool _straighten = false;

	private List<Sprite2D> arrows = new List<Sprite2D>();

	[Export]
	public int LineThickness = 5;

	[Export]
	public int SplineLength = 100;

	[Export]
	public bool ShowPoints = false;

	[Export]
	public PackedScene Arrow;

	[Export]
	public bool Smooth
	{
		get => _smooth;

		set
		{
			if (!value)
				return;

			int count = Curve.PointCount;
			for (int i = 0; i < count; i++) {
				Vector2 spline = getSpline(i);
				Curve.SetPointIn(i, -spline);
				Curve.SetPointOut(i, spline);
			}
		}
	}

	[Export]
	public bool Straighten
	{
		get => _straighten;

		set
		{
			if (!value)
				return;

			int count = Curve.PointCount;
			for (int i = 0; i < count; i++) {
				Curve.SetPointIn(i, new Vector2());
				Curve.SetPointOut(i, new Vector2());
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Draw() {
		Vector2[] points = Curve.GetBakedPoints();
		if (points != null) {
			DrawPolyline(points, Colors.Green, LineThickness, true);
		}

		if (ShowPoints) {
			for (int i = 0; i < Curve.PointCount; i++) {
				DrawCircle(Curve.GetPointPosition(i), LineThickness, Colors.Red);
			}
		}
	}

	public void AddArrows()
	{
		for (int i = 0; i < Curve.PointCount; i++) {
			float angle = getSpline(i).Angle();

			Sprite2D arrow = Arrow.Instantiate<Sprite2D>();

			arrow.Position = getPoint(i);
			arrow.Rotation = angle;

			arrows.Add(arrow);
			AddChild(arrow);
		}
	}

	private Vector2 getSpline(int index)
	{
		Vector2 last = getPoint(index - 1);
		Vector2 next = getPoint(index + 1);
		Vector2 spline = last.DirectionTo(next) * SplineLength *  (index == 2 ? 3 : 1) * ((next - last).Length() > 600 ? 2 : 1);
		return spline;
	}

	private Vector2 getPoint(int index)
	{
		int count = Curve.PointCount;
		index = Mathf.Wrap(index, 0, count);
		return Curve.GetPointPosition(index);
	}

	public void TreeExit()
	{
		while (arrows.Count > 0) {
			arrows[0].QueueFree();
			arrows.RemoveAt(0);
		}
	}
}
