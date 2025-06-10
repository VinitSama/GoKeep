using GoKeep.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository
{
    public class NotesRL : INotesRL
    {
        private readonly DatabaseContext _context;
        public NotesRL(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> AddNotes(string email, AddNoteRequestModel noteModel)
        {
            try
            {
                //var result = await _context.Notes.FromSqlRaw($"EXEC CreateNewNote @Email, @Title, @Content, @IsPinned, @IsArchived, @Istrashed",
                //    new SqlParameter("@Email", email),
                //    new SqlParameter("@Title", noteModel.Title),
                //    new SqlParameter("@Content", noteModel.Content),
                //    new SqlParameter("@IsPinned", noteModel.IsPinned),
                //    new SqlParameter("@IsArchived", noteModel.IsArchived),
                //    new SqlParameter("@Istrashed", noteModel.IsTrashed)).ToListAsync();
                //if (result.Any())
                //{
                //    return result[0].Id;
                //}
                //return -1;


                var user = await _context.UsersKeep.FirstOrDefaultAsync(r => r.Email == email && r.IsActive);
                if (user == null)
                {
                    return -1;
                }
                var newNote = new NoteEntity
                {
                    UserId = user.Id,
                    Title = noteModel.Title,
                    Content = noteModel.Content,
                    IsPinned = noteModel.IsPinned,
                    IsArchived = noteModel.IsArchived,
                    IsTrashed = noteModel.IsTrashed,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                var result = await _context.Notes.AddAsync(newNote);
                if (result != null)
                {
                    await _context.SaveChangesAsync();
                    return result.Entity.Id;
                }
                return -1;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteForever(int noteId)
        {
            try
            {
                //var result = await _context.Database.ExecuteSqlRawAsync($"EXEC DeleteNoteByIdForever @NoteId",
                //    new SqlParameter("@NoteId", noteId));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                var note = await _context.Notes.FirstOrDefaultAsync(r => r.Id == noteId && !r.DeleteForever);
                if (note == null)
                {
                    return false;
                }
                note.DeleteForever = true;
                note.UpdatedAt = DateTime.UtcNow;
                _context.Notes.Update(note);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteNotes(int noteId)
        {
            try
            {
                //var result = await _context.Database.ExecuteSqlRawAsync($"EXEC DeleteNoteById @NoteId",
                //    new SqlParameter("@NoteId", noteId));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                var note = await _context.Notes.FirstOrDefaultAsync(r => r.Id == noteId && !r.DeleteForever);
                if (note == null)
                {
                    return false;
                }
                note.IsTrashed = !note.IsTrashed;
                note.UpdatedAt = DateTime.UtcNow;
                _context.Notes.Update(note);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllNotesResponseModel>> GetAllNotes(string email)
        {
            var response = new List<GetAllNotesResponseModel>();
            try
            {
                var user = await _context.UsersKeep.FirstOrDefaultAsync(r => r.Email == email && r.IsActive);
                if (user == null)
                {
                    return response;
                }
                var records = await _context.Notes
                    .Where(r => r.UserId == user.Id && !r.DeleteForever)
                    .OrderBy(r => r.UpdatedAt)
                    .ToListAsync();
                records.Reverse();
                if (records.Any())
                {
                    foreach (var record in records)
                    {
                        var labels = await _context.NotesLabels
                            .Where(r => r.UserId == user.Id && r.NoteId == record.Id && r.IsActive)
                            .ToListAsync();
                        var labelArray = new List<int>();
                        foreach (var label in labels)
                        {
                            labelArray.Add(label.LabelId);
                        }
                        response.Add(new GetAllNotesResponseModel()
                        {
                            NoteId = record.Id,
                            Title = record.Title,
                            Content = record.Content,
                            IsArchived = record.IsArchived,
                            IsPinned = record.IsPinned,
                            IsTrashed = record.IsTrashed,
                            CreatedAt = record.CreatedAt,
                            UpdatedAt = record.UpdatedAt,
                            IsActive = true,
                            LabelIds = labelArray
                        });
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ToggleArchiveNote(int noteId)
        {
            try
            {
                //var result = await _context.Database.ExecuteSqlRawAsync($"EXEC ToggleArchiveNoteById @NoteId",
                //    new SqlParameter("@NoteId", noteId));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                var note = await _context.Notes.FirstOrDefaultAsync(r => r.Id == noteId && !r.DeleteForever);
                if (note == null)
                {
                    return false;
                }
                note.IsArchived = !note.IsArchived;
                note.UpdatedAt = DateTime.UtcNow;
                _context.Notes.Update(note);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> TogglePinNote(int noteId)
        {
            try
            {
                //var result = await _context.Database.ExecuteSqlRawAsync($"EXEC TogglePinNoteById @NoteId",
                //    new SqlParameter("@NoteId", noteId));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                var note = await _context.Notes.FirstOrDefaultAsync(r => r.Id == noteId && !r.DeleteForever);
                if (note == null)
                {
                    return false;
                }
                note.IsPinned = !note.IsPinned;
                note.UpdatedAt = DateTime.UtcNow;
                _context.Notes.Update(note);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ToggleTrashNote(int noteId)
        {
            try
            {
                //var result = await _context.Database.ExecuteSqlRawAsync($"EXEC ToggleTrashNoteById @NoteId",
                //    new SqlParameter("@NoteId", noteId));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                var note = await _context.Notes.FirstOrDefaultAsync(r => r.Id == noteId && !r.DeleteForever);
                if (note == null)
                {
                    return false;
                }
                note.IsTrashed = !note.IsTrashed;
                note.UpdatedAt = DateTime.UtcNow;
                _context.Notes.Update(note);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateNotes(int noteId, UpdateNoteRequestModel noteModel)
        {
            try
            {
                //var result = await _context.Database.ExecuteSqlRawAsync($"EXEC UpdateNoteById @NoteId, @Title, @Content",
                //    new SqlParameter("@NoteId", noteId),
                //    new SqlParameter("@Title", noteModel.Title ?? (object)DBNull.Value),
                //    new SqlParameter("@Content", noteModel.Content ?? (object)DBNull.Value));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                if (noteModel == null)
                {
                    return false;
                }
                var note = await _context.Notes.FirstOrDefaultAsync(r => r.Id == noteId && !r.DeleteForever);
                if (note == null)
                {
                    return false;
                }
                if (!string.IsNullOrEmpty(noteModel.Title))
                {
                    note.Title = noteModel.Title;
                }
                if (!string.IsNullOrEmpty(noteModel.Content))
                {
                    note.Content = noteModel.Content;
                }
                note.UpdatedAt = DateTime.UtcNow;
                _context.Notes.Update(note);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
