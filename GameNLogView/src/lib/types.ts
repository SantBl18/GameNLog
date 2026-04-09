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

export type GeneralFilter = {
    id: string,
    name: string
}

export type Genre = {
    id: string,
    name: string
    slug: string
}

export type Platform = {
    id: string,
    name: string,
    slug: string,
    abbreviation: string
}