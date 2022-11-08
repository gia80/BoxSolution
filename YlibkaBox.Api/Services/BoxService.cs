using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using YlibkaBox.Api.Domain.Contracts;

namespace YlibkaBox.Api.Services;

public class BoxService : IBoxService
{
    private readonly IBoxRepository _boxRepository;
    private readonly ILogger<BoxService> _logger;
    private readonly IObjectResultFactory _response;

    public BoxService(ILogger<BoxService> logger,
        IObjectResultFactory objectResultFactory, IBoxRepository boxRepository)
    {
        _logger = logger;
        _response = objectResultFactory;
        _boxRepository = boxRepository;
    }


    public async Task<ObjectResult> GetDocument(int id)
    {
        try
        {
            var box = await _boxRepository.GetBox(id);

            if (box == null)
                return _response[HttpStatusCode.NotFound, "Поле не найдено с указанным идентификатором"];

            var bytes = Encoding.ASCII.GetBytes($"{box.Id},{box.Specification}");

            return _response[HttpStatusCode.OK,
                new FileContentResult(bytes, "text/csv") { FileDownloadName = $"record-{id}.csv" }];
        }
        catch (Exception e)
        {
            _logger.LogError("{@Exception} {@Message}", e, "Ошибка извлечения поля по идентификатору");
            return _response[HttpStatusCode.InternalServerError, "Ошибка извлечения поля по идентификатору"];
        }
    }


    public async Task<ObjectResult> GetDocuments()
    {
        try
        {
            var list = await _boxRepository.GetBoxes();

            var stringBuilder = new StringBuilder();

            foreach (var box in list)
                stringBuilder.AppendLine($"{box.Id},{box.Specification}");

            var bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());

            return _response[HttpStatusCode.OK,
                new FileContentResult(bytes, "text/csv") { FileDownloadName = "records.csv" }];
        }
        catch (Exception e)
        {
            _logger.LogError("{@Exception} {@Message}", e, "Ошибка при извлечении коробок");
            return _response[HttpStatusCode.InternalServerError, "Ошибка при извлечении коробок"];
        }
    }
}