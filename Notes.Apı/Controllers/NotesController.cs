using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Apı.Data;
using Notes.Apı.Models.Entities;

namespace Notes.Apı.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await _context.Notes.ToListAsync());
        }
        [HttpGet]
        [Route("{id:Guid}")]//Type güvenli olması için bu şekilde de belirtebilirim
        [ActionName("GetNoteById")]
        public async Task<IActionResult> GetNoteById([FromRoute]Guid id)
        {
            var Note = await _context.Notes.FindAsync(id);
            return (Note == null) ? NotFound() : Ok(Note);
        }
        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();//kullanıcıdan gelen note nesnesine bir id atıyorum
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNoteById), new {id=note.Id},note);//oluşturduğum entity'i geri dönüyorum
        }
        [HttpPut]
        [Route("{id:Guid}")]
       
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id,[FromBody] Note note)
        {
            var ExistingNote = await _context.Notes.FindAsync(id);
            if (ExistingNote == null)
                return NotFound();
            ExistingNote.Id = note.Id;
            ExistingNote.Description = note.Description;
            ExistingNote.Title = note.Title;
            await _context.SaveChangesAsync();
            return Ok(ExistingNote);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNotes([FromRoute] Guid id)
        {
            var Note=await _context.Notes.FindAsync(id);
            if (Note == null)
                return NotFound();
            _context.Notes.Remove(Note);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
