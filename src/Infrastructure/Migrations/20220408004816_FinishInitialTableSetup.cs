using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterCraft.Infrastructure.Migrations
{
    public partial class FinishInitialTableSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayPalAccountId",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "WelcomeVideoUrl",
                table: "Mentors");

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Institution = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    AccountType = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    RoutingNumber = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    Default = table.Column<bool>(type: "INTEGER", nullable: false),
                    MentorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Learners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationUserId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Learners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContentLink = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MentorId = table.Column<int>(type: "INTEGER", nullable: false),
                    LearnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfferingId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackRequests_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackRequests_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackRequests_Offerings_OfferingId",
                        column: x => x.OfferingId,
                        principalTable: "Offerings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardType = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    CardNetwork = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingFirstName = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingLastName = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingCompany = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingStreet = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    BillingPremise = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingCity = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingState = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingPostalCode = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    BillingCountry = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Default = table.Column<bool>(type: "INTEGER", nullable: false),
                    LearnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCards_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
                    VideoType = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    MentorId = table.Column<int>(type: "INTEGER", nullable: false),
                    LearnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    FeedbackRequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_FeedbackRequests_FeedbackRequestId",
                        column: x => x.FeedbackRequestId,
                        principalTable: "FeedbackRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videos_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videos_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    AuthorizationCode = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    TransactionId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    AuthorizationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CaptureDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FeedbackRequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_FeedbackRequests_FeedbackRequestId",
                        column: x => x.FeedbackRequestId,
                        principalTable: "FeedbackRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentCards_PaymentCardId",
                        column: x => x.PaymentCardId,
                        principalTable: "PaymentCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offerings_MentorId",
                table: "Offerings",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_MentorId",
                table: "BankAccounts",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackRequests_LearnerId",
                table: "FeedbackRequests",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackRequests_MentorId",
                table: "FeedbackRequests",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackRequests_OfferingId",
                table: "FeedbackRequests",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCards_LearnerId",
                table: "PaymentCards",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BankAccountId",
                table: "Payments",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FeedbackRequestId",
                table: "Payments",
                column: "FeedbackRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentCardId",
                table: "Payments",
                column: "PaymentCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_FeedbackRequestId",
                table: "Videos",
                column: "FeedbackRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LearnerId",
                table: "Videos",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MentorId",
                table: "Videos",
                column: "MentorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offerings_Mentors_MentorId",
                table: "Offerings",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offerings_Mentors_MentorId",
                table: "Offerings");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "PaymentCards");

            migrationBuilder.DropTable(
                name: "FeedbackRequests");

            migrationBuilder.DropTable(
                name: "Learners");

            migrationBuilder.DropIndex(
                name: "IX_Offerings_MentorId",
                table: "Offerings");

            migrationBuilder.AddColumn<string>(
                name: "PayPalAccountId",
                table: "Mentors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WelcomeVideoUrl",
                table: "Mentors",
                type: "TEXT",
                nullable: true);
        }
    }
}
