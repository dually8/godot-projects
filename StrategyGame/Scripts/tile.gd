extends Area2D
class_name Tile


#region Export Variables
@export var is_start_tile: bool = false

#endregion Export Variables
#region Public Variables
var has_building: bool = false
var can_place_building: bool = false

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var highlight:  Sprite2D = %Highlight
@onready var building_icon:  Sprite2D = %BuildingIcon
@onready var game_manager: GameManager = get_node(Constants.GAME_MANAGER_NODE_PATH)

#endregion OnReady Variables
#region Built-in Godot Methods
func _ready() -> void:
	add_to_group(Constants.TILE_GROUP)

func _on_input_event(viewport: Node, event: InputEvent, shape_idx: int) -> void:
	if event is InputEventMouseButton and event.is_pressed():
		if game_manager.currently_placing_building and can_place_building:
			game_manager.place_building(self) # self being the Tile here

#endregion Built-in Godot Methods
#region Public Methods

# Side effect: sets can_place_building
func set_highlight(value: bool) -> void:
	highlight.visible = value
	can_place_building = value

func place_building(building_texture: Texture) -> void:
	has_building = true
	building_icon.texture = building_texture
	building_icon.visible = true

#endregion Public Methods
#region Private Methods

#endregion Private Methods
