using Godot;
using System;

[Tool]
public partial class vaĺue_editor : VBoxContainer
{
	[Signal]
	public delegate void ChangedValueEventHandler(double value);

	private double _maxValue = 100;
	private double _minValue = 0;
	private double _Value = 50;

	[Export]
	public double MaxValue
	{
		get => _maxValue;
		set
		{
			_maxValue = value;
			
			GetNode<SpinBox>("SpinBox").MaxValue = _maxValue;
			GetNode<HSlider>("HSlider").MaxValue = _maxValue;
		}
	}
	[Export]
	public double MinValue
	{
		get => _minValue;
		set
		{
			_minValue = value;
			
			GetNode<SpinBox>("SpinBox").MinValue = _minValue;
			GetNode<HSlider>("HSlider").MinValue = _minValue;
		}
	}
	[Export]
	public double Value
	{
		get => _Value;
		set
		{
			_Value = value;
			GetNode<SpinBox>("SpinBox").SetValueNoSignal(_Value);
			GetNode<HSlider>("HSlider").SetValueNoSignal(_Value);

			if (!Engine.IsEditorHint()) {
				EmitSignal(SignalName.ChangedValue, _Value);
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
	private void OnSliderChanged(double value)
	{
		Value = value;
	}
	
	private void OnBoxChanged(double value)
	{
		Value = value;
	}	
}
