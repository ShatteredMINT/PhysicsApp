using Godot;
using System;

using System.Collections.Generic;

[Tool]
public partial class coil : Node2D
{
	private List<Sprite2D> sprites = new List<Sprite2D>();

	private int _count = 0;
	private Vector2 _size = new Vector2(200, 100);

	[Export]
	public PackedScene Wind;

	[Export]
	public int Windings
	{
		get => _count;
		set
		{
			_count = value;
			update();
		}
	}

	[Export]
	public int FieldLines
	{
		get => GetNode<field>("Field").Count;
		set
		{
			GetNode<field>("Field").Count = value;
		}
	}

	[Export]
	public Vector2 Size
	{
		get => _size;
		set
		{
			_size = value;
			update();

			GetNode<field>("Field").Size = _size;
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

	private void update()
	{
		while (sprites.Count > 0) {
			sprites[0].QueueFree();
			sprites.RemoveAt(0);
			GD.Print("Removing winding");
		}

		for (int i = 0; i < _count; i++) {

			Sprite2D winding = Wind.Instantiate<Sprite2D>();

			winding.Scale = new Vector2(_size.X / (_count * winding.Texture.GetWidth()), _size.Y / winding.Texture.GetHeight());

			if (i > 0)
				winding.Position = new Vector2(sprites[i-1].Texture.GetWidth() * sprites[i-1].Scale.X + sprites[i-1].Position.X, winding.Texture.GetHeight() * winding.Scale.Y /2);
			else
				winding.Position = new Vector2(winding.Texture.GetWidth() * winding.Scale.X / 2, winding.Texture.GetHeight() * winding.Scale.Y /2);

			sprites.Add(winding);

			AddChild(winding);
			GD.Print("Adding winding " + i);
		}
	}

	public void TreeExit()
	{
		while (sprites.Count > 0) {
			sprites[0].QueueFree();
			sprites.RemoveAt(0);
			GD.Print("Removing winding");
		}
	}

	public void TreeEntered()
	{
		update();
	}
}
