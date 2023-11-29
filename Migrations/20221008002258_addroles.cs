using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Api_Project.Migrations
{
    public partial class addroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            //    values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() }

            //    );
            // migrationBuilder.InsertData(
            //     table: "AspNetRoles",
            //     columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            //     values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() }

            //     );
            string insertdatasql = $@"
            INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'186d7946-a166-4a35-acb7-839e178e53be', N'Admin', N'ADMIN', N'9eb56a37-0b90-4e4a-a9a5-79f8d9fd1ab9')
            
            INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f092c245-bcf7-4767-a9c0-c0283d9a8f87', N'User', N'USER', N'95115d3f-4675-437a-a869-a3a06b09984a')
          
            INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [BirthDate], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'98cbd5d3-8349-41b1-8138-dcff8f0843d5', N'mahmoud', N'eldeeb', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'mahmoudeldeeb', N'MAHMOUDELDEEB', N'ae244910@gmail.com', N'AE244910@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEHvxVuzM7sOql5te43FgRSlQUefwGYF+xM+x7AoDefVjbsv/cOwcBb1EhZFWCKViTQ==', N'6RQ55POLWPL2TSBNFNMRIACSZBOA3I7P', N'48680d9f-5024-4cb8-9f60-5560e44670b1', NULL, 0, 0, NULL, 1, 0)
            
            INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'98cbd5d3-8349-41b1-8138-dcff8f0843d5', N'186d7946-a166-4a35-acb7-839e178e53be')
           
            INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'98cbd5d3-8349-41b1-8138-dcff8f0843d5', N'f092c245-bcf7-4767-a9c0-c0283d9a8f87')

            ";
            migrationBuilder.Sql(insertdatasql);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            Delete  FROM [AspNetUserRoles]
            Delete  FROM [AspNetUsers]
            Delete  FROM [AspNetRoles]
            
            ");
        }
    }
}
