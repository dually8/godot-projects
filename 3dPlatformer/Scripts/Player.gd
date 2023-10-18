class_name Player extends CharacterBody3D

# Actions
# - jump
# - left
# - right
# - up
# - down
# - menu
# Usage: Input.is_action_pressed("right")

var move_speed: float = 4.0
var jump_force: float = 8.0
var gravity: float = 20.0
var score: int = 0

# For setting models facing angle
var facing_angle: float

@onready var model: MeshInstance3D = get_node("Model")
@onready var score_text: Label = get_node("ScoreText")

func _physics_process(delta: float):
	if not is_on_floor():
		velocity.y -= gravity * delta

	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = jump_force

	var input_direction = Input.get_vector("left", "right", "up", "down")
	var direction = Vector3(input_direction.x, 0, input_direction.y)

	velocity.x = direction.x * move_speed
	velocity.z = direction.z * move_speed

	move_and_slide()

	# If input is pressed, set the facing angle to the input direction
	if input_direction.length() > 0:
		facing_angle = Vector2(input_direction.y, input_direction.x).angle()
		# Smoothly rotate the model to the facing angle
		model.rotation.y = lerp_angle(model.rotation.y, facing_angle, 0.3)

	# Game over if player falls off the map
	if global_position.y < -5.0:
		game_over()

# Reset scene on death
func game_over():
	print("YOU DIED")
	await _show_died_screen()
	get_tree().reload_current_scene()

func _show_died_screen():
	print("show died screen")
	var died_screen = get_tree().get_first_node_in_group("DiedScreen") as Control
	died_screen.visible = true
	get_tree().paused = true
	await get_tree().create_timer(2.0).timeout
	died_screen.visible = false
	get_tree().paused = false

func add_score(amount: int) -> void:
	score += amount
	score_text.text = str("Score: ", score)
	print("Score: ", score)

### Default script below
# const SPEED = 5.0
# const JUMP_VELOCITY = 4.5

# # Get the gravity from the project settings to be synced with RigidBody nodes.
# var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")


# func _physics_process(delta):
# 	# Add the gravity.
# 	if not is_on_floor():
# 		velocity.y -= gravity * delta

# 	# Handle Jump.
# 	if Input.is_action_just_pressed("ui_accept") and is_on_floor():
# 		velocity.y = JUMP_VELOCITY

# 	# Get the input direction and handle the movement/deceleration.
# 	# As good practice, you should replace UI actions with custom gameplay actions.
# 	var input_dir = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down")
# 	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
# 	if direction:
# 		velocity.x = direction.x * SPEED
# 		velocity.z = direction.z * SPEED
# 	else:
# 		velocity.x = move_toward(velocity.x, 0, SPEED)
# 		velocity.z = move_toward(velocity.z, 0, SPEED)

# 	move_and_slide()
