using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BB2020MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                    
            //Rules Routes
            //All
            routes.MapRoute(
                name: "AllSkillTypes",
                url: "BaseRules/AllSkillTypes/{id}",
                defaults: new { controller = "BaseRules", action = "AllSkillTypes", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "AllSkills",
                url: "BaseRules/AllSkills/{id}",
                defaults: new { Controller = "BaseRules", action = "AllSkills", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "AllSpecialRules",
                url: "BaseRules/AllSpecialRules/{id}",
                defaults: new { Controller = "BaseRules", action = "AllSpecialRules", id = UrlParameter.Optional });

            //ADD
            routes.MapRoute(
                name: "AddSkillType",
                url: "BaseRules/AddSkillType/{id}",
                defaults: new { Controller = "BaseRules", action = "AddSkillType", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "AddSkill",
                url: "BaseRules/AddSkill/{id}",
                 defaults: new { Controller = "BaseRules", action = "AddSkill", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "AddSpecialRule",
                url: "BaseRules/AddSpecialRule/{id}",
                defaults: new { Controller = "BaseRules", action = "AddSpecialRule", id = UrlParameter.Optional });
            //EDIT
            routes.MapRoute(
                name: "EditSkillType",
                url: "BaseRules/EditSkillType/{id}",
                defaults: new { Controller = "BaseRules", action = "EditSkillType", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "EditSkill",
                url: "BaseRules/EditSkill/{id}",
                defaults: new { Controller = "BaseRules", action = "EditSkill", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "EditSpecialRule",
                url: "BaseRules/EditSpecialRule/{id}",
                defaults: new { Controller = "BaseRules", action = "EditSpecialRule", id = UrlParameter.Optional });
            //DELETE
            routes.MapRoute(
                name: "DeleteSkillType",
                url: "BaseRules/DeleteSkillType/{id}",
                defaults: new { Controller = "BaseRules", action = "DeleteSkillType", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "DeleteSkill",
                url: "BaseRules/DeleteSkill/{id}",
                defaults: new { Controller = "BaseRules", action = "DeleteSkill", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "DeleteSpecialRule",
                url: "BaseRules/DeleteSpecialRule/{id}",
                defaults: new { Controller = "BaseRules", action = "DeleteSpecialRule", id = UrlParameter.Optional });
                


            //Main MapRoutes
            routes.MapRoute(
                name: "Test",
                url: "Main/Test/{TstVAl}",
                defaults: new { controller = "Main", Action = "Test", TstVal = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Navbar",
                url: "Main/Navbar/{id}",
                defaults: new { controller = "Main", action = "Navbar", id = UrlParameter.Optional }
            );


            //Base teams
            //VIEW
            routes.MapRoute(
                name: "ViewRace",
                url: "BaseTeams/ViewRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "ViewRace", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "NextRace",
                url: "BaseTeams/NextRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "NextRace", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "PrevRace",
                url: "BaseTeams/PrevRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "PrevRace", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "SkillIndex",
                url: "BaseTeams/SkillIndex/{ID}",
                defaults: new { Controller = "BaseTeams", Action = "SkillIndex", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "SkillTypesIndex",
                url: "BaseTeams/SkillTypesIndex/{ID}",
                defaults: new { Controller = "BaseTeams", Action = "SkillTypesIndex", ID = UrlParameter.Optional });

            //ADD
            routes.MapRoute(
                name: "BAddSkill",
                url: "BaseTeams/AddSkill/{ID}",
                defaults: new { Controller = "BaseTeams", action = "BAddSkill", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BAddPlayer",
                url: "BaseTeams/AddPlayer/{ID}",
                defaults: new { controller = "BaseTeams", action = "BAddPlayer", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "AddRace",
                url: "BaseTeams/AddRace/{ID}",
                defaults: new { Controller = "BaseTeams", action = "AddRace", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BAddSkillType",
                url: "BaseTeams/AddSkillType/{ID}",
                defaults: new { Controller = "BaseTeams", action = "BAddSkillType", ID = UrlParameter.Optional });


            //EDIT
            routes.MapRoute(
                name: "EditRace",
                url: "BaseTeams/EditRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "EditRace", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BEditPlayer",
                url: "BaseTeams/EditPlayer/{ID}",
                defaults: new { controller = "BaseTeams", action = "BEditPlayer", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BEditSkillType",
                url: "BaseTeams/EditSkillType/{ID}",
                defaults: new { Controller = "BaseTeams", action = "BEditSkillType", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BEditSpecialRules",
                url: "BaseTeams/EditSpecialRules/{ID}",
                defaults: new { Controller = "BaseTeams", action = "BEditSpecialRule", ID = UrlParameter.Optional });



                
            //DELETE
            routes.MapRoute(
                name: "SelectDeleteSkill",
                url: "BaseTeams/SelectDeleteSkill/{id}",
                defaults: new { controller = "BaseTeams", action = "SelectDeleteSkill", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "DeleteRace",
                url: "BaseTeams/DeleteRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "DeleteRace", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BDeleteSkill",
                url: "BaseTeams/DeleteSkill/{ID}",
                defaults: new { Controller = "BaseTeams", Action = "BDeleteSkill", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BDeleteSkillType",
                url: "BaseTeams/DeleteSkillType/{ID}",
                defaults: new { Controller = "BaseTeams", Action = "BDeleteSkillType", ID = UrlParameter.Optional });
            routes.MapRoute(
                name: "BDeletePlayer",
                url: "BaseTeams/BDeletePlayer/{ID}",
                defaults: new { Controller = "BaseTeams", action = "BDeletePlayer", ID = UrlParameter.Optional });
            
            
            //Rosters Routes
            //VIEW
            //ADD
            //EDIT
            //DELETE


            //Default Routes
            routes.MapRoute(
                name: "RulesDefault",
                url: "BaseRules/{action}/{ID}",
                defaults: new { controller = "BaseRules", action = "Index", ID= UrlParameter.Optional });
            routes.MapRoute(
                name: "BaseTeamsDefault",
                url: "BaseTeams/{action}/{id}",
                defaults: new { controller = "BaseTeams", action = "Index", ID= UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Default",
                url: "{Controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", ID = UrlParameter.Optional }
                );
        }
    }
}
