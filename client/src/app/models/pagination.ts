export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

// T is the actual list of results 
export class PaginatedResult<T> {
    result?: T
    pagination?: Pagination;
}