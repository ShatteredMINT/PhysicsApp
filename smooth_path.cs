using Godot;
using System;

[Tool]
public partial class smooth_path : Path2D
{
	private bool _smooth = false;
	private bool _straighten = false;

	[Export]
	public int LineThickness = 5;

	[Export]
	public int SplineLength = 100;

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
			DrawPolyline(points, Colors.Black, LineThickness, true);
		}


		/*for (int i = 0; i < Curve.PointCount; i++) {
			DrawCircle(Curve.GetPointPosition(i), LineThickness, Colors.Red);
		}*/
	}


	private Vector2 getSpline(int index)
	{
		Vector2 last = getPoint(index - 1);
		Vector2 next = getPoint(index + 1);
		Vector2 spline = last.DirectionTo(next) * Mathf.Max(SplineLength * ((next - last).Length() / 100), 5);
		return spline;
	}

	private Vector2 getPoint(int index)
	{
		int count = Curve.PointCount;
		index = Mathf.Wrap(index, 0, count);
		return Curve.GetPointPosition(index);
	}
}
