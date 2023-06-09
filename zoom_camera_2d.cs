using Godot;
using System;

public partial class zoom_camera_2d : Camera2D
{
	private float _zoomLevel = 1.0f;

	private bool _dragging = false;

	[Export]
	public float MinZoom = 0.5f;
	[Export]
	public float MaxZoom = 2.0f;
	[Export]
	public float ZoomFactor = 0.1f;
	[Export]
	public float ZoomDuration = 0.2f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = new Vector2(1280 / 2, 720 / 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{

		if (@event.IsActionPressed("zoom_in")) {
			setZoomLevel(_zoomLevel - ZoomFactor);
		}

		if (@event.IsActionPressed("zoom_out")) {
			setZoomLevel(_zoomLevel + ZoomFactor);
		}

		if (@event.IsActionPressed("camera_move")) {
			_dragging = true;
		}

		if (@event.IsActionReleased("camera_move")) {
			_dragging = false;
		}

		if (@event is InputEventMouseMotion mouseEvent && _dragging){
			Position = Position - mouseEvent.Relative;
		}
	}

	private void setZoomLevel(float value) {
		_zoomLevel = Mathf.Clamp(value, MinZoom, MaxZoom);

		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "zoom", new Vector2(_zoomLevel, _zoomLevel), ZoomDuration).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
	}
}
