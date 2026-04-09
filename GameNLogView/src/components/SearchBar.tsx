interface SearchProps{
    search: string
    onSearchChange: (search: string) => void;
}

export default function SearchBar({search, onSearchChange} : SearchProps){
    return (
        <div>
            <input
                type="text"
                value={search ?? ""}
                onChange={(e) => onSearchChange(e.target.value)}
            />
        </div>
    )
}