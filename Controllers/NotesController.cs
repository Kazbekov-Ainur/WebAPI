using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.AspNetCore.OData.Query;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteContext noteContext;

        public NotesController(NoteContext noteContext)
        {
            this.noteContext = noteContext;
        }

		[HttpGet]
		[EnableQuery]
		[Route("Получение списка заметок")]
		public IQueryable<Note> GetNotes()
		{
			return noteContext.Notes.AsQueryable();
		}

		[HttpGet]
        [EnableQuery]
        [Route("Сортировка по ID")]
        public IQueryable<Note> GetNotesId()
        {
            return noteContext.Notes.AsQueryable().OrderBy(x => x.Id);
        }

		[HttpGet]
		[EnableQuery]
		[Route("Сортировка по Заголовку")]
		public IQueryable<Note> GetNotesSortTitle()
		{
			return noteContext.Notes.AsQueryable().OrderBy(x => x.Title);
		}

		[HttpGet]
		[EnableQuery]
		[Route("Сортировка по Описанию")]
		public IQueryable<Note> GetNotesSortBody()
		{
			return noteContext.Notes.AsQueryable().OrderBy(x => x.Body);
		}

		[HttpGet]
		[EnableQuery]
		[Route("Сортировка по дате и времени создания")]
		public IQueryable<Note> GetNotesSortDate()
		{
			return noteContext.Notes.AsQueryable().OrderBy(x => x.CreatedDate);
		}

		[HttpPost]
        [Route("Добавление заметки")]
        public string AddNote(Note note)
        {
            string response = string.Empty;
            noteContext.Notes.Add(note);
            noteContext.SaveChanges();

            return "Note added";
        }

        [HttpPut]
        [Route("Редактирование заметки")]
        public string EditNote(Note note) 
        {
            noteContext.Entry(note).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            noteContext.SaveChanges();

            return "Note Edited";
        }

        [HttpDelete]
        [Route("Удаление заметки")]
        public string DeleteNote(int id)
        {
            Note note = noteContext.Notes.Where(x => x.Id == id).FirstOrDefault();
            if(note != null)
            {
                noteContext.Notes.Remove(note);
                noteContext.SaveChanges();
                return "Note deleted";
            }
            else
            {
                return "Note Not Found";
            }

        }
    }
}
