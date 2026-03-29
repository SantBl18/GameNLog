import "../globals.css"

interface GameCardProps {
  id: string;
  coverURL: string;
  name: string;
}

export default function GameCard({ id, coverURL, name }: GameCardProps) {
    return (
        <div key={id} className="flex flex-col overflow-hidden items-center text-center">
            <div className="w-33 h-44 bg-gray-200">
                <img
                src={coverURL}
                className="w-full h-full object-contain"
                /> 
            </div>
            <p>{name}</p>
        </div>
    );
}