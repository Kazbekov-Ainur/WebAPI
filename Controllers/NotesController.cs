using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.AspNetCore.OData.Query;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/notes")]
	public class NotesController : ControllerBase
	{
		private readonly NoteContext _context;

		public NotesController(NoteContext context)
		{
			_context = context;
		}

		[HttpPost("CreateNote")] // Создание заметки
		public IActionResult CreateNote([FromBody] Note note)
		{
			_context.Notes.Add(note);
			_context.SaveChanges();
			return Ok();
		}

		[HttpPut("EditNote")] // Редактирование заметки
		public IActionResult UpdateNote(int id, [FromBody] Note note)
		{
			var existingNote = _context.Notes.FirstOrDefault(n => n.Id == id);
			if (existingNote == null)
			{
				return NotFound();
			}

			existingNote.Title = note.Title;
			existingNote.Body = note.Body;
			existingNote.CreatedDate = note.CreatedDate;

			_context.SaveChanges();
			return Ok();
		}

		[HttpDelete("DeleteNote")]  //Удаление заметки
		public IActionResult DeleteNote(int id)
		{
			var note = _context.Notes.FirstOrDefault(n => n.Id == id);
			if (note == null)
			{
				return NotFound();
			}

			_context.Notes.Remove(note);
			_context.SaveChanges();
			return Ok();
		}

		[HttpGet("ViewNotes")] // Вывод списка заметок с поддержкой протокола OData
		[EnableQuery]
		public IQueryable<Note> GetNotes()
		{
			return _context.Notes.AsQueryable();
		}

		[HttpGet("FilterByTitle")] // Фильтрация по заголовку
		[EnableQuery]	
		public IQueryable<Note> GetNotesFilterByTitle(string title)
		{
            var noteFilterByTitle =
				 from n in _context.Notes
				 where n.Title == title
				select n;

            return noteFilterByTitle;

        }

        [HttpGet("FilterByBody")] // Фильтрация по описанию
        [EnableQuery]
        public IQueryable<Note> GetNotesFilterByBody(string body)
        {
            var noteFilterByBody =
                 from n in _context.Notes
                 where n.Body == body
                 select n;

            return noteFilterByBody;

        }

        [HttpGet("FilterByDateCr")] // Фильтрация по дате и времени создания заметки
        [EnableQuery]
        public IQueryable<Note> GetNotesFilterByDateCr(DateTime dateCr)
        {
            var noteFilterByDateCr =
                 from n in _context.Notes
                 where n.CreatedDate == dateCr
                 select n;

            return noteFilterByDateCr;

        }

        [HttpGet("SortById")] // Сортировка по Id
		[EnableQuery]
		public IQueryable<Note> GetNotesSortId()
		{
			return _context.Notes.AsQueryable().OrderBy(x => x.Id);
		}

		[HttpGet("SortByTitle")] // Сортировка по заголовку
		[EnableQuery]
		public IQueryable<Note> GetNotesSortTitle()
		{
			return _context.Notes.AsQueryable().OrderBy(x => x.Title);
		}

		[HttpGet("SortByBody")] // Сортировка по описанию
		[EnableQuery]
		public IQueryable<Note> GetNotesSortBody()
		{
			return _context.Notes.AsQueryable().OrderBy(x => x.Body);
		}

		[HttpGet("SortByDate")] // Сортировка по дате создания
		[EnableQuery]
		public IQueryable<Note> GetNotesSortDate()
		{
			return _context.Notes.AsQueryable().OrderBy(x => x.CreatedDate);
		}

	}
}
