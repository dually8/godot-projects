class_name PlayerUI
extends VBoxContainer

@export var buttons: Array[NodePath]

@onready var turn_manager: TurnManager = get_node("/root/BattleScene")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	turn_manager.character_begin_turn.connect(_on_character_begin_turn)
	turn_manager.character_end_turn.connect(_on_character_end_turn)


func _on_character_begin_turn(character: Character) -> void:
	visible = character.is_player
	if character.is_player:
		_reset_buttons()
		_display_combat_actions(character.combat_actions)

func _on_character_end_turn(_character: Character) -> void:
	visible = false

func _display_combat_actions(combat_actions: Array[CombatAction]) -> void:
	# Create buttons for each combat action
	for action in combat_actions:
		var button = CombatActionButton.new()
		button.text = action.display_name
		button.combat_action = action
		# Connect "pressed" signal
		button.pressed.connect(button.execute_action)
		add_child(button)

func _reset_buttons() -> void:
	for button in get_children():
		button.queue_free()
