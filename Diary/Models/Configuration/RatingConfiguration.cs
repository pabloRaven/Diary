using Diary.Models.Domains;
using System.Data.Entity.ModelConfiguration;

namespace Diary.Models.Configuration
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>

    {
        public RatingConfiguration()
        {
            ToTable("dbo.Ratings");

            HasKey(x => x.Id);
        }

    }
}

