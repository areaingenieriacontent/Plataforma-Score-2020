namespace SCORM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdvanceCourse",
                c => new
                    {
                        AdCo_Id = c.Int(nullable: false, identity: true),
                        AdCo_ScoreObtanied = c.Double(nullable: false),
                        Enro_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        AdUs_Id = c.Int(nullable: false),
                        Module_Modu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AdCo_Id)
                .ForeignKey("dbo.Module", t => t.Module_Modu_Id)
                .ForeignKey("dbo.AdvanceUser", t => t.AdUs_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Enrollment", t => t.Enro_Id, cascadeDelete: true)
                .Index(t => t.Enro_Id)
                .Index(t => t.User_Id)
                .Index(t => t.AdUs_Id)
                .Index(t => t.Module_Modu_Id);
            
            CreateTable(
                "dbo.AdvanceUser",
                c => new
                    {
                        AdUs_Id = c.Int(nullable: false, identity: true),
                        AdUs_ScoreObtained = c.Double(nullable: false),
                        AdUs_PresentDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        ToCo_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdUs_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.ToCo_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.Int(),
                        StateUser = c.Int(),
                        Country = c.Int(),
                        Document = c.String(),
                        CompanyId = c.Int(),
                        PositionId = c.Int(),
                        AreaId = c.Int(),
                        CityId = c.Int(),
                        LocationId = c.Int(),
                        Enable = c.Int(),
                        lastAccess = c.DateTime(),
                        firstAccess = c.DateTime(),
                        TermsandConditions = c.Int(),
                        Videos = c.Int(),
                        SesionUser = c.Int(),
                        TermsJuego = c.Int(),
                        Foto_perfil = c.String(),
                        ComunidadActiva = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Area_AreaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Area", t => t.Area_AreaId)
                .ForeignKey("dbo.Position", t => t.PositionId)
                .ForeignKey("dbo.Area", t => t.AreaId)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Location", t => t.LocationId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.CompanyId)
                .Index(t => t.PositionId)
                .Index(t => t.AreaId)
                .Index(t => t.CityId)
                .Index(t => t.LocationId)
                .Index(t => t.Area_AreaId);
            
            CreateTable(
                "dbo.AnswersForum",
                c => new
                    {
                        AnFo_Id = c.Int(nullable: false, identity: true),
                        AnFo_Name = c.String(),
                        AnFo_Description = c.String(),
                        AnFo_InitDate = c.DateTime(nullable: false),
                        AnFo_Content = c.String(),
                        AnFo_Resource = c.String(),
                        User_Id = c.String(maxLength: 128),
                        ReFo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnFo_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.ResourceForum", t => t.ReFo_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.ReFo_Id);
            
            CreateTable(
                "dbo.ResourceForum",
                c => new
                    {
                        ReFo_Id = c.Int(nullable: false, identity: true),
                        ReFo_Name = c.String(),
                        ReFo_Description = c.String(),
                        ReFo_InitDate = c.DateTime(nullable: false),
                        ReFo_Content = c.String(),
                        ReFo_Resource = c.String(),
                        Job_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReFo_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Job", t => t.Job_Id, cascadeDelete: true)
                .Index(t => t.Job_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.BookRatings",
                c => new
                    {
                        BoRa_Id = c.Int(nullable: false, identity: true),
                        BoRa_InitDate = c.DateTime(),
                        BoRa_StateScore = c.Int(nullable: false),
                        BoRa_Score = c.Double(nullable: false),
                        BoRa_Point = c.Int(nullable: false),
                        BoRa_Description = c.String(),
                        ReFo_Id = c.Int(),
                        ReJo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.BoRa_Id)
                .ForeignKey("dbo.ResourceForum", t => t.ReFo_Id)
                .ForeignKey("dbo.ResourceJobs", t => t.ReJo_Id)
                .Index(t => t.ReFo_Id)
                .Index(t => t.ReJo_Id);
            
            CreateTable(
                "dbo.ResourceJobs",
                c => new
                    {
                        ReJo_Id = c.Int(nullable: false, identity: true),
                        ReJo_Name = c.String(),
                        ReJo_Description = c.String(),
                        ReJo_InitDate = c.DateTime(nullable: false),
                        ReJo_Content = c.String(),
                        ReJo_Resource = c.String(),
                        ReJo_Ext = c.String(),
                        Job_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReJo_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Job", t => t.Job_Id, cascadeDelete: true)
                .Index(t => t.Job_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        Job_Id = c.Int(nullable: false, identity: true),
                        Job_Name = c.String(),
                        Job_Description = c.String(),
                        Job_Resource = c.String(),
                        Job_Ext = c.String(),
                        Job_TypeJob = c.Int(nullable: false),
                        Job_StateJob = c.Int(nullable: false),
                        Job_InitDate = c.DateTime(nullable: false),
                        Job_FinishDate = c.DateTime(nullable: false),
                        Job_Content = c.String(),
                        Job_Points = c.Int(nullable: false),
                        Job_Visible = c.Int(nullable: false),
                        ToCo_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Job_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_Id, cascadeDelete: true)
                .Index(t => t.ToCo_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TopicsCourse",
                c => new
                    {
                        ToCo_Id = c.Int(nullable: false, identity: true),
                        ToCo_Name = c.String(),
                        ToCo_Description = c.String(),
                        ToCo_Attempt = c.Int(nullable: false),
                        ToCo_ExpectedScore = c.Int(nullable: false),
                        ToCo_Content = c.String(),
                        ToCo_ContentVirtual = c.String(),
                        Toco_Image = c.String(),
                        ToCo_TotalQuestion = c.Int(nullable: false),
                        ToCo_RequiredEvaluation = c.Int(nullable: false),
                        ToCo_Type = c.Int(nullable: false),
                        ToCo_Test = c.Int(nullable: false),
                        ToCo_Visible = c.Int(nullable: false),
                        First_Topic = c.Int(nullable: false),
                        ConditionedTopic = c.Int(nullable: false),
                        Topic_Available = c.Boolean(nullable: false),
                        Modu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ToCo_Id)
                .ForeignKey("dbo.Module", t => t.Modu_Id)
                .Index(t => t.Modu_Id);
            
            CreateTable(
                "dbo.BankQuestion",
                c => new
                    {
                        BaQu_Id = c.Int(nullable: false, identity: true),
                        BaQu_Description = c.String(),
                        BaQu_Name = c.String(),
                        BaQu_Porcentaje = c.Int(nullable: false),
                        BaQu_Porcentaje2 = c.Int(nullable: false),
                        BaQu_InintDate = c.DateTime(nullable: false),
                        BaQu_FinishDate = c.DateTime(nullable: false),
                        BaQu_TotalQuestion = c.Int(nullable: false),
                        BaQu_QuestionUser = c.Int(nullable: false),
                        BaQu_SelectQuestion = c.Int(nullable: false),
                        BaQu_Attempts = c.Int(nullable: false),
                        ToCo_Id = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BaQu_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_Id, cascadeDelete: true)
                .Index(t => t.ToCo_Id)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Attempts",
                c => new
                    {
                        Atte_Id = c.Int(nullable: false, identity: true),
                        Atte_InintDate = c.DateTime(nullable: false),
                        Atte_FinishDate = c.DateTime(nullable: false),
                        BaQu_Id = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Atte_Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .Index(t => t.BaQu_Id)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyType = c.Int(nullable: false),
                        CompanySector = c.Int(nullable: false),
                        CompanyName = c.String(),
                        CompanyNit = c.String(maxLength: 12, unicode: false),
                        CompanyGame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId)
                .Index(t => t.CompanyNit, unique: true);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        AreaName = c.String(),
                        UserId = c.String(maxLength: 128),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.CategoryModule",
                c => new
                    {
                        CaMo_Id = c.Int(nullable: false, identity: true),
                        CaMo_Category = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaMo_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.CategoryPrize",
                c => new
                    {
                        Capr_Id = c.Int(nullable: false, identity: true),
                        Capr_category = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Capr_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Prize",
                c => new
                    {
                        Priz_Id = c.Int(nullable: false, identity: true),
                        Priz_Name = c.String(nullable: false),
                        Priz_Description = c.String(),
                        Priz_RequiredPoints = c.Int(nullable: false),
                        Priz_Quantity = c.Int(nullable: false),
                        Priz_Date = c.DateTime(nullable: false),
                        Priz_Stateprize = c.Int(nullable: false),
                        Capr_Id = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Prize_Icon = c.Int(nullable: false),
                        PointManagerCategory_PoMaCa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Priz_Id)
                .ForeignKey("dbo.CategoryPrize", t => t.Capr_Id, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.PointManagerCategory", t => t.PointManagerCategory_PoMaCa_Id)
                .Index(t => t.Capr_Id)
                .Index(t => t.CompanyId)
                .Index(t => t.PointManagerCategory_PoMaCa_Id);
            
            CreateTable(
                "dbo.Exchange",
                c => new
                    {
                        Exch_Id = c.Int(nullable: false, identity: true),
                        Exch_date = c.DateTime(nullable: false),
                        Exch_Finishdate = c.DateTime(),
                        Exch_PointUser = c.Int(nullable: false),
                        Priz_Id = c.Int(nullable: false),
                        ApplicationUser = c.String(),
                        StateExchange = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Point_Poin_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Exch_Id)
                .ForeignKey("dbo.Prize", t => t.Priz_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Point", t => t.Point_Poin_Id)
                .Index(t => t.Priz_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Point_Poin_Id);
            
            CreateTable(
                "dbo.Changeinterface",
                c => new
                    {
                        ChIn_Id = c.Int(nullable: false, identity: true),
                        ChIn_FontType = c.String(),
                        ChIn_ColorBanner = c.String(),
                        ChIn_StyleButton = c.String(),
                        ChIn_Background = c.String(),
                        ChIn_Logo = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChIn_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Edition",
                c => new
                    {
                        Edit_Id = c.Int(nullable: false, identity: true),
                        Edit_Name = c.String(),
                        Edit_InintDate = c.DateTime(nullable: false),
                        Edit_FinishDate = c.DateTime(nullable: false),
                        Edit_StateEdition = c.Int(nullable: false),
                        Edit_Description = c.String(),
                        Edit_ImageName = c.String(),
                        Edit_ImageType = c.String(),
                        Edit_ImageContent = c.Binary(),
                        Edit_Points = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Edit_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Section",
                c => new
                    {
                        sect_Id = c.Int(nullable: false, identity: true),
                        sect_name = c.String(),
                        Edit_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.sect_Id)
                .ForeignKey("dbo.Edition", t => t.Edit_Id, cascadeDelete: true)
                .Index(t => t.Edit_Id);
            
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Arti_Id = c.Int(nullable: false, identity: true),
                        Arti_Name = c.String(),
                        Arti_imagen = c.String(),
                        Arti_Description = c.String(),
                        Arti_Content = c.String(),
                        Arti_Author = c.String(),
                        Arti_StateArticle = c.Int(nullable: false),
                        ArticleWithComment = c.Int(nullable: false),
                        sect_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Arti_Id)
                .ForeignKey("dbo.Section", t => t.sect_Id, cascadeDelete: true)
                .Index(t => t.sect_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Comm_Id = c.Int(nullable: false, identity: true),
                        comm_Title = c.String(),
                        comm_Description = c.String(),
                        Comm_value = c.Int(nullable: false),
                        Comm_Date = c.DateTime(nullable: false),
                        Comm_StateComment = c.Int(nullable: false),
                        Arti_Id = c.Int(nullable: false),
                        comm_Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Comm_Id)
                .ForeignKey("dbo.Article", t => t.Arti_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.comm_Author_Id)
                .Index(t => t.Arti_Id)
                .Index(t => t.comm_Author_Id);
            
            CreateTable(
                "dbo.PointsComment",
                c => new
                    {
                        PoCo_Id = c.Int(nullable: false, identity: true),
                        PoCo_Date = c.DateTime(nullable: false),
                        Comm_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PoCo_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Comments", t => t.Comm_Id, cascadeDelete: true)
                .Index(t => t.Comm_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ResourceNw",
                c => new
                    {
                        ReNw_Id = c.Int(nullable: false, identity: true),
                        ReNw_Name = c.String(),
                        ReNw_ResourceType = c.String(),
                        ReNw_Content = c.Binary(),
                        Arti_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReNw_Id)
                .ForeignKey("dbo.Article", t => t.Arti_Id, cascadeDelete: true)
                .Index(t => t.Arti_Id);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        Enro_Id = c.Int(nullable: false, identity: true),
                        Enro_TypeUser = c.String(),
                        Enro_StateEnrollment = c.Int(nullable: false),
                        Enro_RoleEnrollment = c.Int(nullable: false),
                        Enro_InitDateModule = c.DateTime(nullable: false),
                        Enro_FinishDateModule = c.DateTime(nullable: false),
                        Modu_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Enro_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Module", t => t.Modu_Id, cascadeDelete: true)
                .Index(t => t.Modu_Id)
                .Index(t => t.User_Id)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Certification",
                c => new
                    {
                        Cert_Id = c.Int(nullable: false, identity: true),
                        Cert_Date = c.DateTime(nullable: false),
                        Enro_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Module_Modu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Cert_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Enrollment", t => t.Enro_Id, cascadeDelete: true)
                .ForeignKey("dbo.Module", t => t.Module_Modu_Id)
                .Index(t => t.Enro_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Module_Modu_Id);
            
            CreateTable(
                "dbo.Desertify",
                c => new
                    {
                        Dese_Id = c.Int(nullable: false, identity: true),
                        Dese_Date = c.DateTime(nullable: false),
                        BaQu_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Enro_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Dese_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .ForeignKey("dbo.Enrollment", t => t.Enro_Id, cascadeDelete: true)
                .Index(t => t.BaQu_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Enro_Id);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        Modu_Id = c.Int(nullable: false, identity: true),
                        Modu_Name = c.String(),
                        Modu_Description = c.String(),
                        Modu_Statemodule = c.Int(nullable: false),
                        Modu_InitDate = c.DateTime(nullable: false),
                        Modu_Validity = c.Int(nullable: false),
                        Modu_Period = c.Int(nullable: false),
                        Modu_ImageName = c.String(),
                        Modu_Image = c.String(),
                        Modu_Content = c.String(),
                        Modu_Points = c.Int(nullable: false),
                        Modu_TypeOfModule = c.Int(nullable: false),
                        Modu_Forum = c.Int(nullable: false),
                        Modu_BetterPractice = c.Int(nullable: false),
                        Modu_Improvement = c.Int(nullable: false),
                        Modu_Test = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        CompanyId = c.Int(nullable: false),
                        QSMActive = c.Int(nullable: false),
                        hasProtectedFailure = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Modu_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.User_Id)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.BetterPractice",
                c => new
                    {
                        BePr_Id = c.Int(nullable: false, identity: true),
                        BePr_Points = c.Int(nullable: false),
                        BePr_Comment = c.String(),
                        BePr_TiTle = c.String(),
                        BePr_Resource = c.String(),
                        BePr_Name = c.String(),
                        BePr_InitDate = c.DateTime(),
                        BePr_FinishDate = c.DateTime(),
                        BePr_StateBetterpractice = c.Int(nullable: false),
                        Modu_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BePr_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Module", t => t.Modu_Id, cascadeDelete: true)
                .Index(t => t.Modu_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ResourceBetterPractice",
                c => new
                    {
                        ReBe_Id = c.Int(nullable: false, identity: true),
                        ReBe_Name = c.String(),
                        ReBe_ResourcebettType = c.String(),
                        ReBe_Content = c.Binary(),
                        BePr_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReBe_Id)
                .ForeignKey("dbo.BetterPractice", t => t.BePr_Id, cascadeDelete: true)
                .Index(t => t.BePr_Id);
            
            CreateTable(
                "dbo.Improvement",
                c => new
                    {
                        Impr_Id = c.Int(nullable: false, identity: true),
                        Impr_Points = c.Int(nullable: false),
                        Impr_Comment = c.String(),
                        Impr_Comment2 = c.String(),
                        Impr_Title = c.String(),
                        Impr_Resource = c.String(),
                        Impr_Name = c.String(),
                        Impr_InitDate = c.DateTime(),
                        Impr_FinishDate = c.DateTime(),
                        Impr_StateImprovement = c.Int(nullable: false),
                        Modu_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Impr_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Module", t => t.Modu_Id, cascadeDelete: true)
                .Index(t => t.Modu_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ImageUpload",
                c => new
                    {
                        ImUp_Id = c.Int(nullable: false, identity: true),
                        ImUp_Name = c.String(),
                        ImUp_Url = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImUp_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Loca_Id = c.Int(nullable: false, identity: true),
                        Loca_Description = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Loca_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Log_Id = c.Int(nullable: false, identity: true),
                        Log_Description = c.String(),
                        Log_StateLogs = c.Int(nullable: false),
                        Log_Date = c.DateTime(nullable: false),
                        Log_Ip = c.String(),
                        User_Id = c.String(maxLength: 128),
                        CoLo_Id = c.Int(nullable: false),
                        TaCh_Id = c.Int(nullable: false),
                        IdCh_Id = c.Int(nullable: false),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Log_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.CodeLogs", t => t.CoLo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Company", t => t.Company_Id, cascadeDelete: true)
                .ForeignKey("dbo.IdChange", t => t.IdCh_Id, cascadeDelete: true)
                .ForeignKey("dbo.TableChange", t => t.TaCh_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.CoLo_Id)
                .Index(t => t.TaCh_Id)
                .Index(t => t.IdCh_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.CodeLogs",
                c => new
                    {
                        CoLo_Id = c.Int(nullable: false, identity: true),
                        CoLo_Code = c.Int(nullable: false),
                        CoLo_Description = c.String(),
                    })
                .PrimaryKey(t => t.CoLo_Id);
            
            CreateTable(
                "dbo.IdChange",
                c => new
                    {
                        IdCh_Id = c.Int(nullable: false, identity: true),
                        IdCh_IdChange = c.String(),
                    })
                .PrimaryKey(t => t.IdCh_Id);
            
            CreateTable(
                "dbo.TableChange",
                c => new
                    {
                        TaCh_Id = c.Int(nullable: false, identity: true),
                        TaCh_TableName = c.String(),
                    })
                .PrimaryKey(t => t.TaCh_Id);
            
            CreateTable(
                "dbo.Measure",
                c => new
                    {
                        MeasureId = c.Int(nullable: false, identity: true),
                        MeasureInitDate = c.DateTime(nullable: false),
                        MeasureFinishDate = c.DateTime(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeasureId)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Test", t => t.TestId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.MeasureUser",
                c => new
                    {
                        MausureUserID = c.Int(nullable: false, identity: true),
                        MausureId = c.Int(nullable: false),
                        UsersId = c.String(maxLength: 128),
                        UserEvaluate = c.String(),
                    })
                .PrimaryKey(t => t.MausureUserID)
                .ForeignKey("dbo.Users", t => t.UsersId)
                .ForeignKey("dbo.Measure", t => t.MausureId, cascadeDelete: true)
                .Index(t => t.MausureId)
                .Index(t => t.UsersId);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        TestDescription = c.String(),
                        EvaluateTo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestId);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionDescription = c.String(),
                        ProficiencyId = c.Int(nullable: false),
                        QuestionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Proficiency", t => t.ProficiencyId, cascadeDelete: true)
                .Index(t => t.ProficiencyId);
            
            CreateTable(
                "dbo.Proficiency",
                c => new
                    {
                        ProficiencyId = c.Int(nullable: false, identity: true),
                        ProficiencyDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProficiencyId);
            
            CreateTable(
                "dbo.MG_SettingMp",
                c => new
                    {
                        Sett_Id = c.Int(nullable: false, identity: true),
                        Sett_Attemps = c.Int(nullable: false),
                        Sett_InitialDate = c.DateTime(nullable: false),
                        Sett_CloseDate = c.DateTime(nullable: false),
                        Sett_NumberOfQuestions = c.Int(nullable: false),
                        Sett_Instruction = c.String(),
                        Sett_Audio1 = c.String(),
                        Sett_Audio2 = c.String(),
                        Sett_Audio3 = c.String(),
                        Sett_Audio4 = c.String(),
                        Sett_Audio5 = c.String(),
                        Plan_Id = c.Int(nullable: false),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sett_Id)
                .ForeignKey("dbo.Company", t => t.Company_Id, cascadeDelete: true)
                .ForeignKey("dbo.MG_Template", t => t.Plan_Id, cascadeDelete: true)
                .Index(t => t.Plan_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.MG_MultipleChoice",
                c => new
                    {
                        MuCh_ID = c.Int(nullable: false, identity: true),
                        MuCh_Description = c.String(),
                        MuCh_NameQuestion = c.String(),
                        MuCh_ImageQuestion = c.String(),
                        MuCh_Feedback = c.String(),
                        MuCh_Level = c.Int(nullable: false),
                        Sett_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MuCh_ID)
                .ForeignKey("dbo.MG_SettingMp", t => t.Sett_Id, cascadeDelete: true)
                .Index(t => t.Sett_Id);
            
            CreateTable(
                "dbo.MG_AnswerMultipleChoice",
                c => new
                    {
                        AnMul_ID = c.Int(nullable: false, identity: true),
                        AnMul_Description = c.String(),
                        AnMul_TrueAnswer = c.Int(nullable: false),
                        MuCh_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnMul_ID)
                .ForeignKey("dbo.MG_MultipleChoice", t => t.MuCh_ID, cascadeDelete: true)
                .Index(t => t.MuCh_ID);
            
            CreateTable(
                "dbo.MG_AnswerUser",
                c => new
                    {
                        AnUs_Id = c.Int(nullable: false, identity: true),
                        Attemps = c.Int(nullable: false),
                        Respuesta = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(),
                        FechaEnvio = c.DateTime(),
                        Comodin = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        AnMul_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnUs_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.MG_AnswerMultipleChoice", t => t.AnMul_ID, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.AnMul_ID);
            
            CreateTable(
                "dbo.MG_Order",
                c => new
                    {
                        Order_Id = c.Int(nullable: false, identity: true),
                        Order_NameQuestion = c.String(),
                        Order_Description = c.String(),
                        Order_Level = c.Int(nullable: false),
                        Sett_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_Id)
                .ForeignKey("dbo.MG_SettingMp", t => t.Sett_Id, cascadeDelete: true)
                .Index(t => t.Sett_Id);
            
            CreateTable(
                "dbo.MG_AnswerOrder",
                c => new
                    {
                        AnOr_ID = c.Int(nullable: false, identity: true),
                        AnOr_Description = c.String(),
                        Order_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnOr_ID)
                .ForeignKey("dbo.MG_Order", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.MG_Pairing",
                c => new
                    {
                        Pairi_Id = c.Int(nullable: false, identity: true),
                        Pairi_NameQuestion = c.String(),
                        Pairi_Description = c.String(),
                        Pairi_Feedback = c.String(),
                        Pairi_Level = c.Int(nullable: false),
                        Sett_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pairi_Id)
                .ForeignKey("dbo.MG_SettingMp", t => t.Sett_Id, cascadeDelete: true)
                .Index(t => t.Sett_Id);
            
            CreateTable(
                "dbo.MG_AnswerPairing",
                c => new
                    {
                        AnPa_Id = c.Int(nullable: false, identity: true),
                        Anpa_Answer = c.String(),
                        AnPa_Question = c.String(),
                        Anpa_Feedback = c.String(),
                        Pairi_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnPa_Id)
                .ForeignKey("dbo.MG_Pairing", t => t.Pairi_Id, cascadeDelete: true)
                .Index(t => t.Pairi_Id);
            
            CreateTable(
                "dbo.MG_Point",
                c => new
                    {
                        point_Id = c.Int(nullable: false, identity: true),
                        point_pointOfUser = c.Int(nullable: false),
                        Sett_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.point_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.MG_SettingMp", t => t.Sett_Id, cascadeDelete: true)
                .Index(t => t.Sett_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MG_Template",
                c => new
                    {
                        Plant_Id = c.Int(nullable: false, identity: true),
                        Plant_Color = c.String(),
                        Plant_Img_instructions = c.String(),
                        Plant_Img_Questions = c.String(),
                        Company_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Plant_Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.PointManagerCategory",
                c => new
                    {
                        PoMaCa_Id = c.Int(nullable: false, identity: true),
                        PoMaCa_Periodical = c.Int(nullable: false),
                        PoMaCa_course = c.Int(nullable: false),
                        PoMaCa_Improvements = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PoMaCa_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Posi_id = c.Int(nullable: false, identity: true),
                        Posi_Description = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Posi_id)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.ResourceTopic",
                c => new
                    {
                        ReTo_Id = c.Int(nullable: false, identity: true),
                        ReTo_Name = c.String(),
                        ReTo_Location = c.String(),
                        CompanyId = c.Int(nullable: false),
                        ToCo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReTo_Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_Id, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.ToCo_Id);
            
            CreateTable(
                "dbo.NewAttempts",
                c => new
                    {
                        NeAt_Id = c.Int(nullable: false, identity: true),
                        NeAt_DateInint = c.DateTime(nullable: false),
                        Attempts = c.Int(nullable: false),
                        BaQu_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NeAt_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .Index(t => t.BaQu_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.OpenQuestion",
                c => new
                    {
                        OpQu_Id = c.Int(nullable: false, identity: true),
                        OpQu_Question = c.String(),
                        OpQu_Answer = c.String(),
                        OpQu_Score = c.Int(nullable: false),
                        BaQu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OpQu_Id)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .Index(t => t.BaQu_Id);
            
            CreateTable(
                "dbo.OptionMultiple",
                c => new
                    {
                        OpMu_Id = c.Int(nullable: false, identity: true),
                        OpMu_Question = c.String(),
                        OpMu_Score = c.Int(nullable: false),
                        OpMu_Description = c.String(),
                        OpMult_Content = c.String(),
                        BaQu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OpMu_Id)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .Index(t => t.BaQu_Id);
            
            CreateTable(
                "dbo.AnswerOptionMultiple",
                c => new
                    {
                        AnOp_Id = c.Int(nullable: false, identity: true),
                        AnOp_OptionAnswer = c.String(),
                        AnOp_TrueAnswer = c.Int(nullable: false),
                        Answer_OpMult_Content = c.String(),
                        OpMu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnOp_Id)
                .ForeignKey("dbo.OptionMultiple", t => t.OpMu_Id, cascadeDelete: true)
                .Index(t => t.OpMu_Id);
            
            CreateTable(
                "dbo.AnswerOptionMultipleStudent",
                c => new
                    {
                        AnOp_Id = c.Int(nullable: false, identity: true),
                        AnOp_OptionAnswer = c.String(),
                        AnOp_TrueAnswer = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Answer_OpMult_Content = c.String(),
                        OpMu_Id = c.Int(nullable: false),
                        Date_Present_test = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AnOp_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.OptionMultiple", t => t.OpMu_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.OpMu_Id);
            
            CreateTable(
                "dbo.Pairing",
                c => new
                    {
                        Pair_Id = c.Int(nullable: false, identity: true),
                        Pair_Question = c.String(),
                        Pair_Score = c.Int(nullable: false),
                        Pair_Description = c.String(),
                        BaQu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pair_Id)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .Index(t => t.BaQu_Id);
            
            CreateTable(
                "dbo.AnserPairingStudent",
                c => new
                    {
                        AnPa_Id = c.Int(nullable: false, identity: true),
                        AnPa_OptionsQuestion = c.String(),
                        AnPa_OptionAnswer = c.String(),
                        User_Id = c.String(maxLength: 128),
                        Pair_Id = c.Int(nullable: false),
                        Date_Present_test = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AnPa_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Pairing", t => t.Pair_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Pair_Id);
            
            CreateTable(
                "dbo.AnswerPairing",
                c => new
                    {
                        AnPa_Id = c.Int(nullable: false, identity: true),
                        AnPa_OptionsQuestion = c.String(),
                        AnPa_OptionAnswer = c.String(),
                        Pair_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnPa_Id)
                .ForeignKey("dbo.Pairing", t => t.Pair_Id, cascadeDelete: true)
                .Index(t => t.Pair_Id);
            
            CreateTable(
                "dbo.TrueOrFalse",
                c => new
                    {
                        TrFa_Id = c.Int(nullable: false, identity: true),
                        TrFa_Question = c.String(),
                        TrFa_Description = c.String(),
                        TrFa_Content = c.String(),
                        TrFa_FalseAnswer = c.String(),
                        TrFa_TrueAnswer = c.String(),
                        TrFa_Score = c.String(),
                        TrFa_State = c.Int(nullable: false),
                        BaQu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrFa_Id)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .Index(t => t.BaQu_Id);
            
            CreateTable(
                "dbo.TrueOrFalseStudent",
                c => new
                    {
                        TrFa_Id = c.Int(nullable: false, identity: true),
                        TrFa_Question = c.String(),
                        TrFa_Description = c.String(),
                        TrFa_Content = c.String(),
                        TrFa_FalseAnswer = c.String(),
                        TrFa_TrueAnswer = c.String(),
                        TrFa_Score = c.String(),
                        TrFa_State = c.Int(nullable: false),
                        Date_Present_test = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        BaQu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrFa_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.BankQuestion", t => t.BaQu_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.BaQu_Id);
            
            CreateTable(
                "dbo.Link",
                c => new
                    {
                        Link_Id = c.Int(nullable: false, identity: true),
                        Link_Description = c.String(),
                        Link_RequiredEvaluation = c.Int(nullable: false),
                        ToCo_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Link_Id)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_id, cascadeDelete: true)
                .Index(t => t.ToCo_id);
            
            CreateTable(
                "dbo.ResourceTopics",
                c => new
                    {
                        ReMo_Id = c.Int(nullable: false, identity: true),
                        ReMo_NameResource = c.String(),
                        ReMo_Name = c.String(),
                        ReMo_InitDate = c.DateTime(),
                        ToCo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReMo_Id)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_Id, cascadeDelete: true)
                .Index(t => t.ToCo_Id);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        City_Id = c.Int(nullable: false, identity: true),
                        City_Name = c.String(),
                    })
                .PrimaryKey(t => t.City_Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.LockGame",
                c => new
                    {
                        LoGa_Id = c.Int(nullable: false, identity: true),
                        LoGa_InitDate = c.DateTime(nullable: false),
                        LoGa_FinishDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Game_Id = c.Int(nullable: false),
                        TyBa_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoGa_Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Game", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeBaneo", t => t.TyBa_Id, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Game_Id)
                .Index(t => t.TyBa_Id);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Game_Id = c.Int(nullable: false, identity: true),
                        Game_Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Game_Id);
            
            CreateTable(
                "dbo.TypeBaneo",
                c => new
                    {
                        TyBa_Id = c.Int(nullable: false, identity: true),
                        TyBa_Type = c.String(),
                    })
                .PrimaryKey(t => t.TyBa_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.MG_BlockGameUser",
                c => new
                    {
                        BlGa_Id = c.Int(nullable: false, identity: true),
                        BlGa_Fecha = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BlGa_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Point",
                c => new
                    {
                        Poin_Id = c.Int(nullable: false, identity: true),
                        Poin_Date = c.DateTime(nullable: false),
                        Poin_End_Date = c.DateTime(nullable: false),
                        Quantity_Points = c.Int(nullable: false),
                        TyPo_Id = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Poin_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.TypePoint", t => t.TyPo_Id, cascadeDelete: true)
                .Index(t => t.TyPo_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TypePoint",
                c => new
                    {
                        TyPo_Id = c.Int(nullable: false, identity: true),
                        TyPo_Description = c.String(),
                        Poin_TypePoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TyPo_Id);
            
            CreateTable(
                "dbo.Result",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        ResultDate = c.DateTime(nullable: false),
                        ResultTotalScore = c.Int(nullable: false),
                        MeasureId = c.Int(nullable: false),
                        QualifiedUser_Id = c.String(maxLength: 128),
                        ResultOwner_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Measure", t => t.MeasureId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.QualifiedUser_Id)
                .ForeignKey("dbo.Users", t => t.ResultOwner_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .Index(t => t.MeasureId)
                .Index(t => t.QualifiedUser_Id)
                .Index(t => t.ResultOwner_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Score",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        ProficiencyId = c.Int(nullable: false),
                        ResultId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ScoreId)
                .ForeignKey("dbo.Proficiency", t => t.ProficiencyId, cascadeDelete: true)
                .ForeignKey("dbo.Result", t => t.ResultId, cascadeDelete: true)
                .Index(t => t.ProficiencyId)
                .Index(t => t.ResultId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Nombreprueba = c.Int(nullable: false, identity: true),
                        Jajaja = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Nombreprueba)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AdvanceLoseUser",
                c => new
                    {
                        AdLoUs_Id = c.Int(nullable: false, identity: true),
                        AdLoUs_ScoreObtained = c.Double(nullable: false),
                        AdLoUs_PresentDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        ToCo_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdLoUs_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.TopicsCourse", t => t.ToCo_id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.ToCo_id);
            
            CreateTable(
                "dbo.Banner",
                c => new
                    {
                        Bann_Id = c.Int(nullable: false, identity: true),
                        Bann_Name = c.String(),
                        Bann_Date = c.DateTime(nullable: false),
                        Bann_Description = c.String(),
                        Bann_Link = c.String(),
                        Bann_Image = c.String(),
                        companyId = c.Int(),
                    })
                .PrimaryKey(t => t.Bann_Id);
            
            CreateTable(
                "dbo.BlockService",
                c => new
                    {
                        BlSe_Id = c.Int(nullable: false, identity: true),
                        BlSe_Date = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        TySe_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlSe_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.TypeServiceBlock", t => t.TySe_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.TySe_Id);
            
            CreateTable(
                "dbo.TypeServiceBlock",
                c => new
                    {
                        TySe_Id = c.Int(nullable: false, identity: true),
                        TySe_Description = c.String(),
                    })
                .PrimaryKey(t => t.TySe_Id);
            
            CreateTable(
                "dbo.CorreoModel",
                c => new
                    {
                        IdMensaje = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Categoria = c.String(),
                        Documento = c.String(),
                        Empresa = c.String(),
                        Mensaje = c.String(),
                        Correos = c.String(),
                    })
                .PrimaryKey(t => t.IdMensaje);
            
            CreateTable(
                "dbo.LogsComunidad",
                c => new
                    {
                        IdUsuario = c.String(nullable: false, maxLength: 128),
                        ContOBL = c.Int(nullable: false),
                        ContSoftKills = c.Int(nullable: false),
                        ContBiblioteca = c.Int(nullable: false),
                        ContPeriodico = c.Int(nullable: false),
                        ContJuegos = c.Int(nullable: false),
                        ContVideoteca = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario);
            
            CreateTable(
                "dbo.LogsUserInPlans",
                c => new
                    {
                        idLog = c.Int(nullable: false, identity: true),
                        userid = c.String(),
                        date = c.DateTime(nullable: false),
                        planid_PlanId = c.Int(),
                    })
                .PrimaryKey(t => t.idLog)
                .ForeignKey("dbo.Plan", t => t.planid_PlanId)
                .Index(t => t.planid_PlanId);
            
            CreateTable(
                "dbo.Plan",
                c => new
                    {
                        PlanId = c.Int(nullable: false, identity: true),
                        PlanDescription = c.String(),
                        PlanMinScore = c.Int(nullable: false),
                        PlanMaxScore = c.Int(nullable: false),
                        PlanTo = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                        ProficiencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlanId)
                .ForeignKey("dbo.Proficiency", t => t.ProficiencyId, cascadeDelete: true)
                .Index(t => t.ProficiencyId);
            
            CreateTable(
                "dbo.Resource",
                c => new
                    {
                        ResourceId = c.Int(nullable: false, identity: true),
                        ResourceType = c.String(),
                        ResourceName = c.String(),
                        Content = c.Binary(nullable: false),
                        PlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResourceId)
                .ForeignKey("dbo.Plan", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId);
            
            CreateTable(
                "dbo.PointsObtainedForUser",
                c => new
                    {
                        PointObtainedId = c.Int(nullable: false, identity: true),
                        PointsAssigned = c.Int(nullable: false),
                        IdUser = c.String(nullable: false),
                        Date = c.DateTime(),
                        IdLevelCode = c.Int(nullable: false),
                        IdGame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PointObtainedId);
            
            CreateTable(
                "dbo.QuienSabeMasPuntaje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(),
                        User_Id_QSM = c.String(),
                        Mudole_Id = c.Int(nullable: false),
                        Mudole_Id_QSM = c.Int(nullable: false),
                        FechaPresentacion = c.DateTime(nullable: false),
                        Puntaje = c.Single(nullable: false),
                        PorcentajeAprobacion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StylesLogos",
                c => new
                    {
                        Styl_Id = c.Int(nullable: false, identity: true),
                        companyId = c.Int(),
                        UrlLogo = c.String(),
                        navBarColor = c.String(),
                        UrlImage1 = c.String(),
                        UrlImage2 = c.String(),
                        UrlImage3 = c.String(),
                        UrlImage4 = c.String(),
                        Title1 = c.String(),
                        Title2 = c.String(),
                        Title3 = c.String(),
                        colorsTittle = c.String(),
                        colorsBacgraundTitles = c.String(),
                    })
                .PrimaryKey(t => t.Styl_Id);
            
            CreateTable(
                "dbo.QuestionTest",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.TestId })
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Test", t => t.TestId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.UserClients",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ApplicationUser_Id1 })
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
            CreateTable(
                "dbo.UserEquals",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ApplicationUser_Id1 })
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
            CreateTable(
                "dbo.MyOfficeUser",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ApplicationUser_Id1 })
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
            CreateTable(
                "dbo.UserSuperiors",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ApplicationUser_Id1 })
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.LogsUserInPlans", "planid_PlanId", "dbo.Plan");
            DropForeignKey("dbo.Resource", "PlanId", "dbo.Plan");
            DropForeignKey("dbo.Plan", "ProficiencyId", "dbo.Proficiency");
            DropForeignKey("dbo.BlockService", "TySe_Id", "dbo.TypeServiceBlock");
            DropForeignKey("dbo.BlockService", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AdvanceLoseUser", "ToCo_id", "dbo.TopicsCourse");
            DropForeignKey("dbo.AdvanceLoseUser", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AdvanceCourse", "Enro_Id", "dbo.Enrollment");
            DropForeignKey("dbo.AdvanceCourse", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AdvanceCourse", "AdUs_Id", "dbo.AdvanceUser");
            DropForeignKey("dbo.AdvanceUser", "ToCo_id", "dbo.TopicsCourse");
            DropForeignKey("dbo.AdvanceUser", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserInfo", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserSuperiors", "ApplicationUser_Id1", "dbo.Users");
            DropForeignKey("dbo.UserSuperiors", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.Result", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.Score", "ResultId", "dbo.Result");
            DropForeignKey("dbo.Score", "ProficiencyId", "dbo.Proficiency");
            DropForeignKey("dbo.Result", "ResultOwner_Id", "dbo.Users");
            DropForeignKey("dbo.Result", "QualifiedUser_Id", "dbo.Users");
            DropForeignKey("dbo.Result", "MeasureId", "dbo.Measure");
            DropForeignKey("dbo.Point", "TyPo_Id", "dbo.TypePoint");
            DropForeignKey("dbo.Exchange", "Point_Poin_Id", "dbo.Point");
            DropForeignKey("dbo.Point", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MyOfficeUser", "ApplicationUser_Id1", "dbo.Users");
            DropForeignKey("dbo.MyOfficeUser", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.MG_BlockGameUser", "User_Id", "dbo.Users");
            DropForeignKey("dbo.LockGame", "TyBa_Id", "dbo.TypeBaneo");
            DropForeignKey("dbo.LockGame", "Game_Id", "dbo.Game");
            DropForeignKey("dbo.LockGame", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "LocationId", "dbo.Location");
            DropForeignKey("dbo.UserEquals", "ApplicationUser_Id1", "dbo.Users");
            DropForeignKey("dbo.UserEquals", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClients", "ApplicationUser_Id1", "dbo.Users");
            DropForeignKey("dbo.UserClients", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.City");
            DropForeignKey("dbo.Users", "AreaId", "dbo.Area");
            DropForeignKey("dbo.AnswersForum", "ReFo_Id", "dbo.ResourceForum");
            DropForeignKey("dbo.ResourceForum", "Job_Id", "dbo.Job");
            DropForeignKey("dbo.BookRatings", "ReJo_Id", "dbo.ResourceJobs");
            DropForeignKey("dbo.ResourceJobs", "Job_Id", "dbo.Job");
            DropForeignKey("dbo.Job", "ToCo_Id", "dbo.TopicsCourse");
            DropForeignKey("dbo.ResourceTopics", "ToCo_Id", "dbo.TopicsCourse");
            DropForeignKey("dbo.TopicsCourse", "Modu_Id", "dbo.Module");
            DropForeignKey("dbo.Link", "ToCo_id", "dbo.TopicsCourse");
            DropForeignKey("dbo.TrueOrFalseStudent", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.TrueOrFalseStudent", "User_Id", "dbo.Users");
            DropForeignKey("dbo.TrueOrFalse", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.BankQuestion", "ToCo_Id", "dbo.TopicsCourse");
            DropForeignKey("dbo.Pairing", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.AnswerPairing", "Pair_Id", "dbo.Pairing");
            DropForeignKey("dbo.AnserPairingStudent", "Pair_Id", "dbo.Pairing");
            DropForeignKey("dbo.AnserPairingStudent", "User_Id", "dbo.Users");
            DropForeignKey("dbo.OptionMultiple", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.AnswerOptionMultipleStudent", "OpMu_Id", "dbo.OptionMultiple");
            DropForeignKey("dbo.AnswerOptionMultipleStudent", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AnswerOptionMultiple", "OpMu_Id", "dbo.OptionMultiple");
            DropForeignKey("dbo.OpenQuestion", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.NewAttempts", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.NewAttempts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.BankQuestion", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.ResourceTopic", "ToCo_Id", "dbo.TopicsCourse");
            DropForeignKey("dbo.ResourceTopic", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Position", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Users", "PositionId", "dbo.Position");
            DropForeignKey("dbo.Prize", "PointManagerCategory_PoMaCa_Id", "dbo.PointManagerCategory");
            DropForeignKey("dbo.PointManagerCategory", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.MG_SettingMp", "Plan_Id", "dbo.MG_Template");
            DropForeignKey("dbo.MG_Template", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.MG_Point", "Sett_Id", "dbo.MG_SettingMp");
            DropForeignKey("dbo.MG_Point", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MG_Pairing", "Sett_Id", "dbo.MG_SettingMp");
            DropForeignKey("dbo.MG_AnswerPairing", "Pairi_Id", "dbo.MG_Pairing");
            DropForeignKey("dbo.MG_Order", "Sett_Id", "dbo.MG_SettingMp");
            DropForeignKey("dbo.MG_AnswerOrder", "Order_Id", "dbo.MG_Order");
            DropForeignKey("dbo.MG_MultipleChoice", "Sett_Id", "dbo.MG_SettingMp");
            DropForeignKey("dbo.MG_AnswerMultipleChoice", "MuCh_ID", "dbo.MG_MultipleChoice");
            DropForeignKey("dbo.MG_AnswerUser", "AnMul_ID", "dbo.MG_AnswerMultipleChoice");
            DropForeignKey("dbo.MG_AnswerUser", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MG_SettingMp", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Measure", "TestId", "dbo.Test");
            DropForeignKey("dbo.QuestionTest", "TestId", "dbo.Test");
            DropForeignKey("dbo.QuestionTest", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Question", "ProficiencyId", "dbo.Proficiency");
            DropForeignKey("dbo.MeasureUser", "MausureId", "dbo.Measure");
            DropForeignKey("dbo.MeasureUser", "UsersId", "dbo.Users");
            DropForeignKey("dbo.Measure", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Log", "TaCh_Id", "dbo.TableChange");
            DropForeignKey("dbo.Log", "IdCh_Id", "dbo.IdChange");
            DropForeignKey("dbo.Log", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Log", "CoLo_Id", "dbo.CodeLogs");
            DropForeignKey("dbo.Log", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Location", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.ImageUpload", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Enrollment", "Modu_Id", "dbo.Module");
            DropForeignKey("dbo.Improvement", "Modu_Id", "dbo.Module");
            DropForeignKey("dbo.Improvement", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Module", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Certification", "Module_Modu_Id", "dbo.Module");
            DropForeignKey("dbo.ResourceBetterPractice", "BePr_Id", "dbo.BetterPractice");
            DropForeignKey("dbo.BetterPractice", "Modu_Id", "dbo.Module");
            DropForeignKey("dbo.BetterPractice", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Module", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AdvanceCourse", "Module_Modu_Id", "dbo.Module");
            DropForeignKey("dbo.Desertify", "Enro_Id", "dbo.Enrollment");
            DropForeignKey("dbo.Desertify", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.Desertify", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Enrollment", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Certification", "Enro_Id", "dbo.Enrollment");
            DropForeignKey("dbo.Certification", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Enrollment", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Section", "Edit_Id", "dbo.Edition");
            DropForeignKey("dbo.Article", "sect_Id", "dbo.Section");
            DropForeignKey("dbo.ResourceNw", "Arti_Id", "dbo.Article");
            DropForeignKey("dbo.PointsComment", "Comm_Id", "dbo.Comments");
            DropForeignKey("dbo.PointsComment", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "comm_Author_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Arti_Id", "dbo.Article");
            DropForeignKey("dbo.Edition", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Changeinterface", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Exchange", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Exchange", "Priz_Id", "dbo.Prize");
            DropForeignKey("dbo.Prize", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Prize", "Capr_Id", "dbo.CategoryPrize");
            DropForeignKey("dbo.CategoryPrize", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.CategoryModule", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Area", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Users", "Area_AreaId", "dbo.Area");
            DropForeignKey("dbo.Area", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Attempts", "BaQu_Id", "dbo.BankQuestion");
            DropForeignKey("dbo.Attempts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Job", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ResourceJobs", "User_Id", "dbo.Users");
            DropForeignKey("dbo.BookRatings", "ReFo_Id", "dbo.ResourceForum");
            DropForeignKey("dbo.ResourceForum", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AnswersForum", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSuperiors", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.UserSuperiors", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MyOfficeUser", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.MyOfficeUser", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserEquals", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.UserEquals", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserClients", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.UserClients", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.QuestionTest", new[] { "TestId" });
            DropIndex("dbo.QuestionTest", new[] { "QuestionId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Resource", new[] { "PlanId" });
            DropIndex("dbo.Plan", new[] { "ProficiencyId" });
            DropIndex("dbo.LogsUserInPlans", new[] { "planid_PlanId" });
            DropIndex("dbo.BlockService", new[] { "TySe_Id" });
            DropIndex("dbo.BlockService", new[] { "User_Id" });
            DropIndex("dbo.AdvanceLoseUser", new[] { "ToCo_id" });
            DropIndex("dbo.AdvanceLoseUser", new[] { "User_Id" });
            DropIndex("dbo.UserInfo", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Score", new[] { "ResultId" });
            DropIndex("dbo.Score", new[] { "ProficiencyId" });
            DropIndex("dbo.Result", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Result", new[] { "ResultOwner_Id" });
            DropIndex("dbo.Result", new[] { "QualifiedUser_Id" });
            DropIndex("dbo.Result", new[] { "MeasureId" });
            DropIndex("dbo.Point", new[] { "User_Id" });
            DropIndex("dbo.Point", new[] { "TyPo_Id" });
            DropIndex("dbo.MG_BlockGameUser", new[] { "User_Id" });
            DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.LockGame", new[] { "TyBa_Id" });
            DropIndex("dbo.LockGame", new[] { "Game_Id" });
            DropIndex("dbo.LockGame", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.ResourceTopics", new[] { "ToCo_Id" });
            DropIndex("dbo.Link", new[] { "ToCo_id" });
            DropIndex("dbo.TrueOrFalseStudent", new[] { "BaQu_Id" });
            DropIndex("dbo.TrueOrFalseStudent", new[] { "User_Id" });
            DropIndex("dbo.TrueOrFalse", new[] { "BaQu_Id" });
            DropIndex("dbo.AnswerPairing", new[] { "Pair_Id" });
            DropIndex("dbo.AnserPairingStudent", new[] { "Pair_Id" });
            DropIndex("dbo.AnserPairingStudent", new[] { "User_Id" });
            DropIndex("dbo.Pairing", new[] { "BaQu_Id" });
            DropIndex("dbo.AnswerOptionMultipleStudent", new[] { "OpMu_Id" });
            DropIndex("dbo.AnswerOptionMultipleStudent", new[] { "User_Id" });
            DropIndex("dbo.AnswerOptionMultiple", new[] { "OpMu_Id" });
            DropIndex("dbo.OptionMultiple", new[] { "BaQu_Id" });
            DropIndex("dbo.OpenQuestion", new[] { "BaQu_Id" });
            DropIndex("dbo.NewAttempts", new[] { "User_Id" });
            DropIndex("dbo.NewAttempts", new[] { "BaQu_Id" });
            DropIndex("dbo.ResourceTopic", new[] { "ToCo_Id" });
            DropIndex("dbo.ResourceTopic", new[] { "CompanyId" });
            DropIndex("dbo.Position", new[] { "CompanyId" });
            DropIndex("dbo.PointManagerCategory", new[] { "CompanyId" });
            DropIndex("dbo.MG_Template", new[] { "Company_Id" });
            DropIndex("dbo.MG_Point", new[] { "User_Id" });
            DropIndex("dbo.MG_Point", new[] { "Sett_Id" });
            DropIndex("dbo.MG_AnswerPairing", new[] { "Pairi_Id" });
            DropIndex("dbo.MG_Pairing", new[] { "Sett_Id" });
            DropIndex("dbo.MG_AnswerOrder", new[] { "Order_Id" });
            DropIndex("dbo.MG_Order", new[] { "Sett_Id" });
            DropIndex("dbo.MG_AnswerUser", new[] { "AnMul_ID" });
            DropIndex("dbo.MG_AnswerUser", new[] { "User_Id" });
            DropIndex("dbo.MG_AnswerMultipleChoice", new[] { "MuCh_ID" });
            DropIndex("dbo.MG_MultipleChoice", new[] { "Sett_Id" });
            DropIndex("dbo.MG_SettingMp", new[] { "Company_Id" });
            DropIndex("dbo.MG_SettingMp", new[] { "Plan_Id" });
            DropIndex("dbo.Question", new[] { "ProficiencyId" });
            DropIndex("dbo.MeasureUser", new[] { "UsersId" });
            DropIndex("dbo.MeasureUser", new[] { "MausureId" });
            DropIndex("dbo.Measure", new[] { "TestId" });
            DropIndex("dbo.Measure", new[] { "CompanyId" });
            DropIndex("dbo.Log", new[] { "Company_Id" });
            DropIndex("dbo.Log", new[] { "IdCh_Id" });
            DropIndex("dbo.Log", new[] { "TaCh_Id" });
            DropIndex("dbo.Log", new[] { "CoLo_Id" });
            DropIndex("dbo.Log", new[] { "User_Id" });
            DropIndex("dbo.Location", new[] { "CompanyId" });
            DropIndex("dbo.ImageUpload", new[] { "CompanyId" });
            DropIndex("dbo.Improvement", new[] { "User_Id" });
            DropIndex("dbo.Improvement", new[] { "Modu_Id" });
            DropIndex("dbo.ResourceBetterPractice", new[] { "BePr_Id" });
            DropIndex("dbo.BetterPractice", new[] { "User_Id" });
            DropIndex("dbo.BetterPractice", new[] { "Modu_Id" });
            DropIndex("dbo.Module", new[] { "CompanyId" });
            DropIndex("dbo.Module", new[] { "User_Id" });
            DropIndex("dbo.Desertify", new[] { "Enro_Id" });
            DropIndex("dbo.Desertify", new[] { "User_Id" });
            DropIndex("dbo.Desertify", new[] { "BaQu_Id" });
            DropIndex("dbo.Certification", new[] { "Module_Modu_Id" });
            DropIndex("dbo.Certification", new[] { "User_Id" });
            DropIndex("dbo.Certification", new[] { "Enro_Id" });
            DropIndex("dbo.Enrollment", new[] { "CompanyId" });
            DropIndex("dbo.Enrollment", new[] { "User_Id" });
            DropIndex("dbo.Enrollment", new[] { "Modu_Id" });
            DropIndex("dbo.ResourceNw", new[] { "Arti_Id" });
            DropIndex("dbo.PointsComment", new[] { "User_Id" });
            DropIndex("dbo.PointsComment", new[] { "Comm_Id" });
            DropIndex("dbo.Comments", new[] { "comm_Author_Id" });
            DropIndex("dbo.Comments", new[] { "Arti_Id" });
            DropIndex("dbo.Article", new[] { "sect_Id" });
            DropIndex("dbo.Section", new[] { "Edit_Id" });
            DropIndex("dbo.Edition", new[] { "CompanyId" });
            DropIndex("dbo.Changeinterface", new[] { "CompanyId" });
            DropIndex("dbo.Exchange", new[] { "Point_Poin_Id" });
            DropIndex("dbo.Exchange", new[] { "User_Id" });
            DropIndex("dbo.Exchange", new[] { "Priz_Id" });
            DropIndex("dbo.Prize", new[] { "PointManagerCategory_PoMaCa_Id" });
            DropIndex("dbo.Prize", new[] { "CompanyId" });
            DropIndex("dbo.Prize", new[] { "Capr_Id" });
            DropIndex("dbo.CategoryPrize", new[] { "CompanyId" });
            DropIndex("dbo.CategoryModule", new[] { "CompanyId" });
            DropIndex("dbo.Area", new[] { "CompanyId" });
            DropIndex("dbo.Area", new[] { "UserId" });
            DropIndex("dbo.Company", new[] { "CompanyNit" });
            DropIndex("dbo.Attempts", new[] { "UserId" });
            DropIndex("dbo.Attempts", new[] { "BaQu_Id" });
            DropIndex("dbo.BankQuestion", new[] { "CompanyId" });
            DropIndex("dbo.BankQuestion", new[] { "ToCo_Id" });
            DropIndex("dbo.TopicsCourse", new[] { "Modu_Id" });
            DropIndex("dbo.Job", new[] { "User_Id" });
            DropIndex("dbo.Job", new[] { "ToCo_Id" });
            DropIndex("dbo.ResourceJobs", new[] { "User_Id" });
            DropIndex("dbo.ResourceJobs", new[] { "Job_Id" });
            DropIndex("dbo.BookRatings", new[] { "ReJo_Id" });
            DropIndex("dbo.BookRatings", new[] { "ReFo_Id" });
            DropIndex("dbo.ResourceForum", new[] { "User_Id" });
            DropIndex("dbo.ResourceForum", new[] { "Job_Id" });
            DropIndex("dbo.AnswersForum", new[] { "ReFo_Id" });
            DropIndex("dbo.AnswersForum", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Area_AreaId" });
            DropIndex("dbo.Users", new[] { "LocationId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Users", new[] { "AreaId" });
            DropIndex("dbo.Users", new[] { "PositionId" });
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.AdvanceUser", new[] { "ToCo_id" });
            DropIndex("dbo.AdvanceUser", new[] { "User_Id" });
            DropIndex("dbo.AdvanceCourse", new[] { "Module_Modu_Id" });
            DropIndex("dbo.AdvanceCourse", new[] { "AdUs_Id" });
            DropIndex("dbo.AdvanceCourse", new[] { "User_Id" });
            DropIndex("dbo.AdvanceCourse", new[] { "Enro_Id" });
            DropTable("dbo.UserSuperiors");
            DropTable("dbo.MyOfficeUser");
            DropTable("dbo.UserEquals");
            DropTable("dbo.UserClients");
            DropTable("dbo.QuestionTest");
            DropTable("dbo.StylesLogos");
            DropTable("dbo.Roles");
            DropTable("dbo.QuienSabeMasPuntaje");
            DropTable("dbo.PointsObtainedForUser");
            DropTable("dbo.Resource");
            DropTable("dbo.Plan");
            DropTable("dbo.LogsUserInPlans");
            DropTable("dbo.LogsComunidad");
            DropTable("dbo.CorreoModel");
            DropTable("dbo.TypeServiceBlock");
            DropTable("dbo.BlockService");
            DropTable("dbo.Banner");
            DropTable("dbo.AdvanceLoseUser");
            DropTable("dbo.UserInfo");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Score");
            DropTable("dbo.Result");
            DropTable("dbo.TypePoint");
            DropTable("dbo.Point");
            DropTable("dbo.MG_BlockGameUser");
            DropTable("dbo.UserLogins");
            DropTable("dbo.TypeBaneo");
            DropTable("dbo.Game");
            DropTable("dbo.LockGame");
            DropTable("dbo.UserClaims");
            DropTable("dbo.City");
            DropTable("dbo.ResourceTopics");
            DropTable("dbo.Link");
            DropTable("dbo.TrueOrFalseStudent");
            DropTable("dbo.TrueOrFalse");
            DropTable("dbo.AnswerPairing");
            DropTable("dbo.AnserPairingStudent");
            DropTable("dbo.Pairing");
            DropTable("dbo.AnswerOptionMultipleStudent");
            DropTable("dbo.AnswerOptionMultiple");
            DropTable("dbo.OptionMultiple");
            DropTable("dbo.OpenQuestion");
            DropTable("dbo.NewAttempts");
            DropTable("dbo.ResourceTopic");
            DropTable("dbo.Position");
            DropTable("dbo.PointManagerCategory");
            DropTable("dbo.MG_Template");
            DropTable("dbo.MG_Point");
            DropTable("dbo.MG_AnswerPairing");
            DropTable("dbo.MG_Pairing");
            DropTable("dbo.MG_AnswerOrder");
            DropTable("dbo.MG_Order");
            DropTable("dbo.MG_AnswerUser");
            DropTable("dbo.MG_AnswerMultipleChoice");
            DropTable("dbo.MG_MultipleChoice");
            DropTable("dbo.MG_SettingMp");
            DropTable("dbo.Proficiency");
            DropTable("dbo.Question");
            DropTable("dbo.Test");
            DropTable("dbo.MeasureUser");
            DropTable("dbo.Measure");
            DropTable("dbo.TableChange");
            DropTable("dbo.IdChange");
            DropTable("dbo.CodeLogs");
            DropTable("dbo.Log");
            DropTable("dbo.Location");
            DropTable("dbo.ImageUpload");
            DropTable("dbo.Improvement");
            DropTable("dbo.ResourceBetterPractice");
            DropTable("dbo.BetterPractice");
            DropTable("dbo.Module");
            DropTable("dbo.Desertify");
            DropTable("dbo.Certification");
            DropTable("dbo.Enrollment");
            DropTable("dbo.ResourceNw");
            DropTable("dbo.PointsComment");
            DropTable("dbo.Comments");
            DropTable("dbo.Article");
            DropTable("dbo.Section");
            DropTable("dbo.Edition");
            DropTable("dbo.Changeinterface");
            DropTable("dbo.Exchange");
            DropTable("dbo.Prize");
            DropTable("dbo.CategoryPrize");
            DropTable("dbo.CategoryModule");
            DropTable("dbo.Area");
            DropTable("dbo.Company");
            DropTable("dbo.Attempts");
            DropTable("dbo.BankQuestion");
            DropTable("dbo.TopicsCourse");
            DropTable("dbo.Job");
            DropTable("dbo.ResourceJobs");
            DropTable("dbo.BookRatings");
            DropTable("dbo.ResourceForum");
            DropTable("dbo.AnswersForum");
            DropTable("dbo.Users");
            DropTable("dbo.AdvanceUser");
            DropTable("dbo.AdvanceCourse");
        }
    }
}
