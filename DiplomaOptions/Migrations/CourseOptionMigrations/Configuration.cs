namespace DiplomaOptions.Migrations.CourseOptionMigrations
{
    using DiplomaDataModel.CourseOption.Seed;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.CourseOption.CourseOptionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\CourseOptionMigrations";
        }

        protected override void Seed(DiplomaDataModel.CourseOption.CourseOptionContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.YearTerms.AddOrUpdate(
                  y => y.YearTermId,
                  InitialOptionData.GetYearTerm().ToArray()
            );

            context.SaveChanges();

            context.Options.AddOrUpdate(
                  o => o.OptionId,
                  InitialOptionData.GetOption().ToArray()
            );

            context.SaveChanges();
        }
    }
}
