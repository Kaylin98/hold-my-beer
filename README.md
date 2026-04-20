# 🍺 Hold My Beer

**A high-velocity endless runner where hydration is the only fuel and stopping is not an option.**

[![Unity](https://img.shields.io/badge/Made%20with-Unity-black?style=flat&logo=unity)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-Mobile%20%2F%20Web-green)]()
[![Spillage Risk](https://img.shields.io/badge/Spillage-Extreme-orange)]()

> *"A true warrior never drops his axe. A legend never spills his beer."*

---

## 🎮 Play The Game
**[👉 CLICK HERE TO SPRINT (AND SPILL) 👈](https://play.unity.com/en/games/e0ec2768-7935-4498-9475-97fdc4f21fc3/hold-my-beer)**

---

## 🧐 What is this?
**Hold My Beer** is an endless runner that rewards aggressive pace and questionable decision-making. You play as a warrior who realizes that the more he drinks, the faster he moves. 

The goal is to rack up the highest score possible before the clock hits zero. It's a game of momentum—stacking speed, dodging disaster, and chasing the next checkpoint to stay alive.

---

## 🕹️ Controls

Simple movement for a complicated mission.

| Action | Input |
| :--- | :--- |
| **Move Left** | `A` or `LEFT ARROW` |
| **Move Right** | `D` or `RIGHT ARROW` |
| **Move Forward** | `W` or `UP ARROW` |
| **Move Backward** | `S` or `DOWN ARROW` |

---

## 🧪 The Mechanics (How to Survive)

### 🍺 The Beer Stack
* **The Logic:** Every beer you collect increases your base movement speed.
* **The Result:** The more you grab, the faster you go. Speed is the only way to reach those distant checkpoints.

### 🍷 The Wine Buff (Coin Magnet)
* **The Logic:** Collecting a bottle of wine activates the **Coin Magnet** effect.
* **The Duration:** For 10 seconds, coins will be pulled directly to you, allowing you to focus on the road while the gold finds its own way into your pockets.

### 🛑 Obstacles
* **The Penalty:** Hitting an object doesn't just hurt; it kills your momentum.
* **The Effect:** Every collision slows you down significantly. Too many hits and you'll find yourself crawling while the timer ticks away.

### ⏳ Time & Checkpoints
* **The Pressure:** You are constantly fighting a countdown. When the time hits zero, the run is over.
* **The Life-Line:** Every time you pass a **Checkpoint**, 5 seconds are added to your clock. Precision driving is required to hit these marks and keep the run alive.

---

## 🛠️ Under The Hood

For the curious, here is the logic powering the run:

### 📈 Dynamic Velocity Stacking
The movement system uses a `CurrentSpeed` variable that increments with every `OnTriggerEnter` event involving a beer. This value is clamped to ensure the warrior doesn't reach literal light speed, but it provides a tactile sense of increasing danger as the run progresses.

### 🧲 Magnet Logic
The Wine power-up triggers a Coroutine that enables a `SphereCollider` with a large radius. Any object tagged "Coin" that enters this trigger is given a `Vector3.MoveTowards` command targeting the player's position until the 10-second timer expires.

### ⏱️ Time Management System
A centralized `GameManager` handles the countdown. Checkpoint triggers communicate directly with this manager to reset or extend the `TimeRemaining` float, ensuring the UI and the logic stay synced under pressure.

---

## 📦 Credits

* **Engine:** Unity 6.3
* **Developer:** Kaylin Maharaj
* **Audio:** Tavern-themed ambience and arcade-style pickup sound effects.

---

*Made with 💖 and a very urgent need for checkpoints.*
