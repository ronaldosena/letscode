namespace Application.Common.Interfaces
{
    public interface IResponseAccessor
    {
        void AddToResponse(string name, object content);
        void AddPagination(Pagination.PaginationHeader header);
    }
}
