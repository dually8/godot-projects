extends CharacterBody3D
class_name Enemy


# Stats
var health: int = 5
var move_speed: float = 2.0

# Attacking
var damage: int = 1
var attack_rate: float = 1.0
var attack_range: float = 2.0

var score_to_give: int = 10

# Components
@onready var player: Player = get_node("/root/Main/Player")
@onready var timer: Timer = %Timer

func _ready() -> void:
	# Setup timer
	timer.set_wait_time(attack_rate)
	timer.start()

func _physics_process(_delta: float) -> void:
	var is_in_range: bool = position.distance_to(player.position) <= attack_range
	if not is_in_range:
		# Move towards player
		move_towards_player()


func _on_timer_timeout() -> void:
	# Check distance to player
	if position.distance_to(player.position) <= attack_range:
		attack()

func attack() -> void:
	# Deal damage to player
	player.take_damage(damage)

func move_towards_player() -> void:
	# Calculate direction to player
	var direction: Vector3 = (player.position - position).normalized()
	# Enemies don't jump/fly lol
	direction.y = 0

	# Move enemy towards player
	velocity = direction * move_speed

	# Rotate enemy towards player
	rotation.y = atan2(direction.x, direction.z)

	move_and_slide()

func take_damage(_damage: int) -> void:
	health -= _damage
	print("Enemy took damage! Health: ", health)
	if health <= 0:
		print("Enemy died!")
		die()

func die() -> void:
	# Give player score
	player.add_score(score_to_give)
	# Destroy enemy
	queue_free()
