class_name Player
extends CharacterBody2D

#region Export Variables

#endregion Export Variables
#region Public Variables

# Available animations
enum AnimationType {
	IDLE_DOWN,
	IDLE_UP,
	IDLE_LEFT,
	IDLE_RIGHT,
	MOVE_DOWN,
	MOVE_UP,
	MOVE_LEFT,
	MOVE_RIGHT
}
const animation_names = {
	AnimationType.IDLE_DOWN: "IdleDown",
	AnimationType.IDLE_UP: "IdleUp",
	AnimationType.IDLE_LEFT: "IdleLeft",
	AnimationType.IDLE_RIGHT: "IdleRight",
	AnimationType.MOVE_DOWN: "MoveDown",
	AnimationType.MOVE_UP: "MoveUp",
	AnimationType.MOVE_LEFT: "MoveLeft",
	AnimationType.MOVE_RIGHT: "MoveRight"
}

var current_hp: int = 10
var max_hp: int = 10
var move_speed: int = 250
var damage: int = 1
var gold: int = 0
var current_level: int = 0
var current_exp: int = 0
var exp_to_next_level: int = 50
var exp_to_level_rate: float = 1.2 # Mutiplier for exp_to_next_level
var interact_distance: int = 70
var facing_direction: Vector2 = Vector2(0, 1)

#endregion Public Variables
#region Private Variables

#endregion Private Variables
#region OnReady Variables

@onready var raycast: RayCast2D = %RayCast2D
@onready var animation: AnimatedSprite2D = %AnimatedSprite2D
@onready var ui: UI = get_node("/root/Main/CanvasLayer/UI")
#endregion OnReady Variables
#region Built-in Godot Methods
func _ready() -> void:
	# Init the UI
	_update_ui()

func _physics_process(_delta: float) -> void:
	velocity = Vector2(0, 0)
	var direction = Input.get_vector("move_left", "move_right", "move_up", "move_down")
	if direction != Vector2(0, 0):
		facing_direction = direction
		velocity = direction.normalized() * move_speed

	move_and_slide()
	manage_animations()

func _input(event: InputEvent) -> void:
	if event.is_action_pressed("interact"):
		_try_interact()

#endregion Built-in Godot Methods
#region Public Methods

func manage_animations() -> void:
	if velocity.x > 0:
		play_animation(AnimationType.MOVE_RIGHT)
	elif velocity.x < 0:
		play_animation(AnimationType.MOVE_LEFT)
	elif velocity.y > 0:
		play_animation(AnimationType.MOVE_DOWN)
	elif velocity.y < 0:
		play_animation(AnimationType.MOVE_UP)
	else:
		if facing_direction.x > 0:
			play_animation(AnimationType.IDLE_RIGHT)
		elif facing_direction.x < 0:
			play_animation(AnimationType.IDLE_LEFT)
		elif facing_direction.y > 0:
			play_animation(AnimationType.IDLE_DOWN)
		elif facing_direction.y < 0:
			play_animation(AnimationType.IDLE_UP)



func play_animation(type: AnimationType) -> void:
	if animation.animation != animation_names[type]:
		animation.play(animation_names[type])

func take_damage(amount: int) -> void:
	current_hp -= amount
	_update_ui()
	print("Current HP: " + str(current_hp) + " / " + str(max_hp))
	if current_hp <= 0:
		die()

func die() -> void:
	print("You died!")
	# Restart the game
	get_tree().reload_current_scene()

func gain_exp(amount: int) -> void:
	current_exp += amount
	_update_ui()
	if current_exp >= exp_to_next_level:
		_level_up()

func give_gold(amount: int) -> void:
	gold += amount
	_update_ui()

#endregion Public Methods
#region Private Methods
func _level_up() -> void:
	var overflow_exp = current_exp - exp_to_next_level
	exp_to_next_level = int(exp_to_next_level * exp_to_level_rate)
	current_exp = overflow_exp
	current_level += 1
	_update_ui()

func _try_interact() -> void:
	raycast.target_position = facing_direction * interact_distance
	if raycast.is_colliding():
		var collider = raycast.get_collider()
		if collider is Enemy:
			var enemy: Enemy = collider
			enemy.take_damage(damage)
		elif collider is Interactable:
			var interactable: Interactable = collider
			interactable.interact(self)

func _update_ui() -> void:
	ui.set_level_text(current_level)
	ui.set_health_bar(current_hp, max_hp)
	ui.set_exp_bar(current_exp, exp_to_next_level)
	ui.set_gold_text(gold)

#endregion Private Methods
