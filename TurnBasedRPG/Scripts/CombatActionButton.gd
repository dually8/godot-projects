extends Button
class_name CombatActionButton

var combat_action: CombatAction

# Get Turn Manager on ready
@onready var turn_manager: TurnManager = get_node("/root/BattleScene")

## Will be used to connect button.pressed signal
func execute_action() -> void:
	print(str("Selected \"",combat_action.display_name),"\"")
	# Get Current Character
	var current_character = turn_manager.current_character
	# Cast Combat Action
	current_character.cast_combat_action(combat_action)
