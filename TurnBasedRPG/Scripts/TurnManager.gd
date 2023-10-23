class_name TurnManager
extends Node

@export var player_character: Node
@export var enemy_character: Node

var current_character: Character

@export var next_turn_delay: float = 1.0

var game_over: bool = false

signal character_begin_turn(character: Character)
signal character_end_turn(character: Character)

func begin_next_turn():
	if current_character == player_character:
		current_character = enemy_character
	elif current_character == enemy_character:
		current_character = player_character
	else:
		current_character = player_character

	character_begin_turn.emit(current_character)

func end_current_turn():
	character_end_turn.emit(current_character)

	await get_tree().create_timer(next_turn_delay).timeout

	if game_over == false:
		begin_next_turn()


func character_died(character: Character):
	game_over = true

	if character.is_player == true:
		print("You died!")
	else:
		print("You won!")

func _ready():
	await get_tree().create_timer(next_turn_delay).timeout
	begin_next_turn()
