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
        }
    }
}
