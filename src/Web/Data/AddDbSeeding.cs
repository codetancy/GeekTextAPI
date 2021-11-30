using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Web.Constants;
using Web.Data.Identities;
using Web.Models;

namespace Web.Data
{
    public static class AddDbSeeding
    {
        public static async Task SeedAsync(this AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            if (!dbContext.Genres.Any())
                SeedGenres();

            if (!dbContext.Authors.Any())
                SeedAuthors();

            if (!dbContext.Roles.Any())
                SeedRoles();

            if (!dbContext.Users.Any())
                await SeedUsersAsync();

            void SeedGenres()
            {
                var genres = new List<Genre>()
                {
                    new() {Name = "Academic"},
                    new() {Name = "Action"},
                    new() {Name = "Adventure"},
                    new() {Name = "Biography"},
                    new() {Name = "Comedy"},
                    new() {Name = "Cookbook"},
                    new() {Name = "Essay"},
                    new() {Name = "Fantasy"},
                    new() {Name = "Fiction"},
                    new() {Name = "Graphic Novel"},
                    new() {Name = "High Fantasy"},
                    new() {Name = "History"},
                    new() {Name = "Horror"},
                    new() {Name = "Mystery"},
                    new() {Name = "Military"},
                    new() {Name = "Non-Fiction"},
                    new() {Name = "Philosophy"},
                    new() {Name = "Poetry"},
                    new() {Name = "Religion"},
                    new() {Name = "Romance"},
                    new() {Name = "Sci-Fi"},
                    new() {Name = "Self-Help"},
                    new() {Name = "Thriller"}
                };

                dbContext.AddRange(genres);
                dbContext.SaveChanges();
            }

            void SeedAuthors()
            {
                var authors = new List<Author>()
                {
                    new Author()
                    {
                        // Id = Guid.Parse("8b9e69d2-5586-48ab-877b-a6cbf9ad4b80"),
                        Forename = "Joanne",
                        Surname = "Rowling",
                        PenName = "J.K. Rowling",
                        Biography = "British author, philanthropist, film producer, television producer, and screenwriter",
                        Books = new List<Book>()
                        {
                            new Book()
                            {
                                Id = Guid.Parse("bd7458e4-ac30-451a-8378-7a020795bae6"),
                                Title = "Harry Potter and the Philosopher's Stone",
                                Isbn = "0-7475-3269-9",
                                PublicationDate = DateTime.Parse("1997-06-26"),
                                GenreName = "Fantasy",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "The first novel in the Harry Potter series and Rowling's debut novel, it follows Harry Potter, " +
                                    "a young wizard who discovers his magical heritage on his eleventh birthday, when he receives a " +
                                    "letter of acceptance to Hogwarts School of Witchcraft and Wizardry. Harry makes close friends " +
                                    "and a few enemies during his first year at the school, and with the help of his friends, he faces " +
                                    "an attempted comeback by the dark wizard Lord Voldemort, who killed Harry's parents, but failed " +
                                    "to kill Harry when he was just 15 months old.",
                                CopiesSold = 120_000_000,
                                Rating = 5.0m,
                                UnitPrice = 20.0m
                            },
                            new Book()
                            {
                                Id = Guid.Parse("25f824ed-2e7b-4597-83bc-f215a1daa2b3"),
                                Title = "Harry Potter and the Chamber of Secrets",
                                Isbn = "0-7475-3849-2",
                                PublicationDate = DateTime.Parse("1998-07-02"),
                                GenreName = "Fantasy",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "The plot follows Harry's second year at Hogwarts School of Witchcraft and Wizardry, during which " +
                                    "a series of messages on the walls of the school's corridors warn that the \"Chamber of Secrets\"" +
                                    "has been opened and that the \"heir of Slytherin\" would kill all pupils who do not come from " +
                                    "all-magical families. These threats are found after attacks that leave residents of the school " +
                                    "petrified. Throughout the year, Harry and his friends Ron and Hermione investigate the attacks.",
                                CopiesSold = 77_000_000,
                                Rating = 5.0m,
                                UnitPrice = 10.99m
                            },
                            new Book()
                            {
                                Id = Guid.Parse("b7ce4bf5-7c65-4e99-96f7-a871c9616179"),
                                Title = "Harry Potter and the Prisoner of Azkaban",
                                Isbn = "0-7475-4215-5",
                                PublicationDate = DateTime.Parse("1999-07-08"),
                                GenreName = "Fantasy",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "The book follows Harry Potter, a young wizard, in his third year at Hogwarts School of " +
                                    "Witchcraft and Wizardry. Along with friends Ronald Weasley and Hermione Granger, Harry " +
                                    "investigates Sirius Black, an escaped prisoner from Azkaban, the wizard prison, believed " +
                                    "to be one of Lord Voldemort's old allies.",
                                CopiesSold = 65_000_000,
                                Rating = 4.7m,
                                UnitPrice = 10.99m
                            },
                            new Book()
                            {
                                Id = Guid.Parse("69edf6bf-c716-482e-bd03-5b1e0f1965fc"),
                                Title = "Harry Potter and the Goblet of Fire",
                                Isbn = "0-7475-4624-X",
                                PublicationDate = DateTime.Parse("2000-07-08"),
                                GenreName = "Fantasy",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "It follows Harry Potter, a wizard in his fourth year at Hogwarts School of Witchcraft " +
                                    "and Wizardry, and the mystery surrounding the entry of Harry's name into the Triwizard " +
                                    "Tournament, in which he is forced to compete.",
                                CopiesSold = 65_000_000,
                                Rating = 4.9m,
                                UnitPrice = 12.99m
                            },
                            new Book()
                            {
                                Id = Guid.Parse("6a88b0bc-be0c-4ec3-ba8e-0a14c83d935b"),
                                Title = "Harry Potter and the Order of the Phoenix",
                                Isbn = "0-7475-5100-6",
                                PublicationDate = DateTime.Parse("2003-06-23"),
                                GenreName = "Fantasy",
                                Publisher = "Bloomsbury",
                                Synopsis = "It follows Harry Potter's struggles through his fifth year at Hogwarts School of " +
                                           "Witchcraft and Wizardry, including the surreptitious return of the antagonist Lord " +
                                           "Voldemort, O.W.L. exams, and an obstructive Ministry of Magic.",
                                CopiesSold = 65_000_000,
                                Rating = 4.9m,
                                UnitPrice = 12.99m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("92dc3493-c0e4-4551-9c28-e2c193534482"),
                                Title = "Harry Potter and the Half-Blood Prince",
                                Isbn = "0-7475-8108-8",
                                PublicationDate = DateTime.Parse("2005-07-16"),
                                GenreName = "Fantasy",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "Set during Harry Potter's sixth year at Hogwarts, the novel explores the past of the " +
                                    "boy wizard's nemesis, Lord Voldemort, and Harry's preparations for the final battle " +
                                    "against Voldemort alongside his headmaster and mentor Albus Dumbledore.",
                                CopiesSold = 65_000_000,
                                Rating = 4.9m,
                                UnitPrice = 10.38m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("6cabdcd7-c05b-4fa7-b04e-6642dbae9b25"),
                                Title = "Harry Potter and the Deathly Hallows",
                                Isbn = "0-545-01022-5",
                                PublicationDate = DateTime.Parse("2007-07-14"),
                                GenreName = "Fantasy",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "Without the guidance and protection of their professors, Harry, Ron, and Hermione, " +
                                    "begin a mission to destroy the Horcruxes, the sources of Voldemort's immortality. " +
                                    "Voldemort's Death Eaters have seized control of the Ministry of Magic and Hogwarts, " +
                                    "and they are searching for Harry -- even as he and his friends prepare for the ultimate showdown.",
                                CopiesSold = 75_000_000,
                                Rating = 5.0m,
                                UnitPrice = 14.99m,
                            }
                        }
                    },
                    new Author()
                    {
                        // Id = Guid.Parse("ca20a67c-d47d-463c-8955-22ca0021269b"),
                        Forename = "Neil",
                        Surname = "Gaiman",
                        PenName = "Neil Gaiman",
                        Biography = "English author of short fiction, novels, comic books, graphic novels, nonfiction, " +
                                    "audio theatre, and films.",
                        Books = new List<Book>()
                        {
                            new Book()
                            {
                                Id = Guid.Parse("0ca523f2-0f14-4030-99d1-5b0c5cc9eb83"),
                                Title = "Coraline",
                                Isbn = "0-06-113937-8",
                                PublicationDate = DateTime.Parse("2002-02-07"),
                                GenreName = "Horror",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "Tells the story of a Coraline Jones, finding an idealized parallel world behind " +
                                    "a secret door in her new home, unaware that the alternative world contains a dark " +
                                    "and sinister secret.",
                                CopiesSold = 7_000_000,
                                Rating = 5.0m,
                                UnitPrice = 8.99m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("7434db74-bff6-4206-b881-2d432d6f31b8"),
                                Title = "Good Omens: The Nice and Accurate Prophecies of Agnes Nutter, Witch",
                                Isbn = "0-575-04800-X",
                                PublicationDate = DateTime.Parse("1990-05-10"),
                                GenreName = "Horror",
                                Publisher = "Bloomsbury",
                                Synopsis =
                                    "The book is a comedy about the birth of the son of Satan and the coming of the End " +
                                    "Times. There are attempts by the angel Aziraphale and the demon Crowley to sabotage " +
                                    "the coming of the end times, having grown accustomed to their comfortable surroundings " +
                                    "in England.",
                                CopiesSold = 5_000_000,
                                Rating = 4.5m,
                                UnitPrice = 8.99m,
                            },
                        }
                    },
                    new Author()
                    {
                        // Id = Guid.Parse("e89b3590-5d08-42f2-ad53-da5faa38d675"),
                        Forename = "John Ronald",
                        Surname = "Reuel Tolkien",
                        PenName = "J.R.R. Tolkien",
                        Biography = "English writer, poet, philologist, and academic, best known as the author of the " +
                                    "high fantasy works The Hobbit and The Lord of the Rings.",
                        Books = new List<Book>()
                        {
                            new Book()
                            {
                                Id = Guid.Parse("664c9b4a-a124-4e09-aaf9-2352cbef213f"),
                                Title = "The Hobbit, or There and Back Again",
                                PublicationDate = DateTime.Parse("1937-01-01"),
                                GenreName = "Fantasy",
                                Publisher = "George Allen & Unwin",
                                Synopsis =
                                    "The Hobbit is set within Tolkien's fictional universe and follows the quest of " +
                                    "home-loving Bilbo Baggins, the titular hobbit, to win a share of the treasure " +
                                    "guarded by a dragon named Smaug. Bilbo's journey takes him from his light-hearted, " +
                                    "rural surroundings into more sinister territory",
                                CopiesSold = 100_000_000,
                                Rating = 4.3m,
                                UnitPrice = 12.92m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("b1676473-03ef-419b-b1ec-788665f9d8e1"),
                                Title = "The Fellowship of the Ring",
                                PublicationDate = DateTime.Parse("1954-07-29"),
                                GenreName = "Fantasy",
                                Publisher = "George Allen & Unwin",
                                Synopsis =
                                    "Set in Middle-earth, the story tells of the Dark Lord Sauron, who seeks the One " +
                                    "Ring, which contains part of his soul, in order to return to power. The Ring has " +
                                    "found its way to the young hobbit Frodo Baggins. The fate of Middle-earth hangs " +
                                    "in the balance as Frodo and eight companions (who form the Fellowship of the Ring) " +
                                    "begin their journey to Mount Doom in the land of Mordor, the only place where " +
                                    "the Ring can be destroyed.",
                                CopiesSold = 150_000_000,
                                Rating = 4.4m,
                                UnitPrice = 10.09m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("82938af7-5a60-4b52-9009-0986ff5a214d"),
                                Title = "The Two Towers",
                                PublicationDate = DateTime.Parse("1954-11-11"),
                                GenreName = "Fantasy",
                                Publisher = "George Allen & Unwin",
                                Synopsis =
                                    "Continuing the plot of The Fellowship of the Ring, the film intercuts three " +
                                    "storylines. Frodo and Sam continue their journey towards Mordor to destroy the " +
                                    "One Ring, meeting and joined by Gollum, the ring's former keeper. Aragorn, " +
                                    "Legolas, and Gimli come to the war-torn nation of Rohan and are reunited with " +
                                    "the resurrected Gandalf, before fighting against the legions of the treacherous " +
                                    "wizard Saruman at the Battle of Helm's Deep. Merry and Pippin escape capture, " +
                                    "meet Treebeard the Ent, and help to plan an attack on Isengard, fortress of Saruman.",
                                CopiesSold = 150_000_000,
                                Rating = 4.8m,
                                UnitPrice = 11.17m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("c19323a0-36d3-4a7b-8261-daa743e364b7"),
                                Title = "The Return of the King",
                                PublicationDate = DateTime.Parse("1955-10-20"),
                                GenreName = "Fantasy",
                                Publisher = "George Allen & Unwin",
                                Synopsis =
                                    "Continuing the plot of The Two Towers, Frodo, Sam and Gollum are making their " +
                                    "final way toward Mount Doom in Mordor in order to destroy the One Ring, unaware " +
                                    "of Gollum's true intentions, while Gandalf, Aragorn, Legolas, Gimli and the rest " +
                                    "are joining forces together against Sauron and his legions in Minas Tirith.",
                                CopiesSold = 150_000_000,
                                Rating = 4.5m,
                                UnitPrice = 13.35m,
                            }
                        }
                    },
                    new()
                    {
                        // Id = Guid.Parse("c236c24e-551a-4835-8dce-aef4c0780ee6"),
                        Forename = "Sun",
                        Surname = "Tzu",
                        PenName = "Sun Tzu",
                        Biography = "Chinese general, military strategist, writer, and philosopher who lived in the " +
                                    "Eastern Zhou period of ancient China.",
                        Books = new List<Book>()
                        {
                            new Book()
                            {
                                Id = Guid.Parse("4d607c14-7d75-4b20-b825-5c40c47c304d"),
                                Title = "The Art of War",
                                PublicationDate = DateTime.Parse("500-01-01"),
                                GenreName = "Military",
                                Synopsis =
                                    "The book contains a detailed explanation and analysis of the 5th-century Chinese " +
                                    "military, from weapons and strategy to rank and discipline. Sun also stressed the " +
                                    "importance of intelligence operatives and espionage to the war effort.",
                                CopiesSold = 200_000,
                                Rating = 4.0m,
                                UnitPrice = 7.68m,
                            }
                        }
                    },
                    new()
                    {
                        // Id = Guid.Parse("190cea20-416f-4abc-9984-127ee9e191f2"),
                        Forename = "Paulo",
                        Surname = "Coelho de Souza",
                        PenName = "Paulo Coelho",
                        Biography = "Brazilian lyricist and novelist",
                        Books = new List<Book>()
                        {
                            new Book()
                            {
                                Id = Guid.Parse("2c7cf538-fc2c-4a85-be6e-3a4ec0fc8bfe"),
                                Title = "The Alchemist",
                                Isbn = "0-06-250217-4",
                                PublicationDate = DateTime.Parse("1988-01-01"),
                                GenreName = "Adventure",
                                Synopsis =
                                    "The Alchemist follows a young Andalusian shepherd in his journey to the pyramids " +
                                    "of Egypt, after having a recurring dream of finding a treasure there.",
                                CopiesSold = 65_000_000,
                                Rating = 3.9m,
                                UnitPrice = 7.68m,
                            }
                        }
                    },
                    new()
                    {
                        // Id = Guid.Parse("8105413d-d8a5-4a36-8cf0-035026c9d264"),
                        Forename = "Franklin Patrick",
                        Surname = "Herbert Jr.",
                        PenName = "Frank Herbert",
                        Biography = "American science fiction author best known for the 1965 novel Dune and its five " +
                                    "sequels. Though he became famous for his novels, he also wrote short stories and " +
                                    "worked as a newspaper journalist, photographer, book reviewer, ecological " +
                                    "consultant, and lecturer.",
                        Books = new List<Book>()
                        {
                            new Book()
                            {
                                Id = Guid.Parse("697a8952-44fe-4865-90ab-2ec9b3f910bc"),
                                Title = "Dune",
                                PublicationDate = DateTime.Parse("1965-08-01"),
                                GenreName = "Sci-Fi",
                                Synopsis =
                                    "Dune is set in the distant future amidst a feudal interstellar society in which " +
                                    "various noble houses control planetary fiefs. It tells the story of young Paul " +
                                    "Atreides, whose family accepts the stewardship of the planet Arrakis. While the " +
                                    "planet is an inhospitable and sparsely populated desert wasteland, it is the only " +
                                    "source of melange, or 'spice', a drug that extends life and enhances mental abilities. " +
                                    "As melange can only be produced on Arrakis, control of the planet is a coveted and " +
                                    "dangerous undertaking.",
                                CopiesSold = 20_000_000,
                                Rating = 4.2m,
                                UnitPrice = 9.89m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("3466241d-8d06-4a29-be3c-da52c9ede3f1"),
                                Title = "Dune Messiah",
                                PublicationDate = DateTime.Parse("1969-01-01"),
                                GenreName = "Sci-Fi",
                                Synopsis =
                                    "Dune Messiah continues the story of Paul Atreides, better known—and feared—as the " +
                                    "man christened Muad’Dib. As Emperor of the known universe, he possesses more power " +
                                    "than a single man was ever meant to wield. Worshipped as a religious icon by the " +
                                    "fanatical Fremen, Paul faces the enmity of the political houses he displaced when " +
                                    "he assumed the throne—and a conspiracy conducted within his own sphere of influence.",
                                CopiesSold = 20_000_000,
                                Rating = 4.5m,
                                UnitPrice = 9.99m,
                            },
                            new Book()
                            {
                                Id = Guid.Parse("e9492a2e-d196-4344-a2d7-7d7f39551b98"),
                                Title = "Children of Dune",
                                Isbn = "0-399-11697-4",
                                PublicationDate = DateTime.Parse("1976-04-01"),
                                GenreName = "Sci-Fi",
                                Synopsis =
                                    "At the end of Dune Messiah, Paul Atreides walks into the desert, a blind man,  " +
                                    "leaving his twin children Leto and Ghanima in the care of the Fremen, while his " +
                                    "sister Alia rules the universe as regent. Awakened in the womb by the spice, the " +
                                    "children are the heirs to Paul's prescient vision of the fate of the universe, a " +
                                    "role that Alia desperately craves. House Corrino schemes to return to the throne, " +
                                    "while the Bene Gesserit make common cause with the Tleilaxu and Spacing Guild to " +
                                    "gain control of the spice and the children of Paul Atreides.",
                                CopiesSold = 13_000_000,
                                Rating = 4.0m,
                                UnitPrice = 9.99m,
                            },
                        }
                    },
                };

                dbContext.Authors.AddRange(authors);
                dbContext.SaveChanges();
            }

            void SeedRoles()
            {
                var roles = new List<ApplicationRole>()
                {
                    new() {Name = RoleNames.User, NormalizedName = RoleNames.User.ToUpperInvariant()},
                    new() {Name = RoleNames.Admin, NormalizedName = RoleNames.Admin.ToUpperInvariant()}
                };
                dbContext.Roles.AddRange(roles);
                dbContext.SaveChanges();
            }

            async Task SeedUsersAsync()
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@geektext.com",
                    PhoneNumber = "123-456-7890",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var adminResult = await userManager.CreateAsync(adminUser, "Your-secret-pa55word");
                if (!adminResult.Succeeded) throw new Exception($"{adminResult.Errors.First().Description}");
                await userManager.AddToRolesAsync(adminUser, new[] {RoleNames.User, RoleNames.Admin});

                var regularUser = new ApplicationUser()
                {
                    UserName = "JohnDoe",
                    Email = "John@geektext.com",
                    PhoneNumber = "305-123-4567",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Payments = new List<Payment>()
                    {
                        new Card()
                        {
                            CardNumber = "1111222233334444",
                            CardHolderName = "John Doe",
                            ExpirationMonth = "11",
                            ExpirationYear = "2025",
                            SecurityCode = "123"
                        },
                        new Card()
                        {
                            CardNumber = "1234123412341234",
                            CardHolderName = "John Doe",
                            ExpirationMonth = "03",
                            ExpirationYear = "2022",
                            SecurityCode = "581"
                        },
                    },
                    Carts = new List<Cart>()
                    {
                        new Cart()
                        {
                            CartBooks = new List<CartBook>()
                            {
                                new CartBook() {BookId = Guid.Parse("25f824ed-2e7b-4597-83bc-f215a1daa2b3")}
                            }
                        }
                    },
                    WishLists = new List<WishList>()
                    {
                        new WishList()
                        {
                            Name = "John's Picks",
                            Description = "What I read in a desert island",
                            WishListBooks = new List<WishListBook>()
                            {
                                new WishListBook() {BookId = Guid.Parse("bd7458e4-ac30-451a-8378-7a020795bae6")},
                                new WishListBook() {BookId = Guid.Parse("69edf6bf-c716-482e-bd03-5b1e0f1965fc")}
                            }
                        },
                        new WishList()
                        {
                            Name = "Dune Trilogy",
                            Description = "For the Spice lovers",
                            WishListBooks = new List<WishListBook>()
                            {
                                new WishListBook() {BookId = Guid.Parse("697a8952-44fe-4865-90ab-2ec9b3f910bc")},
                                new WishListBook() {BookId = Guid.Parse("3466241d-8d06-4a29-be3c-da52c9ede3f1")},
                                new WishListBook() {BookId = Guid.Parse("e9492a2e-d196-4344-a2d7-7d7f39551b98")}
                            }
                        }
                    }
                };
                var regularResult = await userManager.CreateAsync(regularUser, "Your-secret-pa55word");
                if (!regularResult.Succeeded) throw new Exception($"{regularResult.Errors.First().Description}");
                await userManager.AddToRoleAsync(regularUser, RoleNames.User);
            }
        }
    }
}
