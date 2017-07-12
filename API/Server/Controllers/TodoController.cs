using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using FluToDo.Api.Repositories.Core;
using FluToDo.Api.Core.Interfaces;
using FluToDo.Api.Core.Models;

namespace FluToDo.Api.Server.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : ApiController
    {
        public TodoController()
        {
            if (TodoItems == null)
            {
                TodoItems = new TodoRepository();
            }
        }

        public static ITodoRepository TodoItems { get; set; }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public TodoItem GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            item.Key = new System.Guid().ToString();
            TodoItems.Add(item);
            return Ok(item.Key);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Update(item);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Remove(id);
            return Ok();
        }
    }
}
