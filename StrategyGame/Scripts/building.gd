extends Node
## Class Name BuildingData

var building_factory: BuildingFactory = BuildingFactory.new()

var base: Building = building_factory.create_base()
var mine: Building = building_factory.create_mine()
var greenhouse: Building = building_factory.create_greenhouse()
var solar_panel: Building = building_factory.create_solar_panel()


enum BuildingType {
	BASE,
	MINE,
	GREENHOUSE,
	SOLAR_PANEL
}

enum ResourceType {
	FOOD,
	METAL,
	OXYGEN,
	ENERGY
}

class Building:
	var type: BuildingType
	var icon_texture: Texture
	var prod_resource: ResourceType
	var prod_resource_amount: int
	var upkeep_resource: ResourceType
	var upkeep_resource_amount: int

	func _init(
		type: BuildingType,
		icon_texture: Texture,
		prod_resource: ResourceType,
		prod_resource_amount: int,
		upkeep_resource: ResourceType,
		upkeep_resource_amount: int
	) -> void:
		self.type = type
		self.icon_texture = icon_texture
		self.prod_resource = prod_resource
		self.prod_resource_amount = prod_resource_amount
		self.upkeep_resource = upkeep_resource
		self.upkeep_resource_amount = upkeep_resource_amount

class BuildingFactory:
	func create_base() -> Building:
		var base = Building.new(
			BuildingType.BASE, # Type
			preload("res://Sprites/Base.png"),  # Icon Texture
			ResourceType.FOOD, # Prod Resource
			0, # Prod Resource Amount
			ResourceType.FOOD, # Upkeep Resource
			0 # Upkeep Resource Amount
		)
		return base
	func create_mine() -> Building:
		var mine = Building.new(
			BuildingType.MINE, # Type
			preload("res://Sprites/Mine.png"),  # Icon Texture
			ResourceType.METAL, # Prod Resource
			1, # Prod Resource Amount
			ResourceType.ENERGY, # Upkeep Resource
			-1 # Upkeep Resource Amount
		)
		return mine
	func create_greenhouse() -> Building:
		var greenhouse = Building.new(
			BuildingType.GREENHOUSE, # Type
			preload("res://Sprites/Greenhouse.png"),  # Icon Texture
			ResourceType.FOOD, # Prod Resource
			1, # Prod Resource Amount
			## NO UPKEEP
			ResourceType.ENERGY, # Upkeep Resource
			0 # Upkeep Resource Amount
		)
		return greenhouse
	func create_solar_panel() -> Building:
		var solar_panel = Building.new(
			BuildingType.SOLAR_PANEL, # Type
			preload("res://Sprites/SolarPanel.png"),  # Icon Texture
			ResourceType.ENERGY, # Prod Resource
			1, # Prod Resource Amount
			## NO UPKEEP
			ResourceType.ENERGY, # Upkeep Resource
			0 # Upkeep Resource Amount
		)
		return solar_panel
