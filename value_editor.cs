using Godot;
using System;

[Tool]
public partial class value_editor : VBoxContainer
{
	[Signal]
	public delegate void ChangedValueEventHandler(double value);

	private string _title = "title";
	private double _maxValue = 100;
	private double _minValue = 0;
	private double _Value = 50;

	[Export]
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			if (Engine.IsEditorHint())
				GetNode<Label>("Label").Text = _title;
		}
	}

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
			SetValueNoSignal(value);

			if (!Engine.IsEditorHint()) {
				EmitSignal(SignalName.ChangedValue, _Value);
			}
		}
	}

	[Export]
	public double Step = 1;
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<SpinBox>("SpinBox").Step = Step;
		GetNode<HSlider>("HSlider").Step = Step;
		
		GetNode<Label>("Label").Text = _title;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetValueNoSignal(double value)
	{
		_Value = value;
		GetNode<SpinBox>("SpinBox").SetValueNoSignal(_Value);
		GetNode<HSlider>("HSlider").SetValueNoSignal(_Value);

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
