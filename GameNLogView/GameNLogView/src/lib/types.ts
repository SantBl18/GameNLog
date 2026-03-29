export type PaginatedResponse<T> = {
    page: number
    pageSize: number
    totalCount: number
    data: T[]
}

export type GameSummary = {
    id: string,
    name: string,
    coverURL: string,
    averageRating: number
}