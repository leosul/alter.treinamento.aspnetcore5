using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace alter.treinamento.api.ViewModel
{
    public class PaginationViewModel
    {
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("totalItemCount")]
        public int TotalItemCount { get; set; }

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }

        [JsonPropertyName("isFirstPage")]
        public bool IsFirstPage { get; set; }

        [JsonPropertyName("isLastPage")]
        public bool IsLastPage { get; set; }

        [JsonPropertyName("firstItemOnPage")]
        public int FirstItemOnPage { get; set; }

        [JsonPropertyName("lastItemOnPage")]
        public int LastItemOnPage { get; set; }
    }

    public class PaginationItems<T>
    {
        [JsonPropertyName("pagination")]
        public PaginationViewModel Pagination { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; }
    }
}
