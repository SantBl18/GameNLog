import "../globals.css"

interface PaginationProps {
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
}

export default function Pagination({ currentPage, totalPages, onPageChange }: PaginationProps) {
    return (
        <div className="flex justify-between items-center">
            { currentPage != 1 ? (
                <button 
                    onClick={() => onPageChange(currentPage - 1)}
                    disabled={currentPage === 1}
                >
                    Previous
                </button>
                ) : null
            }
            <span>{currentPage} / {totalPages}</span>
            { currentPage != totalPages ? (
                <button 
                    onClick={() => {
                        onPageChange(currentPage + 1);
                    }}
                    disabled={currentPage === totalPages}
                >
                Next
                </button>
            ) : null
        }
            
        </div>
    );
}