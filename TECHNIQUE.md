# Technique: The Player-Interaction Layer

This document is a deep dive into the C# interaction layer I (Peter Castelein) wrote for
the simulation — the virtual tools a lineworker uses to prepare power lines for repair,
and the logic that decides whether they did it safely. Everything here is written against
Unity's **XR Interaction Toolkit 2.2.0** (XRI), running on the Oculus XR Plugin on a
Meta Quest 2.

The code is presented verbatim as it shipped in the final capstone build (May 2023),
including a few commented-out experiments. Authorship for every file is listed at the
[bottom](#authorship).

## The task being simulated

Before a lineworker can repair a damaged power line, the neutral lines must be prepared:

1. Drive the bucket up into the designated work zone below the lines.
2. Check every line for power using a power meter on an extendable "shotgun" rod.
3. Attach grounding clamps to the neutral lines **in the prescribed safety order**.

The interaction layer implements the tools for steps 2 and 3, and the scoring engine
that evaluates all three.

## Two-hand rod interaction

[`TwoHandGrabInteractable.cs`](Assets/Scripts/ShotgunRod/TwoHandGrabInteractable.cs)
extends XRI's `XRGrabInteractable` to make the shotgun rod a genuinely two-handed tool.

- **Second-hand grab points.** The rod exposes a list of `XRSimpleInteractable` grab
  points; grabbing one registers a second interactor alongside the primary grab.
- **Two-hand rotation solve.** While both hands hold the rod, its orientation is
  computed each interaction update as a `Quaternion.LookRotation` from the primary
  attach transform toward the second hand, with three configurable up-vector modes
  (`None` / `First` / `Second`) to control roll. One hand aims, the other steers —
  the same way a real hot stick is handled.
- **Midgrab-to-far-grab reach extension.** On second-hand grab, the first-hand attach
  point shifts 1.25 m down the shaft (`OnSecondHandGrab`), effectively letting the
  player choke down on the pole to reach lines high overhead; releasing the second
  hand restores the original grip. An earlier continuous scale-based extension
  survives in the file as commented-out code.
- **Drop recovery.** A dropped rod that stays below its home height starts a ground
  timer; on expiry its velocity is zeroed and it teleports back to its home transform,
  and the drop is reported to the scoring engine via `Tracker.droppedPole()`, so
  fumbled tools count against the run.

## Clamp and power-check sockets

Each power line carries a socket interactor from
[`PowerLines.cs`](Assets/Scripts/ShotgunRod/PowerLines.cs) (an `XRSocketInteractor`
subclass), which does double duty:

- **Power checks.** Hovering the PowerMeter tool over a line fires
  `Tracker.trackEvent("Line N | Power Check")` and deactivates the socket, so a check
  is registered exactly once per line.
- **Clamp placement.** When a clamp's hook hovers, the socket pre-positions its snap
  target to the hook's lateral position along the line. On selection the hook is
  detached from its clamp assembly, snapped to the line, frozen kinematic, and the
  placement event `"Line N | <clamp> | <hook>"` is recorded for order validation.

[`SocketBehavior.cs`](Assets/Scripts/ShotgunRod/SocketBehavior.cs) governs the socket
on the rod's tip that carries tools, and
[`DropScript.cs`](Assets/Scripts/ShotgunRod/DropScript.cs) gives loose tools the same
home-return behavior as the rod.

## Runtime line generation

[`RenderLine.cs`](Assets/Scripts/ShotgunRod/RenderLine.cs) draws the neutral line
between a clamp pair's two hooks with a `LineRenderer` whose endpoints re-anchor to
the hook transforms every frame — so as hooks get grabbed, carried on the rod tip, and
snapped onto power lines, the line follows wherever its endpoints go. Together with the
reach extension, this is the "light procedural elements" of the interaction layer:
geometry generated at runtime from interaction state rather than authored in the scene.

## Scoring and infractions

[`Tracker.cs`](Assets/Scripts/Tracker.cs) is the referee. It consumes the event stream
emitted by the interaction components above and evaluates the run against the safety
procedure:

- **Power-check gate.** All four lines must be checked for power before any clamp is
  placed; clamping early records an `insufficientChecksForLinePower` infraction.
- **Clamp order validation.** The six hook placements must follow the prescribed
  sequence across lines (1 → 2 → 2 → 3 → 1 → 4), and both hooks of a clamp pair must
  land on consistent lines; violations record an `incorrectClampPlacementOrder`
  infraction. Events are deduplicated, so re-checks don't double-count.
- **Work-zone check.** The bucket must be inside the designated work zone when the
  run ends (fed by [`BucketCheck.cs`](Assets/Scripts/Highlight%20scripts/BucketCheck.cs)).
- **Run metrics.** Elapsed time, tool-drop count, and bucket-handle grab count.
- **Report generation.** `FinishButton()` composes the end-of-run report shown to the
  participant: metrics, completed/incomplete tasks, and every infraction, formatted
  for the results panel.

This event-order design kept the scoring logic in one place: interaction components
only *report* what happened; `Tracker` alone knows what "correct" means.

## Experiment integration

The simulation doubles as a human-subjects experiment comparing instruction modalities
(4 instruction/highlight conditions × 2 environment conditions — see the
[research paper](docs/Final%20Research%20Paper.pdf)).
[`PanelManager.cs`](Assets/Scripts/PanelManager.cs) — created by Robin Schniebel, with
my edits to the instruction copy and experiment flow — drives scenario selection and
the per-condition instruction panels, and surfaces `Tracker`'s report as the
participant's results screen.

## Authorship

Per `git log --follow` on the original team repository. Robin's `PACreated/` folder is
XR scaffolding (teleport ray, hand animation, collectables) he brought in when creating
the project.

| Area | Files | Author(s) |
|---|---|---|
| Shotgun rod, clamps, power checks, runtime lines | `ShotgunRod/PowerLines.cs`, `ShotgunRod/RenderLine.cs`, `ShotgunRod/DropScript.cs` | Peter Castelein |
| | `ShotgunRod/TwoHandGrabInteractable.cs`, `ShotgunRod/SocketBehavior.cs` | Peter Castelein (created, majority), Robin Schniebel |
| Scoring / infraction engine | `Tracker.cs` | Peter Castelein (created, 278/303 lines), Robin Schniebel |
| Experiment panels & instructions | `PanelManager.cs` | Robin Schniebel (created, majority), Peter Castelein |
| Bucket & truck controls | `BucketControls/*`, `TruckTP/*`, `TruckMovement.cs` | Robin Schniebel (Peter: minor edits to `TruckMovement.cs`) |
| Highlight system | `Highlight scripts/*` | Robin Schniebel (created), Peter Castelein |
| Sound | `SoundEngine.cs` | Robin Schniebel |
| XR scaffolding | `PACreated/*` | Robin Schniebel |

Environment art and 3D assets by Sarah Luster and Soha Aftab (no code commits).
