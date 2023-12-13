﻿using Microsoft.EntityFrameworkCore;
using harjoitustyoBackend.Models;

namespace harjoitustyoBackend.Models
{
    public class MessageServiceContext : DbContext
    {
        public MessageServiceContext(DbContextOptions<MessageServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<harjoitustyoBackend.Models.User>? User { get; set; }

    }
}

