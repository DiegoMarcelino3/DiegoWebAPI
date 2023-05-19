using DiegoWebAPI.Domain.Models;
using DiegoWebAPI.Infraestructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DiegoWebAPI.Domain.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly AuthService _authService;

        public PostService(PostRepository postRepository, AuthService authService)
        {
            _postRepository = postRepository;
            _authService = authService;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> list = await _postRepository.ListPosts();

            return list;
        }

        public async Task<List<Post>> ListMeusPosts()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Post> list = await _postRepository.ListPostsByUserId(currentUser.Id);

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post item = await _postRepository.GetPostById(postId);

            if (item == null)
            {
                throw new ArgumentException("Post não existe");
            }
            return item;
        }

        public async Task<Post> NovoPost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post novoPost = new Post();
            novoPost.ApplicationUserId = currentUser.Id;
            novoPost.Data = DateTime.Now;
            novoPost.Titulo = post.Titulo;
            novoPost.Conteudo = post.Conteudo;

            novoPost = await _postRepository.CreatePost(novoPost);

            return novoPost;
        }

        public async Task<int> UpdatePost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post findPost = await _postRepository.GetPostById(post.Id);
            if (findPost == null)
                throw new ArgumentException("Post não existe!");

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permisão");

            findPost.Titulo = post.Titulo;
            findPost.Conteudo = post.Conteudo;

            return await _postRepository.UpdatePost(findPost);
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post findPost = await _postRepository.GetPostById(postId);
            if (findPost == null)
                throw new ArgumentException("Post não existe.");

            if (!findPost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permissão");

            await _postRepository.DeletePostAsync(postId);

            return true;
        }
    }
}
