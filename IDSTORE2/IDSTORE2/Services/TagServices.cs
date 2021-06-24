using IDSTORE2.Data;
using IDSTORE2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = IDSTORE2.Models.File;

namespace IDSTORE2.Services
{
    public class TagServices
    {
        private readonly APIContext context;

        public TagServices(APIContext _context)
        {
            context = _context;
        }

        public async Task<List<Tag>> GetTagByFile(Guid _fileID)
        {
            try
            {
                File file = context.File.FirstOrDefault(f => f.FileId == _fileID);
                if (file == null) return null;
                else
                {
                    return file.Tags.ToList();
                }
            }
            catch
            {
                return null;
            }
        }
        public async Task<String[]> GetAllTagName()
        {
            try
            {
                var tags = await GetAllTag();
                List<String> result = new List<String>(tags.Count());
                foreach(Tag _tag in tags)
                {
                    result.Add(_tag.Name);
                }
                return result.ToArray();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Tag>> GetAllTag()
        {
            try
            {
                return await context.Tag.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<Boolean> AddTag(String _nameTag, string _description)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_nameTag)) return false;
                
                Tag tag = new Tag();
                tag.Name = _nameTag;
                tag.Description = _description;

                await context.Tag.AddAsync(tag);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Boolean> UpdateTag(Tag _tag)
        {
            try
            {
                context.Tag.Update(_tag);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Boolean> DeleteTag(Tag _tag)
        {
            try
            {
                context.Tag.Remove(_tag);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
