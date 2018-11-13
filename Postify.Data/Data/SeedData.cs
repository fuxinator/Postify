using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postify.Data;
using Postify.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Postify.Data.Models
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Posts.Any())
                return;

            var users = new[]
            {
                new ApplicationUser
                {
                    UserName = "ManuelFuchs",
                    NormalizedUserName = "MANUELFUCHS",
                    Email = "m.fuchs@email.com",
                    NormalizedEmail = "M.FUCHS@EMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                new ApplicationUser
                {
                    UserName = "DominikBauer",
                    NormalizedUserName = "DOMINIKBAUER",
                    Email = "dominik.bauer@email.com",
                    NormalizedEmail = "DOMINIK.BAUER@EMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                new ApplicationUser
                {
                    UserName = "FranzSchmidt",
                    NormalizedUserName = "FRANZSCHMIDT",
                    Email = "franz.schmidt@email.com",
                    NormalizedEmail = "FRANZ.SCHMIDT@EMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                new ApplicationUser
                {
                    UserName = "BernhardBauer",
                    NormalizedUserName = "BERNHARDBAUER",
                    Email = "bernhard.bauer@email.com",
                    NormalizedEmail = "BERNHARD.BAUER@EMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                new ApplicationUser
                {
                    UserName = "HubertGoisern",
                    NormalizedUserName = "HUBERTGOISERN",
                    Email = "hubert.goisern@email.com",
                    NormalizedEmail = "HUBERT.GOISERN@EMAIL.COM",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            };

            foreach (var user in users)
                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "Test123!");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(context);
                    var result = userStore.CreateAsync(user);
                }

            InitializePosts(context);
        }

        private static async void InitializePosts(ApplicationDbContext context)
        {
            var applicationUsers = await context.Users.ToListAsync();
            var manuel = applicationUsers[0];
            var dominik = applicationUsers[1];
            var franz = applicationUsers[2];
            var bauer = applicationUsers[3];
            var hubert = applicationUsers[4];

            var comment1 = new Comment
            {
                Content = "I don't deserve this award, but I have arthritis and I don't deserve that either.",
                User = manuel,
                Published = DateTime.Now.AddHours(5)
            };
            var comment2 = new Comment
            {
                Content = "He is great who is what he is from Nature, and who never reminds us of others.",
                User = dominik,
                Published = DateTime.Now.AddHours(5)
            };
            var comment3 = new Comment
            {
                Content = "There can be no real freedom without the freedom to fail.",
                User = franz,
                Published = DateTime.Now.AddHours(5)
            };
            var comment4 = new Comment
            {
                Content = "Careless she is with artful care, Affecting to seem unaffected.",
                User = bauer,
                Published = DateTime.Now.AddHours(5)
            };
            var comment5 = new Comment
            {
                Content = "There is no man living that can not do more than he thinks he can.",
                User = hubert,
                Published = DateTime.Now.AddHours(5)
            };
            var comment6 = new Comment
            {
                Content = "Not to discover weakness is The Artifice of strength.",
                User = manuel,
                Published = DateTime.Now.AddHours(5)
            };
            var comment7 = new Comment
            {
                Content = "Of all the things you wear, your expression is the most important.",
                User = dominik,
                Published = DateTime.Now.AddHours(5)
            };
            var comment8 = new Comment
            {
                Content = "If all you are going to do in life are the things that are convenient and comfortable, the great things never get done.",
                User = franz,
                Published = DateTime.Now.AddHours(5)
            };
            var comment9 = new Comment
            {
                Content = "I am convinced that we have a degree of delight, and that no small one, in the real misfortunes and pains of others.",
                User = bauer,
                Published = DateTime.Now.AddHours(5)
            };
            var comment10 = new Comment
            {
                Content = "A committee is a thing which takes a week to do what one good man can do in an hour.",
                User = hubert,
                Published = DateTime.Now.AddHours(5)
            };


            var post1 = new Post
            {
                Content = "Pretension almost always overdoes the original, and hence exposes itself.",
                Comments = new List<Comment> { comment1, comment2, comment3 },
                Published = DateTime.Now,
                User = manuel
            };

            var post2 = new Post
            {
                Content = "Nobody is more dangerous than he who imagines himself pure in heart; for his purity, by definition, is unassailable.",
                Published = DateTime.Now,
                User = franz
            };

            var post3 = new Post
            {
                Content = "How have you left the ancient love That bards of old enjoyed in you! The languid strings do scarcely move! The sound is forced, the notes are few!",
                Published = DateTime.Now,
                User = bauer
            };

            var post4 = new Post
            {
                Content = "So deeply is the gardener's instinct implanted in my soul, I really love the tools with which I work; the iron fork, the spade, the hoe, the rake, the trowel, and the watering pot are pleasant objects in my eyes.",
                Comments = new List<Comment> { comment4, comment5 },
                Published = DateTime.Now,
                User = hubert
            };

            var post5 = new Post
            {
                Content = "With God, I can do all things! But with God and you, and the people who you can interest, by the grace of God, we're gonna cover the world!",
                Comments = new List<Comment> { comment6 },
                Published = DateTime.Now,
                User = manuel
            };

            var post6 = new Post
            {
                Content =
                    "Small detachments of soldiers knocked at each door, and then disappeared within the houses; for the vanquished saw they would have to be civil to their conquerors.",
                Published = DateTime.Now,
                User = dominik
            };

            var post7 = new Post
            {
                Content = "Even the town itself resumed by degrees its ordinary aspect.",
                Comments = new List<Comment> { comment7 },
                Published = DateTime.Now,
                User = hubert
            };

            var post8 = new Post
            {
                Content = "The French seldom walked abroad, but the streets swarmed with Prussian soldiers.",
                Published = DateTime.Now,
                User = manuel
            };

            var post9 = new Post
            {
                Content =
                    "But there was something in the air, a something strange and subtle, an intolerable foreign atmosphere like a penetrating odor--the odor of invasion.",
                Comments = new List<Comment> { comment8, comment9, comment10 },
                Published = DateTime.Now,
                User = bauer
            };

            var post10 = new Post
            {
                Content = "The conquerors exacted money, much money.",
                Published = DateTime.Now,
                User = manuel
            };

            var post11 = new Post
            {
                Content = "Laziness travels so slowly that poverty soon overtakes him.",
                Comments = new List<Comment> { comment1, comment2, comment3 },
                Published = DateTime.Now,
                User = manuel
            };

            var post12 = new Post
            {
                Content = "The world is a divine dream, from which we may presently awake to the glories and certainties of day.",
                Published = DateTime.Now,
                User = franz
            };

            var post13 = new Post
            {
                Content = "Faith is the great motive power, and no man realizes his full possibilities unless he has the deep conviction that life is eternally important, and that his work, well done, is part of an unending plan.",
                Published = DateTime.Now,
                User = bauer
            };

            var post14 = new Post
            {
                Content = "To confront a person with his own shadow is to show him his own light.",
                Comments = new List<Comment> { comment4, comment5 },
                Published = DateTime.Now,
                User = hubert
            };

            var post15 = new Post
            {
                Content = "The fact that I can plant a seed and it becomes a flower, share a bit of knowledge and it becomes another's, smile at someone and receive a smile in return, are to me continual spiritual exercises.",
                Comments = new List<Comment> { comment6 },
                Published = DateTime.Now,
                User = manuel
            };

            var post16 = new Post
            {
                Content = "The art of making deep noises from the chest sound like important messages from the brain.",
                Published = DateTime.Now,
                User = dominik
            };

            var post17 = new Post
            {
                Content = "In all the affairs of life, social as well as political, courtesies of a small and trivial character are the ones which strike deepest in the grateful and appreciating heart.",
                Comments = new List<Comment> { comment7 },
                Published = DateTime.Now,
                User = hubert
            };

            var post18 = new Post
            {
                Content = "Propaganda is that branch of the art of lying which consists in nearly deceiving your friends without quite deceiving your enemies.",
                Published = DateTime.Now,
                User = manuel
            };

            var post19 = new Post
            {
                Content = "And in the end, it's not the years in your life that count. It's the life in your years.",
                Comments = new List<Comment> { comment8, comment9, comment10 },
                Published = DateTime.Now,
                User = bauer
            };

            var post20 = new Post
            {
                Content = "Man needs difficulties. They are necessary for health.",
                Published = DateTime.Now,
                User = manuel
            };

            var post21 = new Post
            {
                Content = "It is well to remember that the entire universe, with one trifling exception, is composed of others.",
                Comments = new List<Comment> { comment6 },
                Published = DateTime.Now,
                User = manuel
            };

            var post22 = new Post
            {
                Content = "Strange when you come to think of it, that of all the countless folk who have lived before our time on this planet not one is known in history for in legend as having died of laughter.",
                Published = DateTime.Now,
                User = dominik
            };

            var post23 = new Post
            {
                Content = "Be cheerful in all that you do. Live joyfully. Live happily. Live enthusiastically, knowing that God does not dwell in gloom and melancholy, but in light and love.",
                Comments = new List<Comment> { comment7 },
                Published = DateTime.Now,
                User = hubert
            };

            var post24 = new Post
            {
                Content = "The Puritan hated bear-baiting, not because it gave pain to the bear, but because it gave pleasure to the spectators.",
                Published = DateTime.Now,
                User = manuel
            };

            var post25 = new Post
            {
                Content = "Until one has loved an animal, part of one's soul remains unawakened.",
                Comments = new List<Comment> { comment8, comment9, comment10 },
                Published = DateTime.Now,
                User = bauer
            };

            var post26 = new Post
            {
                Content = "Don't be afraid your life will end; be afraid that it will never begin.",
                Published = DateTime.Now,
                User = manuel
            };

            context.Posts.Add(post1);
            context.Posts.Add(post2);
            context.Posts.Add(post3);
            context.Posts.Add(post4);
            context.Posts.Add(post5);
            context.Posts.Add(post6);
            context.Posts.Add(post7);
            context.Posts.Add(post8);
            context.Posts.Add(post9);
            context.Posts.Add(post10);
            context.Posts.Add(post11);
            context.Posts.Add(post12);
            context.Posts.Add(post13);
            context.Posts.Add(post14);
            context.Posts.Add(post15);
            context.Posts.Add(post16);
            context.Posts.Add(post17);
            context.Posts.Add(post18);
            context.Posts.Add(post19);
            context.Posts.Add(post20);
            context.Posts.Add(post21);
            context.Posts.Add(post22);
            context.Posts.Add(post23);
            context.Posts.Add(post24);
            context.Posts.Add(post25);
            context.Posts.Add(post26);
            await context.SaveChangesAsync();
        }
    }
}
