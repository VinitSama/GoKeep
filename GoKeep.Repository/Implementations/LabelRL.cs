using GoKeep.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository
{
    public class LabelRL : ILabelRL
    {
        private readonly DatabaseContext _context;
        public LabelRL(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> CreateNewLabel(string email, CreateLabelRequestModel labelModel)
        {
            try
            {
                //var result = await _context.Labels.FromSqlRaw("EXEC CreateNewLabel @Email, @LabelName",
                //        new SqlParameter("@Email", email),
                //        new SqlParameter("@LabelName", labelModel.label)).ToListAsync();
                //if (result.Any())
                //{
                //    return result[0].Id;
                //}
                //return -999;

                var user = await _context.UsersKeep
                    .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
                if (user == null)
                {
                    return -999; // User not found or inactive
                }
                var label = await _context.Labels
                    .FirstOrDefaultAsync(l => l.Name == labelModel.label && l.UserId == user.Id);
                if (label != null)
                {
                    // Label already exists for this user
                    if (label.IsActive == false)
                    {
                        // Reactivate the label if it was previously inactive
                        label.IsActive = true;
                        _context.Labels.Update(label);
                        await _context.SaveChangesAsync();
                    }
                    return label.Id;
                }
                var newLabel = new LabelEntity
                {
                    Name = labelModel.label,
                    UserId = user.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                };
                _context.Labels.Add(newLabel);
                await _context.SaveChangesAsync();
                return newLabel.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLabel(int labelId)
        {
            try
            {
                //var result = await _context.Database
                //    .ExecuteSqlRawAsync("EXEC DeleteLabelById @LabelId",
                //        new SqlParameter("@LabelId", labelId));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                var label = await _context.Labels
                    .FirstOrDefaultAsync(l => l.Id == labelId && l.IsActive);
                if (label == null)
                {
                    return false; // Label not found or already inactive
                }
                label.IsActive = false; // Soft delete by setting IsActive to false
                _context.Labels.Update(label);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllLabelResponseModel>> GetAllLabel(string email)
        {
            var result = new List<GetAllLabelResponseModel>();
            try
            {
                //var records = await _context.Labels
                //    .FromSqlRaw("EXEC GetAllLabelByEmail @Email",
                //        new SqlParameter("@Email", email))
                //    .ToListAsync();
                //if (records.Any())
                //{
                //    foreach (var record in records)
                //    {
                //        result.Add(new GetAllLabelResponseModel()
                //        {
                //            Id = record.Id,
                //            Name = record.Name,
                //            CreatedAt = record.CreatedAt
                //        });
                //    }
                //}
                //return result;

                var user = await _context.UsersKeep
                    .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
                if (user == null)
                {
                    return result; // Return empty if user not found
                }
                var labels = await _context.Labels
                    .Where(l => l.UserId == user.Id && l.IsActive)
                    .Select(l => new GetAllLabelResponseModel
                    {
                        Id = l.Id,
                        Name = l.Name,
                        CreatedAt = l.CreatedAt
                    })
                    .ToListAsync();
                return labels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateLabel(UpdateLabelRequestModel labelModel)
        {
            try
            {
                //var result = await _context.Database
                //    .ExecuteSqlRawAsync("EXEC UpdateLabelById @LabelId, @LabelName",
                //        new SqlParameter("@LabelId", labelModel.Id),
                //        new SqlParameter("@LabelName", labelModel.Name));
                //if (result > 0)
                //{
                //    return true;
                //}
                //return false;

                var label = await _context.Labels
                    .FirstOrDefaultAsync(l => l.Id == labelModel.Id && l.IsActive);
                if (label == null)
                {
                    // Label not found or already inactive
                    return false;
                }
                label.Name = labelModel.Name;
                _context.Labels.Update(label);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
