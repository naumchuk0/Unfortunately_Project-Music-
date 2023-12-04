using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class MusicApp : IdentityDbContext
    {
        public MusicApp() { }
        public MusicApp(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=DESKTOP-1L37OVF\\SQLEXPRESS;Initial Catalog=MusicApp; Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData(new[]
            {
                new Genre() {Id = 1, Name="Rock"},
                new Genre() {Id = 2, Name="Pop"},
                new Genre() {Id = 3, Name="Hip hop"},
                new Genre() {Id = 4, Name="Funk"},
                new Genre() {Id = 5, Name="Disco"},
                new Genre() {Id = 6, Name="Electronic"},
                new Genre() {Id = 7, Name="Heavy metal"},
                new Genre() {Id = 8, Name="Jazz"},
                new Genre() {Id = 9, Name="Techno"}
            });
            modelBuilder.Entity<Artist>().HasData(new[]
            {
                new Artist() {Id = 1, FirstName="Drake", LastName=""},
                new Artist() {Id = 2, FirstName="Mariah", LastName="Carey"},
                new Artist() {Id = 3, FirstName="Elton", LastName="John"},
                new Artist() {Id = 4, FirstName="Lady", LastName="Gaga"},
                new Artist() {Id = 5, FirstName="Ed", LastName="Sheeran"},
                new Artist() {Id = 6, FirstName="Alison", LastName="Swift"},
                new Artist() {Id = 7, FirstName="Eminem", LastName=""}
            });
            modelBuilder.Entity<Playlist>().HasData(new[]
            {
                new Playlist() {Id = 1, Name="Playlist#1"},
                new Playlist() {Id = 2, Name="Playlist#2"},
                new Playlist() {Id = 3, Name="Playlist#3"},
                new Playlist() {Id = 4, Name="Playlist#4"},
                new Playlist() {Id = 5, Name="Playlist#5"},
            });
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
