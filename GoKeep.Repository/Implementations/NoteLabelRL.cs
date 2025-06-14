using GoKeep.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository
{
    public class NoteLabelRL : INoteLabelRL
    {
        private readonly DatabaseContext _context;
        public NoteLabelRL(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNoteLabel(NoteLabelRequestModel notelabelModel, string email)
        {
            try
            {
                var user = await _context.UsersKeep
                    .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
                if (user == null)
                {
                    return false; // Return false if user not found
                }
                var existingNoteLabel = await _context.NotesLabels
                    .FirstOrDefaultAsync(nl => nl.NoteId == notelabelModel.NoteId && nl.LabelId == notelabelModel.LabelId && nl.UserId == user.Id);
                if (existingNoteLabel != null)
                {
                    existingNoteLabel.IsActive= true; // Reactivate if it exists
                    _context.NotesLabels.Update(existingNoteLabel);
                    await _context.SaveChangesAsync();
                    return true;
                }
                var newNoteLabel = new NotesLabelEntity
                {
                    NoteId = notelabelModel.NoteId,
                    LabelId = notelabelModel.LabelId,
                    UserId = user.Id,
                };
                var result = await _context.NotesLabels.AddAsync(newNoteLabel);
                if (result != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteNoteLabel(NoteLabelRequestModel notelabelModel)
        {
            try
            {
                var noteLabel = await _context.NotesLabels
                    .FirstOrDefaultAsync(nl => nl.NoteId == notelabelModel.NoteId && nl.LabelId == notelabelModel.LabelId);
                if (noteLabel != null)
                {
                    noteLabel.IsActive = false; // Soft delete by setting IsActive to false
                    _context.NotesLabels.Update(noteLabel);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetLabelIdByNoteIdResponseModel>> GetLabelIdByNoteId(int noteId)
        {
            var response = new List<GetLabelIdByNoteIdResponseModel>();
            try
            {
                var result = await _context.NotesLabels
                    .Where(nl => nl.NoteId == noteId && nl.IsActive)
                    .Select(nl => new GetLabelIdByNoteIdResponseModel
                    {
                        LabelId = nl.LabelId
                    })
                    .ToListAsync();
                if (result.Any())
                {
                    response.AddRange(result);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllNoteIdByLabelIdModel>> GetNoteIdByLabelId(int labelID)
        {
            var response = new List<GetAllNoteIdByLabelIdModel>();
            try
            {
                var result = await _context.NotesLabels
                    .Where(nl => nl.LabelId == labelID && nl.IsActive)
                    .Select(nl => new GetAllNoteIdByLabelIdModel
                    {
                        NoteId = nl.NoteId
                    })
                    .ToListAsync();
                if (result.Any())
                {
                    response.AddRange(result);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetNotesLabelResponseModel>> GetNoteLable(string email)
        {
            var response = new List<GetNotesLabelResponseModel>();
            try
            {
                var user = await _context.UsersKeep
                    .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
                if (user == null)
                {
                    return response; // Return empty if user not found
                }
                var result = await _context.NotesLabels
                    .Where(nl => nl.Note.UserId == user.Id && nl.IsActive)
                    .Select(nl => new GetNotesLabelResponseModel
                    {
                        NoteId = nl.NoteId,
                        LabelId = nl.LabelId
                    })
                    .ToListAsync();

                if (result.Any())
                {
                    foreach (var noteLabel in result)
                    {
                        response.Add(new GetNotesLabelResponseModel()
                        {
                            NoteId = noteLabel.NoteId,
                            LabelId = noteLabel.LabelId
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
    }
}
