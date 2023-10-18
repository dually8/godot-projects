extends Control

func _unhandled_input(event: InputEvent) -> void:
	if event.is_action_pressed("menu"):
		var is_paused = get_tree().paused
		set_paused(!is_paused)

func set_paused(value: bool) -> void:
	get_tree().paused = value
	visible = value
	if value == true:
		_focus_btn()

func _on_resume_button_pressed():
	set_paused(false)

func _on_exit_button_pressed():
	get_tree().quit()

func _focus_btn():
	$GridContainer/ResumeButton.grab_focus()
