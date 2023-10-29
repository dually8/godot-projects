extends Control
class_name UI



#region Export Variables

#endregion Export Variables
#region Public Variables

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables
@onready var end_turn_button: Button = %EndTurnButton
@onready var turn_text: Label = %TurnText
@onready var building_buttons: HBoxContainer = %BuildingButtons
@onready var mine_button: Button = %MineButton
@onready var green_house_button: Button = %GreenHouseButton
@onready var solar_panel_button: Button = %SolarPanelButton
@onready var food_amount: Label = %FoodAmount
@onready var metal_amount: Label = %MetalAmount
@onready var oxygen_amount: Label = %OxygenAmount
@onready var energy_amount: Label = %EnergyAmount

@onready var game_manager: GameManager = get_node(Constants.GAME_MANAGER_NODE_PATH)

#endregion OnReady Variables
#region Built-in Godot Methods

#endregion Built-in Godot Methods
#region Public Methods

func update_resources():
	food_amount.text = _get_resource_text(
		game_manager.current_food,
		game_manager.food_per_turn)
	metal_amount.text = _get_resource_text(
		game_manager.current_metal,
		game_manager.metal_per_turn)
	oxygen_amount.text = _get_resource_text(
		game_manager.current_oxygen,
		game_manager.oxygen_per_turn)
	energy_amount.text = _get_resource_text(
		game_manager.current_energy,
		game_manager.energy_per_turn)

func update_turn() -> void:
	turn_text.text = "Turn " + str(game_manager.current_turn)
	building_buttons.visible = true

#endregion Public Methods
#region Private Methods

func _get_resource_text(current: int, per_turn: int) -> String:
	return str(current, " (", "+" if per_turn > 0 else "", per_turn, ")")

#endregion Private Methods


func _on_end_turn_button_pressed() -> void:
	update_turn()
	game_manager.end_turn()


func _on_mine_button_pressed() -> void:
	_on_building_button_pressed(BuildingData.BuildingType.MINE)

func _on_green_house_button_pressed() -> void:
	_on_building_button_pressed(BuildingData.BuildingType.GREENHOUSE)

func _on_solar_panel_button_pressed() -> void:
	_on_building_button_pressed(BuildingData.BuildingType.SOLAR_PANEL)

func _on_building_button_pressed(type: BuildingData.BuildingType) -> void:
	building_buttons.visible = false
	game_manager.on_select_building(type)
