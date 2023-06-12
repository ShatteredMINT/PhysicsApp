using Godot;
using System;

[Tool]
public partial class value_display : HBoxContainer
{
	private string _title = "title";

	private double _value = 0;

	[Export]
	public string Title
	{
		get => _title;
		set
		{
			_title = value;
			if (Engine.IsEditorHint())
				GetNode<Label>("Name").Text = _title;
		}
	}

	[Export]
	public double Value
	{
		get => _value;
		set
		{
			_value = value;

			GetNode<SpinBox>("Value").Value = _value;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Label>("Name").Text = _title;
		GetNode<SpinBox>("Value").Value = _value;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
