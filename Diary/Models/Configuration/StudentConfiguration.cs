using Diary.Models.Domains;
using NUnit.Framework;

using System.Data.Entity.ModelConfiguration;


namespace Diary.Models.Configuration
{
    class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("dbo.Students");

            HasKey(x => x.Id);
        }
    }
}
