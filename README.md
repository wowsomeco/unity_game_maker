# Wowsome Game Maker

The sets of functionalities that allow each gameobject to have multiple [Active Components](https://github.com/wowsomeco/unity_game_maker/tree/master/scripts/core/components) that is reactive to events that can be either received or sent the chain reaction(s) in form of another events accordingly.
This is more geared towards 2D stuff in Unity, where at the moment it's more specific to UGUI thingy. We might need to tweak the stuff more to handle Sprite Renderer too eventually should there be more projects that require it.

## How To Use

Add [wowcore](https://github.com/wowsomeco/unity_wowcore.git) as the submodule dependency first
```sh
git submodule add https://github.com/wowsomeco/unity_wowcore.git wcore
```

Then add this repo as a submodule in your Unity Project e.g.

```sh
git submodule add https://github.com/wowsomeco/unity_game_maker.git Assets/wgamemaker
```

## Contents

- Components
  - [Tween](https://github.com/wowsomeco/unity_game_maker/blob/master/scripts/core/components/WGMTween.cs)
  - [Broadcaster](https://github.com/wowsomeco/unity_game_maker/blob/master/scripts/core/components/WGMBroadcaster.cs)
  - [Activator](https://github.com/wowsomeco/unity_game_maker/blob/master/scripts/core/components/WGMActivator.cs)

## Dependencies

The items below need to be added as submodules into your project in order to use this submodule:

- [unity_wowcore](https://github.com/wowsomeco/unity_wowcore)

## Roadmap

* Object can communicate with each other.
* More components e.g. Gesture Handlers, Counter, Timer, etc.
* Node Editor to generate the Object(s) along with its component(s) + event handler(s) accordingly.
* More docs, and sample scenes.