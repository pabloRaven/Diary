﻿using Diary.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace Diary.Models.Configuration
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>

    {
        public GroupConfiguration()
        {
           ToTable("dbo.Groups");

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                ;
            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}

