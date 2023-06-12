using Godot;
using System;

public partial class main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<value_editor>("CanvasLayer/Diameter").Value = GetNode<coil>("Coil").Size.Y;
		GetNode<value_editor>("CanvasLayer/Windings").Value = GetNode<coil>("Coil").Windings;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnWindingsChanged(double value)
	{
		GetNode<coil>("Coil").Windings = (int) value;
	}

	private void OnDiamaterChanged(double value)
	{
		GetNode<coil>("Coil").Size = new Vector2(GetNode<coil>("Coil").Size.X, (float) value);
	}

	private void OnMaterialChanged(double value)
	{
		GetNode<value_display>("CanvasLayer/FieldStrength").Value = value;
	}
}
