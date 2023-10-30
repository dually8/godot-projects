# Game Design Notes

## Core Mechanic

> The action of play which makes the gameplay. The main thing that the player does throughout the game.

Examples:

- Candy Crush: Matching candies
- Super Mario Bros: Jumping

### Why Core Mechanic?

- This keeps your scope flexible
- Good for smaller games that are able to be finished

Example: Mario

- Jump
  - Add things to jump over
  - Add things to hit by jumping
  - Add things to jump and get to

## Experience

> Games are there to give you a specific experience

Know what experience you want for the player

### Types of Experience

- Challenge
- Thrill
- Comedy
- Exploration
- Strategy
- Narrative
- etc.

Examples:

- Skyrim: Exploration, Adventure, Action
- Civilization: Strategy
- The Sims: Self-Expression

### Why play your game?

- For fun?
- For a challenge?
- For cozy vibes?

How do other game devs encourage their audience to feel a certain way?

- Take into consideration the experience when making design decisions
- Make each mechanic/goal/system contribute to giving the player the desired experience
- **IMPORTANT**: Make sure to keep the desired experience in mind while creating the game

#### Analysis

Horror Games

- Experience: Scary
- Design decisions to accomplish this
  - Dark environment with little light
  - Slow player movement
  - Silence and/or tense music
  - Ambient/environmental sounds (e.g., footsteps, creaking, etc.)

#### Course Challenge 1

1. Think of your favorite game
   1. Persona 5
2. Figure out what kind of experience the game is trying to encourage.
   1. Possibilities
      1. Stand up to authority / stand up for yourself
      2. The power of relationships (specifically friendship)
3. What design choices did the devs makes to encourage the experience?
   1. The narrative begins with standing up against someone
   2. The importance placed on building up confidants/social links. The game rewards you with in game tools/items/moves and good story beats for doing this.

## Goals

- Games needs goals
- Can be short and/or long term
- The player should know what their goals are at all times

Example: Chess

- Short term: Capture a specific piece
- Long term: Checkmake

The short term goals lead to the long term goals

### Short term goals

- Example: Climb a tower (e.g., the ubisoft tower)
- Contributes to or compliments the long term goal(s)
- Keeps the player engaged

### Long term goals

- The main conflict
- Compliments the game's desired experience
- Example: Angry birds
  - Short term: finish the level
  - Long term: Unlock the next episode
- Example: Minecraft
  - Short term: Mine resources
  - Long term: Defeat the dragon

How will the player know the goals?

- Exposition
- Quests
- Visualization
  - e.g., big objective is shown in the distance. see skyrim, elden ring, etc.

## Systems

This is a spectrum. It's not one way or the other.

- Linear Gameplay
  - Player actions are predictable and intended by the dev (e.g., call of duty)
- Emergent Gameplay
  - Actions aren't necessarily what the dev intended (e.g., minecraft)

### Systemic Game Design

Design your game with _systems_ in mind

Example:

- Fire system
  - Can set enemies/things on fire
- Water system
  - Can extinguish fires and prevent them from starting

Emergent gameplay behavior: drench yourself in water before a fight with an enemy wielding fire

### Course Challenge 2

Think of a game you played where its systems interacted with one another.

Zelda: Ocarina of Time - No fire arrows? Just shoot an arrow through a lit torch or dip your arrow in the torch first. 👍

The systems: Weapon/Item system + Environment system

## Core Game Loop

- Gives the game structure
  - Repeated actions by the player
    - e.g., Monster Hunter: Fight monsters -> Get parts -> Build/upgrade weapons/armor -> Fight harder monsters
- Gives player clear objective (What are they trying to do?)
- Multiple Loops
  - e.g., Stardew Valley
    - Plant seeds -> Water -> Sell
    - Cast rod -> Catch fish -> Sell
    - Mine rocks -> Find ladder -> Go to next level with better rocks

### Course Challenge 3

- What's the core gameplay loop of: Phasmophobia?
  - Go on missions -> Get money -> Buy better equipment -> Go on harder missions

## Guiding the player

- Guide the player through your level/world (e.g., the next room, enemy, city, etc.)
- How?
  - Light (e.g., Elden Ring)
  - Color (e.g., Witcher 3, The Yellow Lines ™)
  - Guiding Lines (e.g., Shadow of the Colossus, Breath of the Wild)
    - **ME NOTE**: This seems like an extension of light
  - Landmarks (e.g., Skyrim, Ubisoft Tower ™)

### Side me notes

I saw "weenie" in one of the things to "look up" and I thought it was a typo. Evidently, it's a real term. According to [this article](https://www.gamedeveloper.com/design/a-taxonomy-of-weenies-the-landmarks-that-define-i-ghost-of-tsushima-i-) "weenie" is a term coined by some people from disney used to refer to "tall visual landmarks that are centralized attractions". They give EPCOT as one such example (you know, the big round globe there). It seems that it's used as a kind of northern star guiding light type thing. Like, "oh, I'm seeing this part of the globe, so I must be on the west side of the park". Something like that. WEIRD LOL

## Game Feel

- The focus on player feedback
- How should this _feel_ to control?
  - Floaty (PUFF)
  - Rigid
  - Slow
  - Fast
  - etc.
