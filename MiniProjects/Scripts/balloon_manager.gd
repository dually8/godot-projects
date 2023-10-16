class_name BalloonManager extends Node3D

var _score: int = 0
@export var score_text: Label

func increase_score(amount):
	_score += amount
	score_text.text = str("Score: ", _score)
