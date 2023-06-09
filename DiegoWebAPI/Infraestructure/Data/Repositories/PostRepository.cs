﻿using DiegoWebAPI.Domain.Models;
using DiegoWebAPI.Domain.Services;
using DiegoWebAPI.Infraestructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DiegoWebAPI.Infraestructure.Data.Repositories
{
    public class PostRepository
    {
        private readonly MySQLContext _context;

        public PostRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> list = await _context.Post.OrderBy(p => p.Data).Include(p => p.ApplicationUser).ToListAsync();


            return list;
        }

        public async Task<List<Post>> ListPostsByUserId(string userId)
        {
            List<Post> list = await _context.Post
                .Where(p => p.ApplicationUserId
                .Equals(userId))
                .OrderBy(p => p.Data)
                .ToListAsync();


            return list;
        }

        public async Task<Post> GetPostById(int postId)
        {
            Post post = await _context.Post
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.Id == postId);

            return post;
        }

        public async Task<Post> CreatePost(Post post)
        {
            var ret = await _context.Post.AddAsync(post);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<int> UpdatePost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var item = await _context.Post.FindAsync(postId);
            _context.Post.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
