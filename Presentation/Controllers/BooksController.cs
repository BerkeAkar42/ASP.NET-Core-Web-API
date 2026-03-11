using Entities.Models;
using Services.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.DataTransferObjects;
using Presentation.ActionFilters;
using Entities.RequestFeatures;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))] //Uygulama bazlı loglama yapıyoruz. Eğer bir API'ın üzerine eklersek bunu, API bazlı loglama yapar. Bu baştan sona tüm log ifadelerini tutar.
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }


        [HttpGet] //[FromQuery] --> api/books?pageNumber=2&pageSize=10 --> Buradaki ? den sonra gelen değerleri kendisine alır.
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
            var pagedResult = await _manager.BookService.GetAllBooksAsync(bookParameters, false);

            //Pagenation işlemini kolaylaştıran bir yapıdır.
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData)); //MetaData verilerini frontend tarafına JSON formatında verecektir.

            return Ok(pagedResult.books);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {
            var book = await _manager.BookService
                .GetOneBookByIdAsync(id, false);

            return Ok(book);

        }


        [ServiceFilter(typeof(ValidationFilterAttribute))] //Burası kod çalışmadan önce gelen verinin global bir filtreden geçmesini sağlıyor.
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {
            var book = await _manager.BookService.CreateOneBookAsync(bookDto);
            return StatusCode(201, book);
        }


        //order = 1 kullanırsan birden fazla attribute'ları çalışma sırasını ayarlamış olursun.
        [ServiceFilter(typeof(ValidationFilterAttribute), Order = 1)] //Burası kod çalışmadan önce gelen verinin global bir filtreden geçmesini sağlıyor.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);
            return NoContent(); //204  
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();
        }


        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest(); //400 

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

            return NoContent(); //204
        }

    }
}
