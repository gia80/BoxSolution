using Microsoft.AspNetCore.Mvc;

namespace YlibkaBox.Api.Domain.Contracts;

public interface IBoxService
{
    Task<ObjectResult> GetDocument(int id);
    Task<ObjectResult> GetDocuments();
}