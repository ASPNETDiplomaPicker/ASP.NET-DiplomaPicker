namespace OptionsWebSite.Migrations.OptionPickerMigrations
{
    using DiplomaDataModel.OptionPicker;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.OptionPicker.OptionPickerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\OptionPickerMigrations";
        }

        protected override void Seed(DiplomaDataModel.OptionPicker.OptionPickerContext context)
        {
            List<YearTerm> yearTerms = new List<YearTerm>()
            {
                new YearTerm { Year=2015,Term=20,isDefault=false},
                new YearTerm { Year=2015,Term=30,isDefault=false},
                new YearTerm { Year=2016,Term=10,isDefault=false},
                new YearTerm { Year=2016,Term=30,isDefault=true}
            };
            context.YearTerms.AddOrUpdate(s => s.YearTermId, yearTerms.ToArray());

            List<Option> options = new List<Option>()
            {
                new Option {Title="Data Communications",isActive=true },
                new Option {Title="Client Server",isActive=true },
                new Option {Title="Digital Processing",isActive=true },
                new Option {Title="Information Systems",isActive=true },
                new Option {Title="Database",isActive=false },
                new Option {Title="Web & Mobile",isActive=true },
                new Option {Title="Tech Pro",isActive=false },
            };
            context.Options.AddOrUpdate(o => o.OptionId, options.ToArray());

            List<Choice> choices = new List<Choice>()
            {
                new Choice {
                        ChoiceId = 1, YearTermId = 2,
                        StudentId = "A00000001",
                        StudentFirstName = "Kira", StudentLastName = "Yamato",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 3, FourthChoiceOptionId = 4,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 2, YearTermId = 2,
                        StudentId = "A00000002",
                        StudentFirstName = "Morgan", StudentLastName = "Freemasn",
                        FirstChoiceOptionId = 3, SecondChoiceOptionId = 4,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 1,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 3, YearTermId = 2,
                        StudentId = "A00000003",
                        StudentFirstName = "Pikachu", StudentLastName = "Chu",
                        FirstChoiceOptionId = 3, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 1, FourthChoiceOptionId = 4,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 4, YearTermId = 2,
                        StudentId = "A00000004",
                        StudentFirstName = "Nagisa", StudentLastName = "Ibuki",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 4, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 5, YearTermId = 2,
                        StudentId = "A00000005",
                        StudentFirstName = "Tomoyo", StudentLastName = "Ibuki",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 6, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 6, YearTermId = 2,
                        StudentId = "A00000006",
                        StudentFirstName = "Billy", StudentLastName = "Hamilton",
                        FirstChoiceOptionId = 3, SecondChoiceOptionId = 1,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 7, YearTermId = 2,
                        StudentId = "A00000007",
                        StudentFirstName = "Brack", StudentLastName = "Obama",
                        FirstChoiceOptionId = 6, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 1, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 8, YearTermId = 2,
                        StudentId = "A00000008",
                        StudentFirstName = "Nishino", StudentLastName = "Kana",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 6, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 9, YearTermId = 2,
                        StudentId = "A00000009",
                        StudentFirstName = "Lesli", StudentLastName = "Young",
                        FirstChoiceOptionId = 4, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 7, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 10, YearTermId = 2,
                        StudentId = "A00000010",
                        StudentFirstName = "Rick", StudentLastName = "Harden",
                        FirstChoiceOptionId = 6, SecondChoiceOptionId = 4,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 7,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 11, YearTermId = 3,
                        StudentId = "A00000011",
                        StudentFirstName = "Hank", StudentLastName = "Aaron",
                        FirstChoiceOptionId = 7, SecondChoiceOptionId = 6,
                        ThirdChoiceOptionId = 4, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 12, YearTermId = 3,
                        StudentId = "A00000012",
                        StudentFirstName = "Van", StudentLastName = "Heilsin",
                        FirstChoiceOptionId = 2, SecondChoiceOptionId = 4,
                        ThirdChoiceOptionId = 7, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 13, YearTermId = 3,
                        StudentId = "A00000013",
                        StudentFirstName = "Robert", StudentLastName = "Pirlo",
                        FirstChoiceOptionId = 7, SecondChoiceOptionId = 1,
                        ThirdChoiceOptionId = 6, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 14, YearTermId = 3,
                        StudentId = "A00000014",
                        StudentFirstName = "Bill", StudentLastName = "Morrison",
                        FirstChoiceOptionId = 5, SecondChoiceOptionId = 5,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 15, YearTermId = 3,
                        StudentId = "A00000015",
                        StudentFirstName = "Peter", StudentLastName = "Chen",
                        FirstChoiceOptionId = 2, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 1, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 16, YearTermId = 3,
                        StudentId = "A00000016",
                        StudentFirstName = "Kingsly", StudentLastName = "Coleman",
                        FirstChoiceOptionId = 7, SecondChoiceOptionId = 5,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 17, YearTermId = 3,
                        StudentId = "A00000017",
                        StudentFirstName = "Margret", StudentLastName = "Marth",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 3, FourthChoiceOptionId = 7,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 18, YearTermId = 3,
                        StudentId = "A00000018",
                        StudentFirstName = "Luigi", StudentLastName = "Mario",
                        FirstChoiceOptionId = 3, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 1, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceId = 19, YearTermId = 3,
                        StudentId = "A00000019",
                        StudentFirstName = "Jacky", StudentLastName = "Chen",
                        FirstChoiceOptionId = 7, SecondChoiceOptionId = 7,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 1,
                        SelectionDate = DateTime.Now
                }
                ,
                new Choice {
                        ChoiceId = 20, YearTermId = 3,
                        StudentId = "A00000020",
                        StudentFirstName = "Roger", StudentLastName = "Clement",
                        FirstChoiceOptionId = 7, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 1, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                }
            };
            context.Choices.AddOrUpdate(c => c.ChoiceId, choices.ToArray());
        }
    }
}
