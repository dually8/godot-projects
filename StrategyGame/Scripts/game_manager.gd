extends Node2D
class_name GameManager

#region Export Variables

#endregion Export Variables
#region Public Variables

var current_food: int = 0
var current_metal: int = 0
var current_oxygen: int = 0
var current_energy: int = 0

var food_per_turn: int = 0
var metal_per_turn: int = 0
var oxygen_per_turn: int = 0
var energy_per_turn: int = 0

var current_turn: int = 1
var currently_placing_building: bool = false
var building_to_place: BuildingData.BuildingType = BuildingData.BuildingType.MINE

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var ui: UI = get_node(Constants.UI_NODE_PATH)
@onready var map: Map = get_node(Constants.TILES_NODE_PATH)

#endregion OnReady Variables
#region Built-in Godot Methods

func _ready() -> void:
	ui.update_resources()
	ui.update_turn()

#endregion Built-in Godot Methods
#region Public Methods

func on_select_building(building_type: BuildingData.BuildingType) -> void:
	currently_placing_building = true
	building_to_place = building_type

	map.highlight_available_tiles()

func add_to_resource_per_turn(resource_type: BuildingData.ResourceType, amount: int) -> void:
	match resource_type:
		BuildingData.ResourceType.FOOD:
			food_per_turn += amount
		BuildingData.ResourceType.METAL:
			metal_per_turn += amount
		BuildingData.ResourceType.OXYGEN:
			oxygen_per_turn += amount
		BuildingData.ResourceType.ENERGY:
			energy_per_turn += amount
		_: # _ here is equivalent to default in a switch statement
			return

func place_building(tile: Tile) -> void:
	currently_placing_building = false
	var building: BuildingData.Building = _get_building_to_place(building_to_place)
	add_to_resource_per_turn(building.prod_resource, building.prod_resource_amount)
	add_to_resource_per_turn(building.upkeep_resource, building.upkeep_resource_amount)
	map.place_building_at_tile(building.icon_texture, tile)

	ui.update_resources()

func end_turn() -> void:
	current_turn += 1
	current_food += food_per_turn
	current_metal += metal_per_turn
	current_oxygen += oxygen_per_turn
	current_energy += energy_per_turn

	ui.update_resources()
	ui.update_turn()


#endregion Public Methods
#region Private Methods

func _get_building_to_place(building_type: BuildingData.BuildingType) -> BuildingData.Building:
	match building_type:
		BuildingData.BuildingType.MINE:
			return BuildingData.mine
		BuildingData.BuildingType.GREENHOUSE:
			return BuildingData.greenhouse
		BuildingData.BuildingType.SOLAR_PANEL:
			return BuildingData.solar_panel
		_: # _ here is equivalent to default in a switch statement
			return BuildingData.mine

#endregion Private Methods
