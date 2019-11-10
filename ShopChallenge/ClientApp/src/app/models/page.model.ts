export interface PageModel<T> {
    page: number;
    pageSize: number;
    data: ReadonlyArray<T>;
    dataCount: number;
}
