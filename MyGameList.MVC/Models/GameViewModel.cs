﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using MyGameList.Domain.Request;
using System.ComponentModel.DataAnnotations;

namespace MyGameList.Models
{
    public class GameViewModel
    {
        public Guid UserId { get; set; }
        public List<Game> Games { get; set; }
        public GameViewModel()
        {
            this.Games = new List<Game>();
        }
    }

    public class Game
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(1, 10)]
        public decimal Rating { get; set; }
        [Required]
        public string? Genre { get; set; }
        public GameRequestDTO CreateDto()
        {
            return new GameRequestDTO
            {
                Id = Id,
                Name = Name,
                Rating = Rating,
                Genre = Genre
            };
        }
    }
}
