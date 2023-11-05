class_name CameraOrbit
extends Node3D

#region Export Variables

#endregion Export Variables
#region Public Variables
var look_sensitivity: float = 15.0
# For clamping the camera's look angle
var min_look_angle: float =  -20.0
var max_look_angle: float =  75.0

var mouse_delta: Vector2 = Vector2.ZERO

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var player: CharacterBody3D = get_parent()

#endregion OnReady Variables
#region Built-in Godot Methods
func _input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		mouse_delta = event.relative

func _process(delta: float) -> void:
	var rot = Vector3(mouse_delta.y, mouse_delta.x, 0) * look_sensitivity * delta
	rotation_degrees.x = clamp(rotation_degrees.x + rot.x, min_look_angle, max_look_angle)
	player.rotation_degrees.y -= rot.y
	mouse_delta = Vector2.ZERO


func _ready() -> void:
	# Capture mouse
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	# Make camera look at horizon on load
	rotation_degrees.x = 0

#endregion Built-in Godot Methods
#region Public Methods

#endregion Public Methods
#region Private Methods

#endregion Private Methods
