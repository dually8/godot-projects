extends Node2D
class_name GameManager


#region Export Variables

#endregion Export Variables
#region Public Variables
var selected_unit: Unit = null
var players: Array[Unit] = []
var enemies: Array[Unit] = []

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables

#endregion OnReady Variables
#region Built-in Godot Methods
func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton and event.pressed:
		if event.button_index == MOUSE_BUTTON_LEFT:
			_try_select_unit()
		elif event.button_index == MOUSE_BUTTON_RIGHT:
			_try_command_unit()

#endregion Bulit-in Godot Methods
#region Public Methods

#endregion Public Methods
#region Private Methods
func _get_selected_unit() -> Unit:
	var space = get_world_2d().direct_space_state
	var query = PhysicsPointQueryParameters2D.new()
	query.position = get_global_mouse_position()
	var intersection = space.intersect_point(query, 1)

	if !intersection.is_empty():
		var collider = intersection[0].collider as Object
		if collider is Unit:
			return collider as Unit
	return null

func _try_select_unit():
	var unit = _get_selected_unit() as Unit
	if unit != null and unit.is_player:
		_select_unit(unit)
	else:
		_deselect_unit()

func _select_unit(unit: Unit):
	_deselect_unit()
	selected_unit = unit as PlayerUnit
	selected_unit.toggle_selection_visual(true)

func _deselect_unit():
	if selected_unit != null:
		selected_unit.toggle_selection_visual(false)
		selected_unit = null

# Called when right click is pressed
func _try_command_unit():
	if selected_unit == null:
		return

	var target = _get_selected_unit() as Unit
	if target != null and target.is_player == false:
		selected_unit.set_target(target)
	else:
		selected_unit.move_to_location(get_global_mouse_position())

#endregion Private Methods
