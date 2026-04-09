import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import "../globals.css"
import type { GameSummary, PaginatedResponse } from "../lib/types";
import { fetchGames } from "../lib/restAPI";
import GameGrid from "../components/GameGrid";
import Pagination from "../components/Pagination";
import SearchBar from "../components/SearchBar";

export default function Games() {
    const [games, setGames] = useState<GameSummary[]>([]);
    const [searchParams, setSearchParams] = useSearchParams();
    const [totalPages, setTotalPages] = useState(1);

    const page = parseInt(searchParams.get("page") ?? "1");
    const search = searchParams.get("search") ?? "";
    const genresParams = searchParams.getAll("genres")
    const platformsParams = searchParams.getAll("platforms")

    useEffect(() => {
        const delayDebounce = setTimeout(() => {
            fetchGames(page, search).then((data: PaginatedResponse<GameSummary>) => {
            setGames(data.data);
            setTotalPages(Math.ceil(data.totalCount / data.pageSize));
            });
        }, 500)
        return () => clearTimeout(delayDebounce);
    }, [page, search]);

    function handlePageChange(newPage: number){
        setSearchParams(prev => {
            prev.set("page", newPage.toString());
            return prev;
        })
    }

    function handleSearchChange(search: string){
        setSearchParams(prev => {
            if (search !== "")
                prev.set("search", search);
            else
                prev.delete("search")
            prev.set("page", "1");
            return prev;
        })
    }

    function handleGenresChange(genres: string[]){
        
    }

    return (
        <>
            <SearchBar
                search={search} 
                onSearchChange={handleSearchChange}
            />
            <Pagination
                currentPage={page}
                totalPages={totalPages}
                onPageChange={handlePageChange}
            />
            <GameGrid games={games}></GameGrid>
            <Pagination
                currentPage={page}
                totalPages={totalPages}
                onPageChange={handlePageChange}
            />
        </>
    )
}