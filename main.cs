using Godot;
using System;

public partial class main : Node2D
{
	private double materialConstant = 1;
	private const double uniConstant = 0.0000012566370614;
	private double diameter = 1;
	private double length = 1;
	private int windings = 1;
	private double l = 1;
	private double E = 0;
	private double I = 1;
	private double mag = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		I = GetNode<value_editor>("CanvasLayer/Current").Value;
		materialConstant = GetNode<value_editor>("CanvasLayer/Material").Value;
		diameter = GetNode<value_editor>("CanvasLayer/Diameter").Value;
		length = GetNode<value_editor>("CanvasLayer/Length").Value;
		windings = (int) GetNode<value_editor>("CanvasLayer/Windings").Value;
		GetNode<coil>("Coil").Size = new Vector2(GetNode<coil>("Coil").Size.X, (float) diameter * 1000);;
		GetNode<coil>("Coil").Size = new Vector2((float) length * 1000, GetNode<coil>("Coil").Size.Y);
		GetNode<coil>("Coil").Windings = windings;

		updateL();
		updateMag();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnWindingsChanged(double value)
	{
		windings = (int) value;
		GetNode<coil>("Coil").Windings = windings;
		updateL();
		updateMag();
	}

	private void OnDiamaterChanged(double value)
	{
		diameter = value;
		GetNode<coil>("Coil").Size = new Vector2(GetNode<coil>("Coil").Size.X, (float) diameter * 1000);
		updateL();
	}

	private void OnIChanged(double value)
	{
		I = value;
		updateE();
		updateMag();
	}

	private void OnMaterialChanged(double value)
	{
		materialConstant = value;
		GetNode<value_display>("CanvasLayer/Induction").Value = materialConstant;
		updateL();
		updateMag();
	}

	private void OnLengthChanged(double value)
	{
		length = value;
		GetNode<coil>("Coil").Size = new Vector2((float) length * 1000, GetNode<coil>("Coil").Size.Y);
		updateL();
		updateMag();
	}

	private void updateL()
	{
		l = materialConstant * uniConstant * ((windings * windings) / length) * (Mathf.Pi * ((diameter / 2) * (diameter / 2)));
		GetNode<value_display>("CanvasLayer/Induction").Value = l;

		updateE();
	}

	private void updateE()
	{
		E = 0.5 * l * (I * I);
		GetNode<value_display>("CanvasLayer/FieldStrength").Value = E;

		GetNode<coil>("Coil").FieldLines = windings > 0 ? (int) Mathf.Lerp(1, 20, Mathf.Min(E / 20, 1)) : 0;
	}

	private void updateMag()
	{
		mag = materialConstant * uniConstant * I * (windings / length);
		GetNode<value_display>("CanvasLayer/Magnetism").Value = mag;
	}
}
