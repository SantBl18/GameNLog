import "../globals.css"
import type { GameSummary } from "../lib/types";
import GameCard from "./GameCard";

export default function GameGrid({games}: {games: GameSummary[]}) {
    return (
         <div className="p-4">
            <div className="grid grid-cols-10 gap-4">
                {games.map(game => (
                    <GameCard key={game.id} id={game.id} coverURL={game.coverURL} name={game.name} />
                ))}
            </div>
        </div>
    );
}