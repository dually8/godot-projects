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
#endregion OnReady Variables
#region Built-in Godot Methods

func _physics_process(_delta: float) -> void:
	velocity = Vector2(0, 0)
	var direction = Input.get_vector("move_left", "move_right", "move_up", "move_down")
	if direction != Vector2(0, 0):
		facing_direction = direction
		velocity = direction.normalized() * move_speed

	move_and_slide()
	manage_animations()

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

#endregion Public Methods
#region Private Methods

#endregion Private Methods
