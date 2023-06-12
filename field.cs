using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class field : Node2D
{
	private int _count = 0;
	private List<smooth_path> lines = new List<smooth_path>();
	private Vector2 _size = new Vector2(200, 100);
	public float _thickness = 2;


	[Export]
	public int Count
	{
		get => _count;
		set
		{
			_count = value;
			update();
		}
	}

	[Export]
	public PackedScene LineScene { get; set; }

	[Export]
	public Vector2 Size
	{
		get => _size;
		set
		{
			_size = value;
			update();
		}
	}

	[Export]
	public float Thickness
	{
		get => _thickness;
		set
		{
			_thickness = value;
			update();
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		update();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}



	private void update()
	{


		while (lines.Count > 0) {
			lines[0].QueueFree();
			lines.RemoveAt(0);
		}

		for (int i = 0; i < _count; i++) {
			addLine(i);
		}
	}

	private void addLine(int index)
	{
		float yOffset = ((_size.Y / 2) / _count) * index;
		float max = (yOffset + ((_size.Y / 2) / _count)) * (index + 1) * _thickness;
		float xOffset = (_size.X) * (index + 0.1f) / _count;

		//Top half
		smooth_path line = LineScene.Instantiate<smooth_path>();
		line.SplineLength = (int) _size.X / 10;

		line.Curve.AddPoint(new Vector2(0, yOffset));
		line.Curve.AddPoint(new Vector2(-xOffset, -_size.Y / 5 * _thickness * 0.5f));
		line.Curve.AddPoint(new Vector2((_size.X / 2), -max));
		line.Curve.AddPoint(new Vector2(xOffset + _size.X, -_size.Y / 5 * _thickness * 0.5f));
		line.Curve.AddPoint(new Vector2(_size.X, yOffset));
		line.Smooth = true;
		line.AddArrows();

		lines.Add(line);
		AddChild(line);

		//Bottom half
		line = LineScene.Instantiate<smooth_path>();
		line.SplineLength = (int) _size.X / 10;

		line.Curve.AddPoint(new Vector2(0, _size.Y - yOffset));
		line.Curve.AddPoint(new Vector2(-xOffset, _size.Y + (_size.Y / 5 * _thickness * 0.5f)));
		line.Curve.AddPoint(new Vector2((_size.X / 2), _size.Y + max));
		line.Curve.AddPoint(new Vector2(xOffset + _size.X, _size.Y + (_size.Y / 5 * _thickness * 0.5f)));
		line.Curve.AddPoint(new Vector2(_size.X, _size.Y - yOffset));
		line.Smooth = true;
		line.AddArrows();

		lines.Add(line);
		AddChild(line);
	}

	public void TreeExit()
	{
		while (lines.Count > 0) {
			lines[0].QueueFree();
			lines.RemoveAt(0);
		}
	}

	public void TreeEntered()
	{
		update();
	}
}
