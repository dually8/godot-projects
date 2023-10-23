class_name Character
extends Node2D

@export var is_player: bool
@export var current_hp: int = 25
@export var max_hp: int = 25

@export var combat_actions: Array[CombatAction] = []
@export var opponent: Node

@onready var health_bar: ProgressBar = get_node("HealthBar")
@onready var health_text: Label = get_node("HealthBar/HealthText")

@export var visual: Texture2D
@export var flip_visual: bool = false

@onready var _turn_manager: TurnManager = get_node("/root/BattleScene")

#region Public Methods
func take_damage(damage: int):
	current_hp -= damage
	_update_health_bar()

	if current_hp <= 0:
		# Signal to the turn manager that this character has died
		_turn_manager.character_died(self)
		# Destroy this node
		queue_free()

func heal(amount: int):
	current_hp += amount
	if current_hp > max_hp:
		current_hp = max_hp
	_update_health_bar()

func cast_combat_action(action: CombatAction):
	if action.damage > 0:
		opponent.take_damage(action.damage)
	elif action.heal > 0:
		heal(action.heal)

	# End the turn
	_turn_manager.end_current_turn()

#endregion Public Methods


#region Private Methods
func _ready():
	$Sprite.texture = visual
	$Sprite.flip_h = flip_visual
	# Connect to the character_begin_turn signal
	_turn_manager.character_begin_turn.connect(_on_character_begin_turn)
	_update_health_bar()

func _update_health_bar():
	health_bar.value = current_hp
	health_bar.max_value = max_hp
	health_text.text = str(current_hp, " / ", max_hp) # e.g., 15 / 25

func _on_character_begin_turn(_character: Character):
	if _character == self and is_player == false:
		# It's this character's turn, and it's not the player's turn
		# so decide what to do
		_decide_combat_action()

func _decide_combat_action() -> void:
	var health_percent: float = float(current_hp) / float(max_hp)
	var healing_actions: Array[CombatAction] = combat_actions.filter(func (action): return action.heal > 0)
	var damaging_actions: Array[CombatAction] = combat_actions.filter(func (action): return action.damage > 0)
	var should_heal = randf() > health_percent + 0.2

	if should_heal:
		# Cast a random healing action
		var action: CombatAction = healing_actions[randi() % healing_actions.size()]
		print(str("Enemy casts ", action.display_name, "!"))
		cast_combat_action(action)
		return

	# Cast a random damaging action
	var action: CombatAction = damaging_actions[randi() % damaging_actions.size()]
	print(str("Enemy casts ", action.display_name, "!"))
	cast_combat_action(action)

#endregion Private Methods
