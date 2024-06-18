using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiGaHRMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BillingPlatforms",
                columns: table => new
                {
                    BillingPlatformId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPlatforms", x => x.BillingPlatformId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactPersonName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesignationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DesignationName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesignationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    HolidayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.HolidayId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IncentivePurposes",
                columns: table => new
                {
                    IncentivePurposeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Purpose = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncentivePurposes", x => x.IncentivePurposeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LeaveMasters",
                columns: table => new
                {
                    LeaveMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LeaveType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeaveCount = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveMasters", x => x.LeaveMasterId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordPhrase = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordSalt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RateUSD = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RateINR = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WeeklyLimit = table.Column<int>(type: "int", nullable: false),
                    BillingType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    BillingPlatformId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_BillingPlatforms_BillingPlatformId",
                        column: x => x.BillingPlatformId,
                        principalTable: "BillingPlatforms",
                        principalColumn: "BillingPlatformId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TeamLeadId = table.Column<long>(type: "bigint", nullable: true),
                    ReportingManagerId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ReportingManagerId",
                        column: x => x.ReportingManagerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_TeamLeadId",
                        column: x => x.TeamLeadId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttendanceDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OutTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeDesignations",
                columns: table => new
                {
                    EmployeeDesignationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDesignations", x => x.EmployeeDesignationId);
                    table.ForeignKey(
                        name: "FK_EmployeeDesignations_Designations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designations",
                        principalColumn: "DesignationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDesignations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeSalaries",
                columns: table => new
                {
                    EmployeeSalaryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalaryForAMonth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    HRA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Conveyance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MedicalAllowance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SpecialAllowance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TDS = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LeaveDeduction = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    OtherDeduction = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DaysInAMonth = table.Column<int>(type: "int", nullable: false),
                    PresentDays = table.Column<int>(type: "int", nullable: false),
                    Leaves = table.Column<int>(type: "int", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaries", x => x.EmployeeSalaryId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryStructures",
                columns: table => new
                {
                    EmployeeSalaryStructureId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Basic = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    HRA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DA = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Conveyance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MedicalAllowance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SpecialAllowance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TDS = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryStructures", x => x.EmployeeSalaryStructureId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryStructures_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incentives",
                columns: table => new
                {
                    IncentiveId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IncentivePurposeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incentives", x => x.IncentiveId);
                    table.ForeignKey(
                        name: "FK_Incentives_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incentives_IncentivePurposes_IncentivePurposeId",
                        column: x => x.IncentivePurposeId,
                        principalTable: "IncentivePurposes",
                        principalColumn: "IncentivePurposeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LeaveBalances",
                columns: table => new
                {
                    LeaveBalanceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    EarnedLeaves = table.Column<short>(type: "smallint", nullable: false),
                    CasualLeaves = table.Column<short>(type: "smallint", nullable: false),
                    SickLeaves = table.Column<short>(type: "smallint", nullable: false),
                    MaternityLeaves = table.Column<short>(type: "smallint", nullable: false),
                    CompensatoryOffs = table.Column<short>(type: "smallint", nullable: false),
                    MarriageLeaves = table.Column<short>(type: "smallint", nullable: false),
                    PaternityLeaves = table.Column<short>(type: "smallint", nullable: false),
                    BereavementLeaves = table.Column<short>(type: "smallint", nullable: false),
                    LossofPayLeaves = table.Column<short>(type: "smallint", nullable: false),
                    EarnedLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    CasualLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    SickLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    MaternityLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    CompensatoryOffsAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    MarriageLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    PaternityLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    BereavementLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    LossofPayLeavesAvailaed = table.Column<short>(type: "smallint", nullable: false),
                    LeaveBalanceStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveBalances", x => x.LeaveBalanceId);
                    table.ForeignKey(
                        name: "FK_LeaveBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    LeaveRequestId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsHalfDay = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Reason = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeaveRequestStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApproverComment = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Approver = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.LeaveRequestId);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    TimesheetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TimesheetDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TimesheetStatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Approver = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.TimesheetId);
                    table.ForeignKey(
                        name: "FK_Timesheets_Employees_Approver",
                        column: x => x.Approver,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Timesheets_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TimeSheetDetails",
                columns: table => new
                {
                    TimeSheetDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Task = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoursSpent = table.Column<short>(type: "smallint", nullable: false),
                    IsBillable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TaskType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    TimesheetId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetDetails", x => x.TimeSheetDetailId);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetails_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_TimeSheetDetails_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_TimeSheetDetails_Timesheets_TimesheetId",
                        column: x => x.TimesheetId,
                        principalTable: "Timesheets",
                        principalColumn: "TimesheetId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDesignations_DesignationId",
                table: "EmployeeDesignations",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDesignations_EmployeeId",
                table: "EmployeeDesignations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportingManagerId",
                table: "Employees",
                column: "ReportingManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamLeadId",
                table: "Employees",
                column: "TeamLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryStructures_EmployeeId",
                table: "EmployeeSalaryStructures",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incentives_EmployeeId",
                table: "Incentives",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incentives_IncentivePurposeId",
                table: "Incentives",
                column: "IncentivePurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveBalances_EmployeeId",
                table: "LeaveBalances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BillingPlatformId",
                table: "Projects",
                column: "BillingPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetails_ClientId",
                table: "TimeSheetDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetails_ProjectId",
                table: "TimeSheetDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetails_TimesheetId",
                table: "TimeSheetDetails",
                column: "TimesheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_Approver",
                table: "Timesheets",
                column: "Approver");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_EmployeeId",
                table: "Timesheets",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeDesignations");

            migrationBuilder.DropTable(
                name: "EmployeeSalaries");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryStructures");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Incentives");

            migrationBuilder.DropTable(
                name: "LeaveBalances");

            migrationBuilder.DropTable(
                name: "LeaveMasters");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "TimeSheetDetails");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "IncentivePurposes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "BillingPlatforms");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
