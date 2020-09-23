namespace vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingDb : DbMigration
    {
        public override void Up()
        {
            Sql(@"insert into MembershipTypes(Name, SignupFee, DurationInMonth, DiscountRate) values
                    ('Pay as you go', 0, 0, 0),
                    ('Monthly', 30, 1, 5),
	                ('Quaterly',60,3,10),
	                ('yearly',90,12,15);

                    insert into MoviesGeneres(Name) values('Kids'),('18+'),('Action'),('Crimee');");
        }
        
        public override void Down()
        {
        }
    }
}
