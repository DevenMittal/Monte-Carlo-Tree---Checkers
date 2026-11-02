# Monte Carlo Checkers

I wrote a console checkers experiment in C# to explore Monte Carlo tree search. The computer chooses moves by selecting nodes with UCT, expanding possible moves, simulating play, and backpropagating outcomes.

## Demo

Run a short, deterministic board-state demo without typing input:

```sh
dotnet run --project "Monte Carlo Tree - Checkers/Monte Carlo Tree - Checkers.csproj" -c Release -- --demo
```

The captured output is in [`docs/checkers-demo.txt`](docs/checkers-demo.txt). It shows the opening position, one legal human move, and the computer's response after 1,600 MCTS iterations. It is a snapshot, not a complete match.

There is also a short visual recording: [`docs/checkers-demo.mp4`](docs/checkers-demo.mp4).

![Checkers demo: three turns](docs/checkers-demo-three-turns-v2.gif)

## Search and game loop

`GameMaster<T>` separates the search from the game-state interface. `Checkers` supplies board states and successors. The player-facing loop uses **1,600 search iterations** for the computer’s response.

```mermaid
flowchart LR
  A[Select with UCT] --> B[Expand node]
  B --> C[Random rollout]
  C --> D[Backpropagate outcome]
  D --> A
```

This is a learning implementation, not a complete or tournament-tested checkers engine. Human move validation is simplified, and tree reuse needs further checking across turns.

## Run locally

The console project targets **.NET 6** and declares no third-party package dependencies.

```sh
dotnet run --project "Monte Carlo Tree - Checkers/Monte Carlo Tree - Checkers.csproj" -c Release
```

The board uses zero-based indices. Enter **four integers, one per line**: source row, source column, destination row, destination column. Despite some variable names saying X/Y, the first coordinate indexes a board row.

An opening move from row 5, column 0 to row 4, column 1 is:

```text
5
0
4
1
```

The computer then searches and redraws the board. Keep entering moves, or use **Ctrl+C** to stop.

## Verification and limits

The Release build passes on macOS. Interactive input now exits cleanly at end-of-input, and the demo uses a fixed seed so the captured board sequence can be reproduced. This checks one response, not a complete game or the correctness of every rule.

## Code guide

| File in `Monte Carlo Tree - Checkers/` | Responsibility |
| --- | --- |
| `Program.cs` | Console board and player input |
| `Play.cs` | Player moves and search budget |
| `GameMaster.cs` | Selection, expansion, simulation, backpropagation |
| `Node.cs` | Tree nodes and UCT |
| `Checkers.cs`, `IGamestate.cs` | Board rules and search-state contract |