- 4 Steps
  - Input
    - How the player communicates with the game
      - e.g., Controllers: Pushing the joystick forward moves the player in that direction
    - It should feel natural (e.g., R2/RT is shoot in basically all FPSs, so making shoot a different button feels weird at this point)
  - Response
    - How does the game response to the inputs?
      - e.g., holding jump button gives you a bigger/longer jump
  - Context
    - Influences the player's sense of control
      - e.g., open world games should let the player traverse quickly
  - Polish
    - How things look and sound
      - e.g., Smash Bros/Street Fighter 6 sell the impact of hits with sounds and visual feedback
- Your game's metaphor
  - Ties into how the game world is perceived and what player's should expect
    - e.g., the difference between Mario Kart and Gran Turismo as racing games (unrealistic vs realistic)
    - e.g., the difference between Doom and Insurgency (no fall damage vs fall damage)

## Teaching the player

- How to control the player?
- How to engage with the systems?
- What is the story of the game?

### Text Tutorials

- Easiest (but potentially worst) way since it takes you out of the experience
- Most useful for strategy games and/or complex game mechanics

### 3 Step Process

1. Teach the player the mechanic in a safe environment
2. Allow the player to use the mechanic in a real situation
3. Combine the mechanic with others

#### Example

- Half-Life 2
  - [Barnacle enemy](https://half-life.fandom.com/wiki/Barnacle)
    - First appearance is a scripted encounter showing you how it sucks up things into it from the ceiling
    - Next appearance, player encounters enemy and can decide whether to shoot it or avoid it
    - Later in the game, player encounters room full of them and lets the player use explosive barrels in order to eradicate room of enemies

### Course Challenge 4

Think of a game you played recently and how its mechanics were taught. What improvements would you make?

Persona 3 Portable

Intro battle is basically done for you, but it serves as a tool to help you know what to expect from battles later on. During the first mission to Tartarus, you're reminded of the system and gradually feed new information like how to exploit weaknesses. This is done mostly through text/dialogue, but it feels natural due to the relationship you have with the characters.

To be honest, I don't know how I'd "improve" this because 1) I think to works well enough, and 2) I don't know how you'd introduce a more "natural" approach to this. I remember games like Pokémon Red/Blue basically just let you figure out type weaknesses by yourself or through NPC dialogue, but the rock/paper/scissors mechanics are so ingrained into the gaming zeitgeist at this point, I think it's hard to overlook it unless you're brand new to gaming. I think it's hard to not have a lot of things either shown or explained to you in today's landscape with YouTube and other social media. I bought Super Mario Wonder recently and everything has kind of been spoiled for me at this point because I feel like I've already seen everything about it online, so the game was never given a chance to teach me anything, especially because I've been playing Mario since 1992. It's weird.

## Creating your portfolio

> **ME NOTE**: this section feels obvious, so I may not write a whole lot here. Stuff like, "make your resume look good" (unhelpful)

- What type of job?
  - Designer
  - Programmer (<- Me. I'm this one 😊)
  - Artist
- Design your portfolio around the job you want

### Project template for resume

**NOTE**: Date range here is the development cycle from start to finish, not the lifespan of the game

Game Name (January 2021 - March 2021)

Platform: PC
Role: Lead programmer

About: A short description about the game and what you contributed to it.

## Further Reading

- [The Art of Game Design - Jesse Schell](https://schellgames.com/art-of-game-design/)
- [Designing Around a Core Mechanic](https://www.gamedeveloper.com/design/designing-around-a-core-mechanic)
- [Using Player Experience to Design Games](https://www.gamedeveloper.com/design/using-player-experience-to-design-games)
- [The Objectives Of Game Goals](https://www.gamedeveloper.com/mobile/the-objectives-of-game-goals)
- [How to Keep Players Engaged (Without Being Evil)](https://www.youtube.com/watch?v=hbzGO_Qonu0)
- [Systemic Games: A Design Philosophy](https://the-artifice.com/systemic-games-philosophy/#:~:text=Systemic%20games%20are%20games%20that,whole%20game%20world%20is%20affected.)
- [Advanced Game Design: A Systems Approach - Michael Sellers](https://www.goodreads.com/book/show/35135766-advanced-game-design)
- [The Rise of the Systemic Game](https://www.youtube.com/watch?v=SnpAAX9CkIc)
- [Why the Core Gameplay Loop is Critical For Game Design](https://www.gamedeveloper.com/business/why-the-core-gameplay-loop-is-critical-for-game-design)
- [The Importance of a Well Defined Core Gameplay Loop](https://www.gamedeveloper.com/design/the-importance-of-a-well-defined-core-gameplay-loop)
- [An Architectural Approach to Level Design - Christopher W. Totten](https://www.amazon.com.au/Architectural-Approach-Level-Design-ebook/dp/B00L2EBHCI)
- [Level Design Tips and Tricks](https://www.gamedeveloper.com/design/level-design-tips-and-tricks)
- [Level Design Playlist by Game Maker’s Toolkit](https://www.youtube.com/playlist?list=PLc38fcMFcV_t6cVUpPXYnooVe1r_C0_4f)
- [Game Feel - Steve Swink](https://www.goodreads.com/en/book/show/3385050-game-feel)
- [Game Feel: Why your death animation sucks](https://www.youtube.com/watch?v=pmSAG51BybY)
- [Super Mario 3D World's 4 Step Level Design](https://www.youtube.com/watch?v=dBmIkEvEBtA)
