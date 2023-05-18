using DiegoWebAPI.Domain.Models;
using DiegoWebAPI.Infraestructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DiegoWebAPI.Domain.Servicies
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> list = await _postRepository.ListPosts();

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post item = await _postRepository.GetPost(postId);

            if (item == null)
            {
                throw new ArgumentException("Post não existe");
            }
            return item;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.Data = DateTime.Now;

            post = await _postRepository.CreatePost(post);

            return post;
        }

        public async Task<int> UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }

        public async Task<bool> DeletePost(int postId)
        {
            Post findPost = await _postRepository.GetPost(postId);
            if (findPost == null)
                throw new ArgumentException("Post não existe.");

            await _postRepository.DeletePost(postId);

            return true;
        }
    }
}
