import { PagingOptions } from "./paging-options";

export class SearchResults<T>
{
    public items: T[];

    public pageOptions: PagingOptions;
}