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
            routes.MapRoute(
            name: "SelectDeleteSkill",
            url: "BaseTeams/SelectDeleteSkill/{id}",
            defaults: new { controller = "BaseTeams", action = "SelectDeleteSkill", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "AllSkillTypes",
                url: "BaseRules/AllSkillTypes/{id}",
                defaults: new { controller = "BaseRules", action = "AllSkillTypes", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "AddSkillType",
                url: "BaseRules/AddSkillType/{id}",
                defaults: new { Controller = "BaseRules", action = "AddSkillType", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "EditSkillType",
                url: "BaseRules/EditSkillType/{id}",
                defaults: new { Controller = "BaseRules", action = "EditSkillType", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "DeleteSkillType",
                url: "BaseRules/DeleteSkillType/{id}",
                defaults: new { Controller = "BaseRules", action = "DeleteSkillType", id = UrlParameter.Optional });
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
            routes.MapRoute(
                name: "NextRace",
                url: "BaseTeams/NextRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "NextRace", ID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PrevRace",
                url: "BaseTeams/PrevRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "PrevRace", ID = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "DelRace",
                url: "BaseTeams/DeleteRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "DeleteRace", ID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EditRace",
                url: "BaseTeams/EditRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "EditRace", ID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ViewRace",
                url: "BaseTeams/ViewRace/{ID}",
                defaults: new { controller = "BaseTeams", action = "ViewRace", ID = UrlParameter.Optional }
                );

            routes.MapRoute(

                name: "AddPlayer",
                url: "BaseTeams/AddPlayer/{IRaceID}",
                defaults: new { controller = "BaseTeams", action = "AddPlayer", IRaceID = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "EditPlayer",
                url: "BaseTeams/EditPlayer/{ID}",
                defaults: new { controller = "BaseTeams", action = "EditPlayer", ID = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "AddSkill",
                url: "BaseTeams/AddSkill/{ID}",
                defaults: new { Controller = "BaseTeams", action = "AddSkill", ID = UrlParameter.Optional}
                );

            
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
